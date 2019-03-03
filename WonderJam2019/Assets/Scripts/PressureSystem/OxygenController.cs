using System;
using System.Collections.Generic;
using UnityEngine;

public class OxygenController : MonoBehaviour
{
    private Animator _animator;

    [SerializeField]
    private OxygenConsumer[] _oxygenConsumers;

    [SerializeField]
    private bool _isOpened;

    public event Action<OxygenController> Opened;
    public event Action<OxygenController> Closed;

    public AudioClip valveSound;
    public AudioSource audioValve;

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

    private void Awake()
    {
        this._animator = this.GetComponent<Animator>();
        if (this._animator == null) throw new NullReferenceException("Cannot find the animator associated to the valve.");
    }

    public void Interact()
    {
        this.IsOpened = !this.IsOpened;
        if (!this.IsOpened)
        {
            this._animator.Play("valveClose");
            GameObject.FindGameObjectWithTag("HUD").GetComponentInChildren<HudController>().setTextInfo("Fermeture de la valve", false);
        }
        else
        {
            this._animator.Play("valveOpen");
            GameObject.FindGameObjectWithTag("HUD").GetComponentInChildren<HudController>().setTextInfo("Ouverture de la valve", false);
        }
        audioValve.PlayOneShot(valveSound);
    }
}