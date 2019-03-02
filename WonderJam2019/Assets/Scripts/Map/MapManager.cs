using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.TextCore;
using TMPro;
using System.Text;

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
    private bool mapShown = true;

    private RectTransform descriptionPane;
    private MapObject selectedMapObject;
    private RectTransform mapSurface;
    private RectTransform legendPane;
    private TextMeshProUGUI networkPaneText;

    private void Start()
    {
        this.descriptionPane = FindObjectsOfType<RectTransform>().FirstOrDefault(o => o.name == DescriptionPaneName);
        if (this.descriptionPane is null) throw new System.NullReferenceException("Could not find the description pane.");
        this.descriptionPane.gameObject.SetActive(false);

        this.mapSurface = FindObjectsOfType<RectTransform>().FirstOrDefault(rt => rt.name == "Map Content");
        if (this.mapSurface is null) throw new System.NullReferenceException("Could not find the Map Content.");

        this.legendPane = FindObjectsOfType<RectTransform>().FirstOrDefault(rt => rt.name == "LegendPane");
        if (this.legendPane is null) throw new System.NullReferenceException("Could not find the Legend Pane.");

        this.networkPaneText = FindObjectsOfType<TextMeshProUGUI>().FirstOrDefault(rt => rt.name == "NetworkPaneText");
        if (this.networkPaneText is null) throw new System.NullReferenceException("Could not find the Network Pane Text.");

        // Bind events
        Singleton<PressureManager>.Instance.GeneratedGraphs += this.UpdateNetworkPane;

        // Update network pane
        this.UpdateNetworkPane(Singleton<PressureManager>.Instance.NetworkCount);
    }

    private void UpdateNetworkPane(int networkCount)
    {
        var sb = new StringBuilder();
        sb.AppendLine("<size=150%><b>Réseaux</b><size=100%>");
        if(networkCount > 0)
        {
            for(int i = 0; i < networkCount; ++i)
            {
                sb.AppendLine($"<sprite=\"icons_atlas\" index=3 color=#{ColorUtility.ToHtmlStringRGB(this.NetworkColors[i])} tint=1> Pièces appartenant au réseau {i}");
            }
        }
        else
        {
            sb.AppendLine("Aucun réseau d'oxygène! YOU DED.");
        }

        this.networkPaneText.text = sb.ToString();
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
        if (this.selectedMapObject != null && mapObject != this.selectedMapObject)
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

    private void Update()
    {
        if (this.mapShown)
        {
            float factor = 0.25f;
            if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Equals))
            {
                if (this.mapSurface.localScale.x < factor * 8)
                {
                    this.mapSurface.localScale += new Vector3(factor, factor, 0);
                    this.mapSurface.position -= new Vector3(factor * this.mapSurface.sizeDelta.x / 2, factor / 2 * -1 * this.mapSurface.sizeDelta.y, 0);
                }

            }
            else if (Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus))
            {
                if (this.mapSurface.localScale.x > factor)
                {
                    this.mapSurface.localScale -= new Vector3(factor, factor, 0);
                    this.mapSurface.position += new Vector3(factor * this.mapSurface.sizeDelta.x / 2, factor / 2 * -1 * this.mapSurface.sizeDelta.y, 0);
                }
            }

            else if(Input.GetKeyDown(KeyCode.H))
            {
                this.legendPane.gameObject.SetActive(!this.legendPane.gameObject.activeSelf);
            }
        }
    }
}