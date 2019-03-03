using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComsScript : MonoBehaviour
{
    bool isrepair;
    // Start is called before the first frame update
    void Start()
    {
        isrepair = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (isrepair)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            Debug.Log("WARNING COMSCONSOLE BROKEN CHANGE CIRCUIT");
            GameObject.FindGameObjectWithTag("HUD").GetComponentInChildren<HudController>().setTextInfo("Veuillez insérer la clé physique (se trouve actuellement près d'une brêche)", false);
        }
    }

    public void repair()
    {
        isrepair = true;
    }
}
