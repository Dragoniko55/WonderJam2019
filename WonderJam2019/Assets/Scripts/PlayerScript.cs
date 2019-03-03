using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    public float PressureTolerance = 1000f;
    public float MaxDisplayPressure = 5000f;
    public TMPro.TextMeshProUGUI RoomText;

    private HudController hudController;
    private RoomScript currentRoom;    

    public RoomScript CurrentRoom
    {
        get => this.currentRoom;
        set
        {
            if (value != this.currentRoom)
            {
                this.RoomText.text = "SAS";
                this.currentRoom = value;

                if (this.currentRoom != null)
                {
                    this.RoomText.text = "Pièce " + value.RoomName;
                    this.currentRoom.OxygenConsumer.PressureChanged -= OxygenConsumer_PressureChanged;
                }
                
                this.currentRoom.OxygenConsumer.PressureChanged += OxygenConsumer_PressureChanged;
            }
        }
    }

    private void Awake()
    {
        this.hudController = FindObjectOfType<HudController>();
    }

    private void OxygenConsumer_PressureChanged(OxygenConsumer oxygenConsumer)
    {
        this.hudController.setOxygenValue((oxygenConsumer.CurrentPressure / this.MaxDisplayPressure) - this.PressureTolerance / this.MaxDisplayPressure);

        if(oxygenConsumer.CurrentPressure < this.PressureTolerance)
        {
            // TODO: Load GameOver Screen
        }
    }
}
