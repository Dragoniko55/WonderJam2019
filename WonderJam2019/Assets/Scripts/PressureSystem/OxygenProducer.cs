using System;
using System.Collections.Generic;
using UnityEngine;

public class OxygenProducer : MonoBehaviour
{
    [SerializeField]
    private OxygenController[] _oxygenControllers;

    [SerializeField]
    private bool _isActive;

    [SerializeField]
    private long _availablePressure = 1000;

    public event Action<OxygenProducer> Activated;

    public bool IsActive
    {
        get => this._isActive;
        set
        {
            if (value != this._isActive)
            {
                this._isActive = value;
                
                if (this._isActive)
                {
                    this.Activated?.Invoke(this);
                }
            }
        }
    }

    public long AvailablePressure => this._availablePressure;
    public long CurrentPressure => this.IsActive ? this.AvailablePressure : 0;
    public IEnumerable<OxygenController> OxygenControllers => this._oxygenControllers;

    private void Awake()
    {
        if (this.IsActive)
        {
            this.Interact();
        }
    }

    public void Interact()
    {
        DestroyImmediate(this.GetComponentInChildren<SphereCollider>());
        this.IsActive = true;
    }
}