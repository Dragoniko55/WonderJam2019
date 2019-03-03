using System;
using UnityEngine;

[RequireComponent(typeof(RoomScript))]
public class OxygenConsumer : MonoBehaviour
{
    [SerializeField]
    private long _volume = 1000;
    private long _currentPressure;

    public event Action<OxygenConsumer> VolumeChanged;
    public event Action<OxygenConsumer> PressureChanged;
    
    public long Volume
    {
        get => this._volume;
        set
        {
            if (value != this._volume)
            {
                this._volume = value;
                this.VolumeChanged?.Invoke(this);
            }
        }
    }

    public long CurrentPressure
    {
        get => this._currentPressure;
        set
        {
            if (value != this._currentPressure)
            {
                this._currentPressure = value;
                this.PressureChanged?.Invoke(this);
            }
        }
    }
}