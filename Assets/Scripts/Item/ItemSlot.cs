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

    private PlayerInven Inven
    {
        get { return Player.Instance.Inven; }
    }

    public override void OnPointerClick(PointerEventData data)
    {
        //우클릭 시 자동 장착
        if(data.button == PointerEventData.InputButton.Right && item != null)
        {
            ItemSlot slot = null;
            if(item.category == Category.MainWeapon)
            {
                if (Inven.MainWeapon.Contains(this))
                {
                    if (Inven.AddItem(this))
                    {
                        SetItem(null);
                    }
                    return;
                }
                else
                {
                    slot = Inven.GetHands()[0];
                }
            }
            else if (item.category == Category.SubWeapon)
            {

                if (Inven.SubWeapon.Contains(this))
                {
                    if (Inven.AddItem(this))
                    {
                        SetItem(null);
                    }
                    return;
                }
                else if (Inven.GetHands()[0].item == null)
                {
                    return;
                }
                else
                {
                    slot = Inven.GetHands()[1];
                }
            }
            else if (item.category == Category.Accessory)
            {
                if (Inven.Accessories.Contains(this))
                {
                    if (Inven.AddItem(this))
                    {
                        SetItem(null);
                    }
                    return;
                }
                else
                {
                    slot = Inven.Accessories.Find((ItemSlot x) => x.item == null);
                    if (slot == null)
                    {
                        slot = Inven.Accessories[0];
                    }
                }
            }
            Item ATM = item;
            if (Inven.AddItem(slot, this))
            {
                slot.SetItem(ATM);
                if (ATM == item)
                {
                    SetItem(null);
                }
            }
        }
    }

    public override void OnPointerDown(PointerEventData data)
    {
        if(data.button == PointerEventData.InputButton.Left)
        {
            if (Inven.SelectedSlot == null && item != null)
            {
                gameObject.transform.parent.SetAsLastSibling();
                Inven.SelectedSlot = this;
            }
        }
    }

    public bool SetableItem(Item item)
    {
        return (item == null || category == Category.Inventory || category == item.category);
    }
    public override void OnPointerUp(PointerEventData data)
    {
        if (data.button == PointerEventData.InputButton.Left)
        {
            if (Inven.SelectedSlot == this)
            {
                Inven.SelectedSlot = null;
                ItemSlot slot = Inven.GetAllItemSlots().Find((ItemSlot x) => x.select);
                itemimage.transform.localPosition = Vector3.zero;
                if (slot != null)
                {
                    Item item2 = slot.item;
                    Item ATM = item;
                    if (slot.SetableItem(ATM) && SetableItem(item2))
                    {
                        slot.SetItem(ATM);
                        SetItem(item2);
                    }
                    else if (ATM.category == Category.MainWeapon && slot.category == Category.SubWeapon && Inven.AddItem(Inven.MainWeapon[Inven.SubWeapon.IndexOf(slot)], this))
                    {
                        Inven.MainWeapon[Inven.SubWeapon.IndexOf(slot)].SetItem(ATM);
                        if (ATM == item)
                        {
                            SetItem(null);
                        }
                    }
                    else if (ATM.category == Category.SubWeapon && slot.category == Category.MainWeapon && Inven.AddItem(Inven.SubWeapon[Inven.MainWeapon.IndexOf(slot)], this))
                    {
                        Inven.SubWeapon[Inven.MainWeapon.IndexOf(slot)].SetItem(ATM);
                        if (ATM == item)
                        {
                            SetItem(null);
                        }
                    }
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
        Player.Instance.StatChange();
        this.item = item;
    }
}