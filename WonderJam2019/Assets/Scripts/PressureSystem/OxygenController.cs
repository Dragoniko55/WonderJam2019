using System;
using System.Collections.Generic;
using UnityEngine;

public class OxygenController : MonoBehaviour
{
    public event Action Opened;
    public event Action Closed;

    public IEnumerable<OxygenConsumer> OxygenConsumers { get; }
}