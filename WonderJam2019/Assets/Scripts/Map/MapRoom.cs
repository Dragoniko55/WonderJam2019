using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Outline))]
public class MapRoom : MapObject
{
    private RoomScript roomScript;
    private Outline outline;

    protected override void Awake()
    {
        // Call base
        base.Awake();

        // Search in the entire scene for the RoomScript associated to the Room UI Widget
        this.roomScript = FindObjectsOfType<RoomScript>().FirstOrDefault(o => o.name == this.name);
        if (this.roomScript is null) throw new System.NullReferenceException("GameObject must have RoomScript component attached to it.");

        this.outline = this.GetComponent<Outline>();
        if (this.outline is null) throw new System.NullReferenceException("GameObject must have Outline component attached to it.");

        // Bind render event
        this.roomScript.OnRoomChange += this.Render;
        this.roomScript.OnRoomChange += this.GenerateDescription;
    }

    public override void Render()
    {
        // var roomContainer = rootMap.transform.Find(MapManager.RoomsContainerName);
        // if (roomContainer is null) throw new System.NullReferenceException("Room folder not found in the specified canvas.");

        // TODO: get the network id from the singleton by passing reference to roomScript
        var network_id = 0;
        
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

        if(this.roomScript.isPressurised())
        {
            this.displayImage.color = Color.blue;
        }
        else
        {
            this.displayImage.color = Color.red;
        }

        Debug.Log(this.roomScript.CurrentPressure);
        Debug.Log(this.roomScript.RequiredPressure);
    }

    protected override void GenerateDescription()
    {
        var sb = new System.Text.StringBuilder("<size=200%><b>Room ");
        sb.Append(this.roomScript.RoomName);
        sb.Append("</b><size=100%>");
        sb.Append(System.Environment.NewLine);
        sb.Append("Network ID: x");
        sb.Append(System.Environment.NewLine);
        sb.Append("Room pressure:");
        sb.Append(this.roomScript.CurrentPressure);
        sb.Append(" kpA");
        this.objectDescription = sb.ToString();
    }
}
