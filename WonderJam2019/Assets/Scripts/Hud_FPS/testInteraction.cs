using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testInteraction : MonoBehaviour
{
    int it = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Interact()
    {
        if (it % 2 == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<HudController>().setTextInfo("Un cube de test IMPORTANT a été cliqué", true);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<HudController>().setOxygenValue(0.1f);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<HudController>().setTextInfo("Un second clic a été réalisé", false);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<HudController>().setOxygenValue(1.0f);
        }
        ++it;
    }
}
