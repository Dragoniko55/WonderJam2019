using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class load : MonoBehaviour
{
    public OxygenController oxygenController;

    private void Awake()
    {
        Singleton<PressureManager>.EnsureCreated();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            oxygenController.IsOpened = !oxygenController.IsOpened;
    }
}
