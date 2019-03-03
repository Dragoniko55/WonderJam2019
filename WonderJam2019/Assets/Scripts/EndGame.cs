using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine("Reset");
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(12);
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {

       

    }
}
