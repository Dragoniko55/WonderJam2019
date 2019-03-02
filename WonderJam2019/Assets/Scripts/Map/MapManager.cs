using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.TextCore;
using TMPro;

public class MapManager : MonoBehaviour
{
    // TODO: Move to instance member once singleton
    public readonly string RoomsContainerName = "Rooms";
    public readonly string DoorsContainerName = "Doors";
    public readonly string ValvesContainerName = "Valves";
    public readonly string OxygenContainerName = "Oxygen";
    public readonly string DescriptionPaneName = "DescriptionPane";
    public readonly string MasterCanvasName = "Map Content"; // TODO

    /// <summary>
    /// We have a maximum of 7 networks
    /// </summary>
    public readonly Color[] NetworkColors = new Color[] 
    {
        Color.blue,
        Color.cyan,
        Color.gray,
        Color.green,
        Color.magenta,
        Color.red,
        Color.yellow,
        Color.white
    };

    private bool showDescription = false;
    private RectTransform descriptionPane;
    private MapObject selectedMapObject;

    private void Start()
    {
        this.descriptionPane = FindObjectsOfType<RectTransform>().FirstOrDefault(o => o.name == DescriptionPaneName);
        if (this.descriptionPane is null) throw new System.NullReferenceException("Could not find the description pane.");
        this.descriptionPane.gameObject.SetActive(false);
    }

    public void ShowDescription(string description)
    {
        this.descriptionPane.GetComponentInChildren<TextMeshProUGUI>().text = description;
        this.showDescription = true;
        this.descriptionPane.gameObject.SetActive(true);
    }

    public void HideDescription()
    {
        this.descriptionPane.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        this.showDescription = false;
        this.descriptionPane.gameObject.SetActive(false);
    }

    public void Select(MapObject mapObject)
    {
        if(this.selectedMapObject != null && mapObject != this.selectedMapObject)
        {
            this.selectedMapObject.OnUnselected();
        }

        this.selectedMapObject = mapObject;
    }

    private void FixedUpdate()
    {
        if (this.showDescription)
        {
            this.descriptionPane.transform.position = (Vector2)Input.mousePosition - (this.descriptionPane.sizeDelta);
        }        
    }
}