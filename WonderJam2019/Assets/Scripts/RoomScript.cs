using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OxygenConsumer))]
public class RoomScript : MonoBehaviour
{
    [SerializeField] private string roomName;
    [SerializeField] private long requiredPressure = 1000;
    [SerializeField] private float currentPressure;
    private OxygenConsumer oxygenConsumer;

    public event System.Action OnRoomChange;

    public string RoomName
    {
        get
        {
            return this.roomName;
        }
    }
    
    public float RequiredPressure
    {
        get
        {
            return this.requiredPressure;
        }
    }   
    
    public float CurrentPressure
    {
        get
        {
            return this.currentPressure;
        }
        private set
        {            
            this.currentPressure = value;
            this.OnRoomChange?.Invoke();
        }
    }

    public OxygenConsumer OxygenConsumer
    {
        get
        {
            return this.oxygenConsumer;
        }
    }


    private void Awake()
    {
        this.oxygenConsumer = this.GetComponent<OxygenConsumer>();
        if (this.oxygenConsumer is null) throw new System.NullReferenceException("GameObject must have OxygenProducer component attached to it.");

        // Bind pressure changed event
        this.oxygenConsumer.PressureChanged += c => this.CurrentPressure = c.CurrentPressure;

    }


    public bool IsPressurised()
    {
        return this.currentPressure >= this.requiredPressure;
    }
}
