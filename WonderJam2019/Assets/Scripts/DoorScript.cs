using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] GameObject LinkedRoom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void open()
    {
        bool CanOpen = LinkedRoom.GetComponent<RoomScript>().isPressurised();

        if (CanOpen)
        {
            Debug.Log("Door Oppening");
        } 
        else
        {
            Debug.Log("WARNING PRESSURE TOO LOW");
        }
            
    }
}
