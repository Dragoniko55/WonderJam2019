﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public abstract class MapObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected Image displayImage;
    protected GameObject rootMap;
    protected string objectDescription;

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

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Singleton<MapManager>.Instance.HideDescription();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Singleton<MapManager>.Instance.ShowDescription(this.objectDescription);
    }
}