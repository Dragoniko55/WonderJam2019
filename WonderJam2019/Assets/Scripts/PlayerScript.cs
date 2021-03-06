﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour
{
    public float PressureTolerance = 1000f;
    public float MaxDisplayPressure = 5000f;
    public TMPro.TextMeshProUGUI RoomText;

    public AudioSource dyingSource;

    private HudController hudController;

    private RoomScript previousRoom;
    private RoomScript currentRoom;

    public RoomScript CurrentRoom
    {
        get => this.currentRoom;
        set
        {
            if (value != this.currentRoom)
            {
                this.RoomText.text = "Couloir";

                if (this.previousRoom != null && value != this.previousRoom)
                {
                    this.previousRoom.OxygenConsumer.PressureChanged -= OxygenConsumer_PressureChanged;
                }

                if (this.currentRoom == null && this.previousRoom != value)
                {
                    value.OxygenConsumer.PressureChanged += OxygenConsumer_PressureChanged;
                }

                this.previousRoom = this.currentRoom;
                this.currentRoom = value;

                if (this.currentRoom != null)
                {
                    this.RoomText.text = "Pièce " + value.RoomName;
                    this.OxygenConsumer_PressureChanged(this.currentRoom.OxygenConsumer);
                }
            }
        }
    }

    private void Awake()
    {
        this.hudController = FindObjectOfType<HudController>();
    }

    private void OxygenConsumer_PressureChanged(OxygenConsumer oxygenConsumer)
    {
        this.hudController.setOxygenValue((oxygenConsumer.CurrentPressure - this.PressureTolerance) / this.MaxDisplayPressure);

        if (oxygenConsumer.CurrentPressure < this.PressureTolerance)
        {

            StartCoroutine(die());

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator die()
    {
        var currentRoom = this.CurrentRoom;

        //cought
        if (!dyingSource.isPlaying)
        {
            dyingSource.Play();
        }

        yield return new WaitForSeconds(5f);

        var room = CurrentRoom ?? previousRoom;
        if (room == currentRoom && !room.IsPressurised())
        {

            SceneManager.LoadScene(3);
        }

        dyingSource.Stop();
    }
}


