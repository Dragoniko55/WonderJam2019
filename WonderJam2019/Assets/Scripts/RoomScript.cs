using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    [SerializeField] float currentPressure;
    [SerializeField] float RequiredPressure;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isPressurised()
    {
        if (currentPressure >= RequiredPressure)
            return true;
        else
            return false;
    }
}
