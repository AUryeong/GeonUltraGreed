using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : UISlot
{
    [SerializeField]
    Image itemimage;

    [SerializeField]
    Sprite whitesprite;

    [SerializeField]
    Sprite defaultsprite;

    Item showitem = null;


    public bool isinven = false;
    public override void OnPointerEnter(PointerEventData data)
    {
        base.OnPointerEnter(data);
        if (isinven)
        {
            transform.GetComponentInParent<Image>().sprite = whitesprite;
        }
    }
    public override void OnPointerExit(PointerEventData data)
    {
        base.OnPointerExit(data);
        if (isinven)
        {
            transform.GetComponentInParent<Image>().sprite = defaultsprite;
        }
    }
    public virtual void ShowItem(Item item, bool X = false)
    {
        if(item != null)
        {
            if (showitem == null || showitem.ItemText != item.ItemText)
            {
                itemimage.sprite = Resources.Load<Sprite>("Item/" + item.ItemText);
            }
        }
        else
        {
            if(X == true)
            {
                itemimage.sprite = Resources.Load<Sprite>("Item/X");
            }
            else
            {
                itemimage.sprite = Resources.Load<Sprite>("Item/None");
            }
        }
        itemimage.SetNativeSize();
        showitem = item;
    }
}