using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class MapRoom : MapObject, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // for testing
    private RoomScript roomScript;

    protected override void Awake()
    {
        // Call base
        base.Awake();

        // Search in the entire scene for the RoomScript associated to the Room UI Widget
        this.roomScript = FindObjectsOfType<RoomScript>().FirstOrDefault(o => o.name == this.name);
        if (this.roomScript is null) throw new System.NullReferenceException("GameObject must have RoomScript component attached to it.");

        this.roomScript.OnRoomChange += this.Render;
    }

    private void Start()
    {
        // for testing
        this.Render();
    }

    public override void Render()
    {
        var roomContainer = rootMap.transform.Find(MapManager.RoomsContainerName);
        if (roomContainer is null) throw new System.NullReferenceException("Room folder not found in the specified canvas.");

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

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
