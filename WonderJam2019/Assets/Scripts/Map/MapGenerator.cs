using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapGenerator : MapObject, IPointerClickHandler
{
    public Color ActivatedGeneratorColor;
    public Color DeactivatedGeneratorColor;

    private OxygenProducer oxygenProducer;
    private MapValve[] mapValves;

    protected override void Awake()
    {
        // Call base
        base.Awake();

        // Get oxygen producer
        this.oxygenProducer = FindObjectsOfType<OxygenProducer>().FirstOrDefault(oc => oc.name == this.name);
        if(this.oxygenProducer is null) throw new System.NullReferenceException("Generator must contain a oxygen producer component.");

        // Bind event
        this.oxygenProducer.Activated += op => this.Render();
        this.oxygenProducer.Activated += op => this.GenerateDescription();
    }

    protected override void Start()
    {
        base.Start();

        // Get map valves
        this.mapValves = this.oxygenProducer?.OxygenControllers
            ?.Select(v => v.GetComponent<ValveScript>()?.mapValve)
            ?.Where(mv => mv != null)
            ?.ToArray();
    }

    public override void Render()
    {
        if(this.oxygenProducer.IsActive)
        {
            this.displayImage.color = this.ActivatedGeneratorColor;
        }

        else
        {
            this.displayImage.color = this.DeactivatedGeneratorColor;
        }
    }

    protected override void GenerateDescription()
    {
        var sb = new System.Text.StringBuilder();
        sb.Append("<size=200%><b>");
        sb.Append("Un Générateur");
        sb.Append("</b><size=100%>");
        sb.Append(System.Environment.NewLine);
        sb.Append("=====================");
        sb.Append(System.Environment.NewLine);
        sb.Append("Etat: " + (this.oxygenProducer.IsActive ? "On" : "Off"));
        this.objectDescription = sb.ToString();
    }

    public override void OnSelected()
    {
        base.OnSelected();

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
