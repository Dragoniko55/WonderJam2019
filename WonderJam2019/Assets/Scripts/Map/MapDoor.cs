using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MapDoor : MapObject
{
    private DoorScript doorScript;
    public Sprite OpenDoorSprite;
    public Sprite ClosedDoorSprite;
    
    protected override void Awake()
    {
        // Call base
        base.Awake();

        // Search in the entire scene for the RoomScript associated to the Room UI Widget
        this.doorScript = FindObjectsOfType<DoorScript>().FirstOrDefault(o => o.name == this.name);
        if (this.doorScript is null) throw new System.NullReferenceException("GameObject must have DoorScript component attached to it.");

        // Bind render event
        this.doorScript.OnDoorChange += this.Render;
        this.doorScript.OnDoorChange += this.GenerateDescription;
    }

    public override void Render()
    {
        // TODO: Implement
        if(this.doorScript.IsOpen())
        {
            this.displayImage.sprite = this.OpenDoorSprite;
        }
        else
        {
            this.OpenDoorSprite = this.ClosedDoorSprite;
        }
    }

    protected override void GenerateDescription()
    {
        if(this.doorScript.IsOpen())
        {
            this.objectDescription = "The door is open.";
        }

        else
        {
            this.objectDescription = "A common door.";
        }   
    }
}
