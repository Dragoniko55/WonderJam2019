using System.Collections;
using UnityEngine;

[RequireComponent(typeof(OxygenConsumer))]
public class LeakingConsumer : MonoBehaviour
{
    [SerializeField]
    private float _secondsPerIncrement = 0.2f;

    [SerializeField]
    private long _volumeIncreasePerIncrement = 100;

    private OxygenConsumer _oxygenConsumer;
    private long _initialVolume;

    private void Awake()
    {
        this._oxygenConsumer = this.GetComponent<OxygenConsumer>();
        this._initialVolume = this._oxygenConsumer.Volume;
    }

    private void Start()
    {
        this.StartCoroutine(nameof(this.UpdateVolume));
    }

    private IEnumerator UpdateVolume()
    {
        while (true)
        {
            if (Singleton<PressureManager>.Instance.GetNetworkIndex(this._oxygenConsumer) >= 0)
            {
                this._oxygenConsumer.Volume += this._volumeIncreasePerIncrement;
            }
            else
            {
                this._oxygenConsumer.Volume = this._initialVolume;
            }

            yield return new WaitForSeconds(this._secondsPerIncrement);
        }
    }
}