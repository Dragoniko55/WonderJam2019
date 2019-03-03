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
        }
    }

    public void repair()
    {
        isrepair = true;
    }
}
