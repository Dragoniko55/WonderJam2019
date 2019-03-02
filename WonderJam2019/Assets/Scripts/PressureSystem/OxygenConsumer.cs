using System;
using UnityEngine;

public class OxygenConsumer : MonoBehaviour
{
    public event Action<OxygenConsumer> VolumeChanged;
    public event Action<OxygenConsumer> PressureChanged;
    
    public long Volume { get; set; }
    public long CurrentPressure { get; set; }
}