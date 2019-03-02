using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Outline))]
public class MapRoom : MapObject
{
    public int SecondsHighlighted = 5;

    private RoomScript roomScript;    
    private Outline outline;
    private MapValve[] mapValves;

    protected override void Awake()
    {
        // Call base
        base.Awake();

        // Search in the entire scene for the RoomScript associated to the Room UI Widget
        this.roomScript = FindObjectsOfType<RoomScript>().FirstOrDefault(o => o.name == this.name);
        if (this.roomScript is null) throw new System.NullReferenceException("GameObject must have RoomScript component attached to it.");

        this.outline = this.GetComponent<Outline>();
        if (this.outline is null) throw new System.NullReferenceException("GameObject must have Outline component attached to it.");

        // Bind render and description event
        this.roomScript.OnRoomChange += this.Render;
        this.roomScript.OnRoomChange += this.GenerateDescription;

        Singleton<PressureManager>.Instance.GeneratedGraphs += c => this.Render();
        Singleton<PressureManager>.Instance.GeneratedGraphs += c => this.GenerateDescription();

        // Set room name
        this.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = this.roomScript.RoomName;
    }

    protected override void Start()
    {
        base.Start();

        // Get map valves
        this.mapValves = Singleton<PressureManager>.Instance
            .GetLinkedControllers(this.roomScript.OxygenConsumer)
            .Select(v => v.GetComponent<ValveScript>().mapValve)
            .ToArray();
    }

    public override void Render()
    {
        // var roomContainer = rootMap.transform.Find(MapManager.RoomsContainerName);
        // if (roomContainer is null) throw new System.NullReferenceException("Room folder not found in the specified canvas.");
        
        // TODO: get the network id from the singleton by passing reference to roomScript
        var network_id = Singleton<PressureManager>.Instance.GetNetworkIndex(this.roomScript.OxygenConsumer);
        
        if(network_id > -1)
        {
            if (!this.outline.enabled)
                this.outline.enabled = true;

            this.outline.effectColor = Singleton<MapManager>.Instance.NetworkColors[network_id];
        }
        else
        {
            // TODO: No network!
            this.outline.enabled = false;
        }

        if(this.roomScript.IsPressurised())
        {
            this.displayImage.color = Color.blue;
        }
        else
        {
            this.displayImage.color = Color.red;
        }
    }

    protected override void GenerateDescription()
    {
        var sb = new System.Text.StringBuilder("<size=200%><b>Room ");
        sb.Append(this.roomScript.RoomName);
        sb.Append("</b><size=100%>");
        sb.Append(System.Environment.NewLine);
        sb.Append("Network ID: ");
        sb.Append(Singleton<PressureManager>.Instance.GetNetworkIndex(this.roomScript.OxygenConsumer).ToString());
        sb.Append(System.Environment.NewLine);
        sb.Append("Room pressure: ");
        sb.Append(this.roomScript.CurrentPressure);
        sb.Append(" units");
        sb.Append(System.Environment.NewLine);
        sb.Append("Room volume: ");
        sb.Append(this.roomScript.OxygenConsumer.Volume);
        sb.Append(" units");
        this.objectDescription = sb.ToString();
    }


    public override void OnSelected()
    {
        base.OnSelected();

        Debug.Log(mapValves.Count());

        foreach (var mapValve in this.mapValves)
        {
            mapValve.Higlight();
        }
    }

    public override void OnUnselected()
    {
        base.OnUnselected();

        foreach (var mapValve in this.mapValves)
        {
            mapValve.UnHighlight();
        }
    }
}
