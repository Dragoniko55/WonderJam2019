using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Outline))]
public class MapValve : MapObject
{
    public Sprite OpenValveSprite;
    public Sprite ClosedValveSprite;

    private Outline outline;

    protected override void Awake()
    {
        // Call base
        base.Awake();

        this.outline = this.GetComponent<Outline>();
        if (this.outline is null) throw new System.NullReferenceException("GameObject must have Outline component attached to it.");
        this.outline.enabled = false;
    }

    public void ShowOutline()
    {
        this.outline.enabled = true;
    }

    public void HideOutline()
    {
        this.outline.enabled = false;
    }

    public override void Render()
    {
        if(true) // TODO: Check if valve is activated
        {
            this.displayImage.sprite = this.OpenValveSprite;
        }
        else
        {
            this.displayImage.sprite = this.ClosedValveSprite;
        }
    }

    protected override void GenerateDescription()
    {
        var sb = new System.Text.StringBuilder();
        sb.Append("<size=200%><b>");
        sb.Append("Title");
        sb.Append("</b><size=100%>");
        sb.Append(System.Environment.NewLine);
        this.objectDescription = sb.ToString();
    }
}
