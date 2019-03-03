using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MapDoor : MapObject
{
    private DoorScript doorScript;
    public Sprite DoorSprite;
    
    protected override void Awake()
    {
        // Call base
        base.Awake();

        // Search in the entire scene for the RoomScript associated to the Room UI Widget
        this.doorScript = FindObjectsOfType<DoorScript>().FirstOrDefault(o => o.name == this.name);
        if (this.doorScript is null) throw new System.NullReferenceException("GameObject must have DoorScript component attached to it.");
    }

    public override void Render()
    {
        this.displayImage.sprite = this.DoorSprite;
    }

    protected override void GenerateDescription()
    {
        this.objectDescription = "Une porte commune.";
    }
}
