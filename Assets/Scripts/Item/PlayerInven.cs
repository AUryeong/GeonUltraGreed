using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInven : MonoBehaviour
{
    [SerializeField]
    private ItemSlot copy;

    [SerializeField]
    GameObject[] gameObjects;

    [SerializeField]
    GameObject[] handsselect;

    public ItemSlot SelectedSlot;
    public List<ItemSlot> Inventories = new List<ItemSlot>();
    public List<ItemSlot> Accessories = new List<ItemSlot>();
    public List<ItemSlot> MainWeapon = new List<ItemSlot>();
    public List<ItemSlot> SubWeapon = new List<ItemSlot>();
    

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
    }

    public bool AddItem(ItemSlot item)
    {
        if(item != null)
        {
            List<ItemSlot> emptylist = Inventories.FindAll((ItemSlot x) => x.item == null && x != item);
            if (emptylist.Count > 0)
            {
                emptylist[0].SetItem(item.item);
                return true;
            }
            
        }
        return false;
    }

    public bool AddItem(List<ItemSlot> itemlist)
    {
        if (itemlist != null && itemlist.Count > 0)
        {
            List<ItemSlot> emptylist = Inventories.FindAll((ItemSlot x) => x.item == null && !itemlist.Contains(x));
            if (emptylist.Count >= itemlist.Count)
            {
                for(int i = 0; i < itemlist.Count; i++)
                {
                    emptylist[i].SetItem(itemlist[i].item);
                }
                return true;
            }

        }
        return false;
    }

    private void Awake()
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
        copy.SetItem(new Item() { ItemText = "ShortSword", category = ItemSlot.Category.MainWeapon });
    }

}
