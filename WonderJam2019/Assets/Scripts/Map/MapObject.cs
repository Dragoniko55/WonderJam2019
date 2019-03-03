using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public abstract class MapObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    protected Image displayImage;
    protected GameObject rootMap;
    protected string objectDescription;
    protected bool selected = false;

    [SerializeField] Texture2D cursorTexture;

    /// <summary>
    /// Init component
    /// </summary>
    protected virtual void Awake()
    {
        this.displayImage = this.GetComponent<Image>();
        this.rootMap = FindObjectsOfType<RectTransform>().FirstOrDefault(c => c.name == Singleton<MapManager>.Instance.MasterCanvasName)?.gameObject;
        if (this.rootMap is null) throw new System.NullReferenceException("Could not locate master canvas.");

        // TODO: Check if the name is really unique

        // TODO: Register in MapManager, need singleton class
    }

    protected virtual void Start()
    {
        // Self render on Start.
        this.Render();
        this.GenerateDescription();
    }

    /// <summary>
    /// Render method custom to the map component
    /// </summary>
    public abstract void Render();

    protected abstract void GenerateDescription();


    public virtual void OnSelected()
    {
        this.selected = true;
    }

    public virtual void OnUnselected()
    {
        this.selected = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!this.selected)
        {
            Singleton<MapManager>.Instance.Select(this);
            this.OnSelected();
        }
        else
        {
            Singleton<MapManager>.Instance.Select(null);
            this.OnUnselected();
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Singleton<MapManager>.Instance.HideDescription();

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Singleton<MapManager>.Instance.ShowDescription(this.objectDescription);

        Cursor.SetCursor(this.cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}
