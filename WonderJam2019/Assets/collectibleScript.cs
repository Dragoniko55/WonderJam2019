using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectibleScript : MonoBehaviour
{
    [SerializeField] GameObject LinkedComs;

    ComsScript thecoms;
    // Start is called before the first frame update
    void Start()
    {
        thecoms = LinkedComs.GetComponent<ComsScript>();
    }

    public void Interact()
    {
        thecoms.repair();
        desablecollectible();
    }

    public void desablecollectible()
    {

        this.GetComponent<BoxCollider>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
    }
}
