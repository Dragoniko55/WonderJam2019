using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    float currentPressure;
    public float CurrentPressure
    {
        get
        {
            return this.currentPressure;
        }
        set
        {
            this.currentPressure = value;
            this.OnRoomChange();
        }
    }

    float requiredPressure;
    public float RequiredPressure
    {
        get
        {
            return this.requiredPressure;
        }
        set
        {
            this.requiredPressure = value;
            this.OnRoomChange();
        }
    }

    public event System.Action OnRoomChange;

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
        /*if (currentPressure >= RequiredPressure)
            return true;
        else
            return false;*/

        return Random.value >= 0.5;
    }


}
