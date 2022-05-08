using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class UISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField]
    Sprite selected;

    Sprite gibon;

    Image image;

    public bool select = false;


    protected virtual void Awake()
    {
        if(gibon == null)
        {
            gibon = GetComponent<Image>().sprite;
        }
        if(image == null)
        {
            image = GetComponent<Image>();
        }
    }

    public virtual void OnPointerUp(PointerEventData data)
    {
    }

    public virtual void OnPointerDown(PointerEventData data)
    {
    }

    public virtual void OnPointerClick(PointerEventData data)
    {
    }

    protected virtual void OnDisable()
    {
        image.sprite = gibon;
    }

    public virtual void OnPointerEnter(PointerEventData data)
    {
        image.sprite = selected;
        select = true;
    }
    public virtual void OnPointerExit(PointerEventData data)
    {
        image.sprite = gibon;
        select = false;
    }
}
