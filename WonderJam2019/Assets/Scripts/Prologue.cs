using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue : MonoBehaviour
{
    public float f_speedText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
        { 
        transform.Translate(new Vector3(0, f_speedText * Time.deltaTime, 0));
        }
    }
}
