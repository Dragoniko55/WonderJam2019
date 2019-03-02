using System;
using UnityEngine;

public class OxygenConsumer : MonoBehaviour
{
    public event Action VolumeUpdated;
    
    public long Volume { get; set; }
    public long CurrentPressure { get; set; }
}