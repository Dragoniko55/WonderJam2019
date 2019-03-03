using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectibleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Linkedspawner;

    // Start is called before the first frame update
    void Start()
    {
        int randomint = Random.Range(0, Linkedspawner.Length);
        Debug.Log(randomint);
        int cpt = 0;
        foreach(GameObject spawn in Linkedspawner)
        {

            if (cpt != randomint)
            {
                spawn.GetComponent<collectibleScript>().desablecollectible();
            }
            cpt++;
        }
    }
}
