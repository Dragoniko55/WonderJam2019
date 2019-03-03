using System;
using System.Collections.Generic;
using UnityEngine;

public class OxygenController : MonoBehaviour
{
    [SerializeField]
    private OxygenConsumer[] _oxygenConsumers;

    [SerializeField]
    private bool _isOpened;

    public event Action<OxygenController> Opened;
    public event Action<OxygenController> Closed;

    public bool IsOpened
    {
        get => this._isOpened;
        set
        {
            if (value != this._isOpened)
            {
                this._isOpened = value;

                if (this._isOpened)
                {
                    this.Opened?.Invoke(this);
                }
                else
                {
                    this.Closed?.Invoke(this);
                }
            }
        }
    }

    public IEnumerable<OxygenConsumer> OxygenConsumers => this._oxygenConsumers;
}