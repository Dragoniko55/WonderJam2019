using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] GameObject LinkedRoom;
    [SerializeField] private bool isOpen = false;
    
    public event System.Action OnDoorChange;

    public void Close()
    {
        // TODO: To Implement
        this.isOpen = false;
        this.OnDoorChange();
    }

    public bool IsOpen()
    {
        return this.isOpen;
    }

    public void Open()
    {
        bool CanOpen = LinkedRoom.GetComponent<RoomScript>().isPressurised();

        if (CanOpen)
        {
            Debug.Log("Door Oppening");
            this.isOpen = true;
            this.OnDoorChange();
        } 
        else
        {
            Debug.Log("WARNING PRESSURE TOO LOW");
        }            
    }
}
