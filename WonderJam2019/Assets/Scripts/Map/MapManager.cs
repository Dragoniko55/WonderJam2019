using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // TODO: Move to instance member once singleton
    public static string RoomsContainerName = "Rooms";
    public static string DoorsContainerName = "Doors";
    public static string ValvesContainerName = "Valves";
    public static string OxygenContainerName = "Oxygen";
    public static string DescriptionPaneName = "DescriptionPane";

    public static string MasterCanvasName = "Map Content"; // TODO

    private bool showDescription = true;
    private GameObject descriptionPane;
    private Vector2 descriptionPaneOffset = new Vector2(200, 200);

    private void Start()
    {
        this.descriptionPane = GameObject.Find(DescriptionPaneName);
        if (this.descriptionPane is null) throw new System.NullReferenceException("Could not find the description pane.");
    }

    private void FixedUpdate()
    {
        if (this.showDescription)
        {
            Vector2 pos = Input.mousePosition;
            this.descriptionPane.transform.position = pos - this.descriptionPaneOffset;
            Debug.Log(pos);
        }        
    }

}