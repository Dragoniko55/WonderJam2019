using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public Slider sliderOxygen; // Jauge representant le niveau oxygene
    public Text textInfo; // Texte affichant les informations importantes et utiles
    public Image barreSlider;
    public Gradient sliderGradient;
    public AudioSource textInfoAlarm;
    public float lowOxygenLevel;
    public AudioSource oxygenLevelAlarm;
    public int nonImportantTextDuration;

    private bool textAlarm;
    private bool infoTextBlinking;

    // TESTS & DEBUG
    //bool test = false;
    // FIN TESTS & DEBUG

    // Start is called before the first frame update
    void Start()
    {
        sliderOxygen.value = 1.0f; // Jauge pleine au début
        textInfo.text = ""; // Texte vide au début
        infoTextBlinking = false;

        // TESTS & DEBUG

        // FIN TESTS & DEBUG
    }

    // Update is called once per frame
    void Update()
    {
        barreSlider.color = sliderGradient.Evaluate(sliderOxygen.value);

        // TESTS & DEBUG
        //if (!test)
        //{
        //    setOxygenValue(0.3f);
        //    setTextInfo("MODE DEBUG", true);
        //    //setTextInfo("MODE DEBUG", false);
        //    test = true;
        //}
        // FIN TESTS & DEBUG
    }

    public void setOxygenValue(float newOxygenValue = 1.0f)
    {
        if (newOxygenValue < 0.0f) { newOxygenValue = 0.0f; }
        else if (newOxygenValue > 1.0f) { newOxygenValue = 1.0f; }
        sliderOxygen.value = newOxygenValue;
        if (sliderOxygen.value < lowOxygenLevel)
        {
            oxygenLevelAlarm.Play();
            setTextInfo("Pression globale trop basse", true);
        }
        else
        {
            oxygenLevelAlarm.Stop();
            setTextInfo("", false);
        }
    }

    public void setTextInfo(string newText = "", bool textBlink = false, bool playAlarm = false)
    {
        textInfo.text = newText;
        if (textBlink && !infoTextBlinking)
        {
            StartCoroutine("makeTextBlink");
            infoTextBlinking = true;
            if (playAlarm) { textInfoAlarm.Play(); }
        }
        else if (!textBlink)
        {
            StopCoroutine("makeTextBlink");
            textInfoAlarm.Stop();
            infoTextBlinking = false;
            textInfo.color = new Color(0, 0, 0);
            StopCoroutine("waitAndDeleteTextInfo");
            StartCoroutine("waitAndDeleteTextInfo");
            Debug.Log("Debug setter");
        }
    }

    IEnumerator makeTextBlink()
    {
        while (true)
        {
            textInfo.color = new Color(255, 0, 0);
            yield return new WaitForSeconds(.1f);
            textInfo.color = new Color(0, 0, 0);
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator waitAndDeleteTextInfo()
    {
        Debug.Log("Debug coroutine");
        yield return new WaitForSeconds(nonImportantTextDuration);
        if (!infoTextBlinking)
        {
            setTextInfo("", false);
        }
    }
}
