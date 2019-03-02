using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactionController : MonoBehaviour
{
    public float interactionDist;
    public Camera mainCamera;
    public Image crosshairs;
    public Text interactText;
    public Color crosshairsOver;
    public Color crosshairsOut;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        interactText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out hit, interactionDist, layerMask))
        {
            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log("CLIC");
                hit.collider.SendMessageUpwards("Interact");
            }

            Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            crosshairs.color = crosshairsOver;
            interactText.enabled = true;
        }
        else
        {
            crosshairs.color = crosshairsOut;
            interactText.enabled = false;
        }
        
    }
}
