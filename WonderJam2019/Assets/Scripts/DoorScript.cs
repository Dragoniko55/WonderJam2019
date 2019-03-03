using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] GameObject LinkedRoom;
    [SerializeField] private bool isOpen = false;

    RoomScript room;
    Transform doortransform;

    public Animator animOpenDoor;


    float smoothFactor = 2f;
    Vector3 velocity = Vector3.zero;

    bool isopening = false;

    Vector3 initposs;
    Vector3 targetposs;

    public event System.Action OnDoorChange;

    private void Start()
    {
        room = LinkedRoom.GetComponent<RoomScript>();
    }

    //void Update()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        Interact();
    //    }
    //    Interact();
    //}
    
    public void Interact()
    {
        bool CanOpen = room.IsPressurised();
        if (CanOpen)
        {
            Debug.Log("Door Oppening");
            animOpenDoor.Play("porteinteract");
            //StartCoroutine(openandclose());
        } 
        else
        {
            Debug.Log("WARNING PRESSURE TOO LOW");
        }            
    }
}
