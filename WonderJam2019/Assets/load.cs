using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class load : MonoBehaviour
{
    private void Awake()
    {
        // Singleton<PressureManager>.EnsureCreated();
    }

    // Start is called before the first frame update
    void Start()
    {
        Singleton<PressureManager>.EnsureCreated();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
