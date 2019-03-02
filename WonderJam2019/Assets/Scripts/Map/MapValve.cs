using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

[RequireComponent(typeof(Outline))]
public class MapValve : MapObject
{
    public Color OpenValveColor;
    public Color ClosedValveColor;

    public Color DefaultOutlineColor;
    public Color SelectedOutlineColor;

    private Outline outline;
    private OxygenController oxygenController;

    protected override void Awake()
    {
        // Call base
        base.Awake();

        // Outline
        this.outline = this.GetComponent<Outline>();
        if (this.outline is null) throw new System.NullReferenceException("GameObject must have Outline component attached to it.");
        this.UnHighlight();

        // Oxygen controller
        this.oxygenController = FindObjectsOfType<OxygenController>().FirstOrDefault(oc => oc.name == this.name);
        if (this.outline is null) throw new System.NullReferenceException("Valve must contain a oxygen controller component.");

        // Bind events
        this.oxygenController.Opened += c => this.Render();
        this.oxygenController.Closed += c => this.Render();

        this.oxygenController.Opened += c => this.GenerateDescription();
        this.oxygenController.Closed += c => this.GenerateDescription();
    }

    public void UnHighlight()
    {
        this.outline.effectColor = this.DefaultOutlineColor;
        this.outline.effectDistance = new Vector2(1, 1);
    }

    public void Higlight()
    {
        this.outline.effectColor = this.SelectedOutlineColor;
        this.outline.effectDistance = new Vector2(3, 3);
    }

    public override void Render()
    {
        if(this.oxygenController.IsOpened)
        {
            this.displayImage.color = this.OpenValveColor;
        }
        else
        {
            this.displayImage.color = this.ClosedValveColor;
        }
    }

    protected override void GenerateDescription()
    {
        var sb = new System.Text.StringBuilder();
        sb.Append("<size=200%><b>");
        sb.Append("A Valve");
        sb.Append("</b><size=100%>");
        sb.Append(System.Environment.NewLine);
        sb.Append("Linked room(s):");

        foreach(var consumer in this.oxygenController.OxygenConsumers)
        {
            sb.Append(System.Environment.NewLine);
            sb.Append("• Room ");
            sb.Append(consumer.GetComponent<RoomScript>().RoomName);
        }

        sb.Append(System.Environment.NewLine);
        sb.Append("=====================");
        sb.Append(System.Environment.NewLine);
        sb.Append("Status: " + (this.oxygenController.IsOpened ? "Opened" : "Closed"));

        this.objectDescription = sb.ToString();
    }
}
