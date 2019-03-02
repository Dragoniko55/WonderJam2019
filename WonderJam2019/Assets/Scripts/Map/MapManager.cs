using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // Move to instance member once singleton
    public static string RoomsContainerName = "Rooms";
    public static string DoorsContainerName = "Doors";
    public static string ValvesContainerName = "Valves";
    public static string OxygenContainerName = "Oxygen";

    public static string MasterCanvasName = "Map Content"; // TODO
}
