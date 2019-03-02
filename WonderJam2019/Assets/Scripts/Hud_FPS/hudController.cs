using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudController : MonoBehaviour
{
    public Slider sliderOxygen;

    // Start is called before the first frame update
    void Start()
    {
        sliderOxygen.value = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setOxygenValue(float oxygenValue = 1.0f)
    {
        if (oxygenValue < 0.0f) { oxygenValue = 0.0f; }
        else if (oxygenValue > 1.0f) { oxygenValue = 1.0f; }
        sliderOxygen.value = oxygenValue;
    }
}
