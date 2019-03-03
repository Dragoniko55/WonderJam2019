using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(RoomScript))]
public class PlayerDetector : MonoBehaviour
{
    private BoxCollider boxCollider;
    private RoomScript roomScript;

    private void Awake()
    {
        this.boxCollider = this.GetComponent<BoxCollider>();
        this.boxCollider.isTrigger = true;

        this.roomScript = this.GetComponent<RoomScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerScript>().CurrentRoom = this.roomScript; // me clean
        }
    }
}
