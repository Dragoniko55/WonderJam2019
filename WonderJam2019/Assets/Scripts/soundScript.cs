using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundScript : MonoBehaviour
{
    public AudioClip open;
    public AudioClip close;
    public AudioSource source;

    public bool playOpen = false;
    public bool playClose = false;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playOpen)
        {
            source.PlayOneShot(open);
            playOpen = false;
        }
        else if (playClose)
        {
            source.PlayOneShot(close);
            playClose = false;
        }
    }
}
