using System;
using System.Collections.Generic;
using UnityEngine;

public class OxygenController : MonoBehaviour
{
    [SerializeField] OxygenConsumer[] _oxygenConsumers;

    public event Action<OxygenController> Opened;
    public event Action<OxygenController> Closed;

    public bool IsOpened { get; set; }
    public IEnumerable<OxygenConsumer> OxygenConsumers => this._oxygenConsumers;
}