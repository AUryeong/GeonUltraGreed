using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    protected Sprite selected;

    Sprite gibon;

    void Awake()
    {
        if(gibon == null)
        {
            gibon = GetComponent<Image>().sprite;
        }
    }

    public virtual void OnPointerEnter(PointerEventData data)
    {
        GetComponent<Image>().sprite = selected;
    }
    public virtual void OnPointerExit(PointerEventData data)
    {
        GetComponent<Image>().sprite = gibon;
    }
}
