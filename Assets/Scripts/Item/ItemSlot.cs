using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class ItemSlot : UISlot
{
    public enum Category
    {
        Inventory,
        MainWeapon,
        SubWeapon,
        Accessory
    }
    public Image itemimage;

    [SerializeField]
    Sprite whitesprite;

    [SerializeField]
    Sprite defaultsprite;

    public Item item = null;

    public bool select = false;

    public Category category = Category.Inventory;

    public override void OnPointerDown(PointerEventData data)
    {
        if(Player.Instance.Inven.SelectedSlot == null && item != null)
        {
            gameObject.transform.parent.SetAsLastSibling();
            Player.Instance.Inven.SelectedSlot = this;
        }
    }

    public bool SetableItem(Item item)
    {
        return (item == null || category == Category.Inventory || category == item.category);
    }
    public override void OnPointerUp(PointerEventData data)
    {
        if (Player.Instance.Inven.SelectedSlot == this)
        {
            Player.Instance.Inven.SelectedSlot = null;
            ItemSlot slot = Player.Instance.Inven.GetAllItemSlots().Find((ItemSlot x) => x.select);
            itemimage.transform.localPosition = Vector3.zero;
            if (slot != null)
            {
                Item item2 = slot.item;
                if (slot.SetableItem(item))
                {
                    if (SetableItem(item2))
                    {
                        slot.SetItem(item);
                        SetItem(item2);
                    }
                }
                else if (item.category == Category.MainWeapon && slot.category == Category.SubWeapon)
                {
                    slot.SetItem(item);
                    SetItem(item2);
                }
                else if (item2 != null && Player.Instance.Inven.AddItem(new List<ItemSlot>() { slot , this }))
                {
                    slot.SetItem(null);
                    SetItem(null);
                }
                else
                {

                }
            }
        }
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        select = false;
    }
    public override void OnPointerEnter(PointerEventData data)
    {
        base.OnPointerEnter(data);
        if (category == Category.Inventory)
        {
            transform.GetComponentInParent<Image>().sprite = whitesprite;
        }
        select = true;
    }
    public override void OnPointerExit(PointerEventData data)
    {
        base.OnPointerExit(data);
        if (category == Category.Inventory)
        {
            transform.GetComponentInParent<Image>().sprite = defaultsprite;
        }
        select = false;
    }
    public virtual void SetItem(Item item, bool X = false)
    {
        if(item != null)
        {
            if (this.item == null || this.item.ItemText != item.ItemText)
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
        this.item = item;
    }
}