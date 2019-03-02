using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UnityEngine.UI.Image))]
[RequireComponent(typeof(RectTransform))]
public abstract class MapObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image displayImage;
    //public Vector2 position;
    public GameObject rootMap;
    protected string objectDescription;

    /// <summary>
    /// Init component
    /// </summary>
    protected virtual void Awake()
    {
        this.displayImage = this.GetComponent<Image>();
        //this.position = this.GetComponent<RectTransform>().position; // Implicit conversion to vector 2d
        this.rootMap = FindObjectsOfType<RectTransform>().FirstOrDefault(c => c.name == MapManager.MasterCanvasName)?.gameObject;
        if (this.rootMap is null) throw new System.NullReferenceException("Could not locate master canvas.");

        // TODO: Check if the name is really unique

        // TODO: Register in MapManager, need singleton class
    }

    protected void ShowDescription(PointerEventData eventData)
    {

    }

    public abstract void Render();
    public abstract void OnPointerEnter(PointerEventData eventData);
    public abstract void OnPointerExit(PointerEventData eventData);
    public abstract void OnPointerClick(PointerEventData eventData);
}
