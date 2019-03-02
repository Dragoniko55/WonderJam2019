using System;
using System.Collections.Generic;
using UnityEngine;

public class OxygenProducer : MonoBehaviour
{
    [SerializeField] OxygenController[] _oxygenControllers;

    public event Action<OxygenProducer> Activated;

    public bool IsActive { get; set; }
    public long AvailablePressure { get; set; }
    public IEnumerable<OxygenController> OxygenControllers => this._oxygenControllers;
}