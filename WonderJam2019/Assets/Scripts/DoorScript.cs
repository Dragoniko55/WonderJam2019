using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] GameObject LinkedRoom;
    [SerializeField] private bool isOpen = false;

    RoomScript room;
    Transform doortransform;

    float smoothFactor = 2f;
    Vector3 velocity = Vector3.zero;

    bool isopening = false;

    Vector3 initposs;
    Vector3 targetposs;

    public event System.Action OnDoorChange;

    private void Start()
    {
        room = LinkedRoom.GetComponent<RoomScript>();
        doortransform = GetComponent<Transform>();

        initposs = doortransform.position;
        targetposs = new Vector3(doortransform.position.x + 2, doortransform.position.y, doortransform.position.z);
        
    }

    void Update()
    {
        if(isopening)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetposs, ref velocity, smoothFactor);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, initposs, ref velocity, smoothFactor);
        }
            
    }

    public void Interact()
    {
        bool CanOpen = room.IsPressurised();

        if (CanOpen)
        {
            Debug.Log("Door Oppening");
            StartCoroutine(openandclose());
        } 
        else
        {
            Debug.Log("WARNING PRESSURE TOO LOW");
        }            
    }

    IEnumerator openandclose()
    {
        print("open");
        isopening = true;
        yield return new WaitForSeconds(5);
        print("close");
        isopening = false;
    }
}
