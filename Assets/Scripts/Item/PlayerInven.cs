using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInven : MonoBehaviour
{
    [SerializeField]
    private ItemSlot copy;

    [SerializeField]
    GameObject[] selectedimages;

    [SerializeField]
    GameObject[] gameObjects;

    [SerializeField]
    GameObject[] handsselect;

    public ItemSlot SelectedSlot;
    public List<ItemSlot> Inventories = new List<ItemSlot>();
    public List<ItemSlot> Accessories = new List<ItemSlot>();
    public List<ItemSlot> MainWeapon = new List<ItemSlot>();
    public List<ItemSlot> SubWeapon = new List<ItemSlot>();

    public Hand hand = Hand.LeftHand;

    public StatBonus GetStat()
    {
        StatBonus statBonus = new StatBonus();
        List<ItemSlot> slots = new List<ItemSlot>(Accessories);
        slots.AddRange(GetHands());
        foreach(ItemSlot slot in slots)
        {
            if(slot.item != null)
            {
                statBonus.Add(slot.item.GetStat());
            }
        }
        return statBonus;
    }

    public ItemSlot[] GetHands()
    {
        return new ItemSlot[] { MainWeapon[(int)hand] , SubWeapon[(int)hand] };
    }
    public List<ItemSlot> GetAllItemSlots()
    {
        List<ItemSlot> list = new List<ItemSlot>(Inventories);
        list.AddRange(Accessories);
        list.AddRange(MainWeapon);
        list.AddRange(SubWeapon);
        return list;
    }
    public void InventoryClose()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            gameObject.SetActive(false);
        }
        if (SelectedSlot != null)
        {
            SelectedSlot.itemimage.transform.position = Input.mousePosition;
        }
        if (!selectedimages[(int)hand].activeSelf)
        {
            selectedimages[((int)hand + 1) % 2].SetActive(false);
            selectedimages[(int)hand].SetActive(true);
        }
    }

    public bool AddItem(Item item)
    {
        if (item != null)
        {
            List<ItemSlot> emptylist = Inventories.FindAll((ItemSlot x) => x.item == null);
            if (emptylist.Count > 0)
            {
                emptylist[0].SetItem(item);
                return true;
            }
            return false;
        }
        return true;
    }
    public bool AddItem(ItemSlot item, ItemSlot order = null)
    {
        if(item != null)
        {
            List<ItemSlot> emptylist = Inventories.FindAll((ItemSlot x) => x.item == null || item == x || (order != null && order == x));
            if (emptylist.Count > 0)
            {
                emptylist[0].SetItem(item.item);
                return true;
            }
            return false;
        }
        return true;
    }

    public bool AddItem(List<Item> itemlist)
    {
        if (itemlist != null && itemlist.Count > 0)
        {
            List<ItemSlot> emptylist = Inventories.FindAll((ItemSlot x) => x.item == null);
            if (emptylist.Count >= itemlist.Count)
            {
                int i2 = 0;
                for (int i = 0; i < itemlist.Count; i++)
                {
                    if (itemlist[i] == null)
                    {
                        emptylist[i2].SetItem(itemlist[i]);
                    }
                    else
                    {
                        emptylist[i2].SetItem(itemlist[i]);
                        i2++;
                    }
                }
                return true;
            }
            return false;
        }
        return true;
    }
    public bool AddItem(List<ItemSlot> itemlist)
    {
        if (itemlist != null && itemlist.Count > 0)
        {
            List<ItemSlot> emptylist = Inventories.FindAll((ItemSlot x) => x.item == null || itemlist.Contains(x));
            if (emptylist.Count >= itemlist.Count)
            {
                int i2 = 0;
                for(int i = 0; i < itemlist.Count; i++)
                {
                    if(itemlist[i].item == null)
                    {
                        emptylist[i2].SetItem(itemlist[i].item);
                    }
                    else
                    {
                        emptylist[i2].SetItem(itemlist[i].item);
                        i2++;
                    }
                }
                return true;
            }
            return false;
        }
        return true;
    }

    public void Init()
    {
        Inventories.Add(copy);
        foreach (GameObject obj in gameObjects)
        {
            ItemSlot copies = GameObject.Instantiate<ItemSlot>(copy, obj.transform);
            copies.transform.localPosition = Vector3.zero;
            copies.SetItem(null);
            string s = copies.transform.parent.gameObject.name;
            copies.name = s;
            if (s.Contains("Inven"))
            {
                Inventories.Add(copies);
                copies.category = ItemSlot.Category.Inventory;
            }
            else if (s.Contains("Main Weapon"))
            {
                MainWeapon.Add(copies);
                copies.category = ItemSlot.Category.MainWeapon;
            }
            else if (s.Contains("Sub Weapon"))
            {
                SubWeapon.Add(copies);
                copies.category = ItemSlot.Category.SubWeapon;
            }
            else if (s.Contains("Accessory"))
            {
                Accessories.Add(copies);
                copies.category = ItemSlot.Category.Accessory;
            }
        }
        AddItem(new ShortSword());
    }

    public enum Hand
    {
        LeftHand,
        RightHand
    }
}
