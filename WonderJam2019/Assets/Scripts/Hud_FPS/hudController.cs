using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudController : MonoBehaviour
{
    public Slider sliderOxygen; // Jauge representant le niveau oxygene
    public Text textInfo; // Texte affichant les informations importantes et utiles

    // TESTS & DEBUG

    // FIN TESTS & DEBUG

    // Start is called before the first frame update
    void Start()
    {
        sliderOxygen.value = 1.0f; // Jauge peine au début
        textInfo.text = "!!!!!!!!!"; // Texte vide au début

        // TESTS & DEBUG
        StartCoroutine("makeTextInfoBlink");
        // FIN TESTS & DEBUG
    }

    // Update is called once per frame
    void Update()
    {


        // TESTS & DEBUG

        // FIN TESTS & DEBUG
    }

    public void setOxygenValue(float newOxygenValue = 1.0f)
    {
        if (newOxygenValue < 0.0f) { newOxygenValue = 0.0f; }
        else if (newOxygenValue > 1.0f) { newOxygenValue = 1.0f; }
        sliderOxygen.value = newOxygenValue;
    }

    public void setTextInfo(string newText = "", bool textBlink = true)
    {
        textInfo.text = newText;
        if (textBlink) { StartCoroutine("makeTextInfoBlink"); }
    }

    IEnumerator makeTextInfoBlink()
    {
        while (true)
        {
            textInfo.color = new Color(255, 0, 0);
            yield return new WaitForSeconds(.1f);
            textInfo.color = new Color(0, 0, 0);
            yield return new WaitForSeconds(.1f);
        }
    }
}
