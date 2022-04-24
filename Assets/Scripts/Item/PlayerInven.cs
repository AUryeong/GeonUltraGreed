using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
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

    [SerializeField]
    TextMeshProUGUI moneytext;

    public ItemSlot SelectedSlot;
    public List<ItemSlot> Inventories = new List<ItemSlot>();
    public List<ItemSlot> Accessories = new List<ItemSlot>();
    public List<ItemSlot> MainWeapon = new List<ItemSlot>();
    public List<ItemSlot> SubWeapon = new List<ItemSlot>();

    public Hand hand = Hand.LeftHand;

    public int money;

    public StatBonus GetStat()
    {
        StatBonus statBonus = new StatBonus();
        List<ItemSlot> slots = new List<ItemSlot>(GetHands());
        slots.AddRange(Accessories);
        foreach(ItemSlot slot in slots)
        {
            if(slot.item != null)
            {
                StatBonus stat = slot.item.Stat.Copy().Add(slot.item.AddStat);
                stat.AttackSpeed += stat.AttackSpeed * stat.AttackSpeedPer / 100;
                statBonus.Add(stat);
            }
        }
        return statBonus;
    }

    public ItemSlot[] GetHands()
    {
        return new ItemSlot[] { MainWeapon[(int)hand], SubWeapon[(int)hand] };
    }
    public List<ItemSlot> GetAllItemSlots()
    {
        List<ItemSlot> list = new List<ItemSlot>(Inventories);
        list.AddRange(Accessories);
        list.AddRange(SubWeapon);
        list.AddRange(MainWeapon);
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
        moneytext.text = GameManager.NumberComma(money);
        if (SelectedSlot != null)
        {
            SelectedSlot.itemimage.transform.position = Input.mousePosition;
        }
        if (!selectedimages[(int)hand].activeSelf)
        {
            selectedimages[((int)hand + 1) % 2].SetActive(false);
            selectedimages[(int)hand].SetActive(true);
        }
        ItemSlot slot = GetAllItemSlots().Find((ItemSlot x) => x.item != null && x.select);
        if(slot != null && slot != SelectedSlot)
        {
            IWManager.Instance.ShowItem(slot);
        }
        else
        {
            IWManager.Instance.DisableItem();
        }
    }

    public bool AddItem(Item item)
    {
        if (item != null)
        {
            List<ItemSlot> emptylist = MainWeapon.FindAll((ItemSlot x) => x.item == null);
            if (item.category == ItemSlot.Category.MainWeapon && emptylist.Count > 0)
            {
                emptylist[0].SetItem(item);
                Player.Instance.StatChange();
                return true;
            }
            emptylist = SubWeapon.FindAll((ItemSlot x) => x.item == null);
            if (item.category == ItemSlot.Category.SubWeapon && emptylist.Count > 0)
            {
                emptylist[0].SetItem(item);
                Player.Instance.StatChange();
                return true;
            }
            emptylist = Accessories.FindAll((ItemSlot x) => x.item == null);
            if (item.category == ItemSlot.Category.Accessory && emptylist.Count > 0)
            {
                emptylist[0].SetItem(item);
                Player.Instance.StatChange();
                return true;
            }
            emptylist = Inventories.FindAll((ItemSlot x) => x.item == null);
            if (emptylist.Count > 0)
            {
                emptylist[0].SetItem(item);
                Player.Instance.StatChange();
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
                Player.Instance.StatChange();
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
                Player.Instance.StatChange();
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
                Player.Instance.StatChange();
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
            copies.SetItem(null, false, true);
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
        Item shortsword = new Item()
        {
            ItemText = "ShortSword",
            category = ItemSlot.Category.MainWeapon,
            Description = "\"가볍고 휘두르기 편한 검\"",
            Name = "숏 소드",
            rank = Rank.Default,
            Stat = new StatBonus()
            {
                MinDmg = 8,
                MaxDmg = 10,
                AttackSpeed = 3.03f
            }
        };
        Item greatsword = new Item()
        {
            ItemText = "GreatSword",
            category = ItemSlot.Category.MainWeapon,
            Description = "\"크고 아름다운 소드\"",
            Name = "그레이트 소드",
            rank = Rank.Default,
            Stat = new StatBonus()
            {
                MinDmg = 18,
                MaxDmg = 20,
                AttackSpeed = 1.54f
            }
        };
        Item chainchestplate = new Item()
        {
            ItemText = "ChainArmor",
            category = ItemSlot.Category.Accessory,
            Description = "\"가볍지만, 소리가 커서 발걸음이 위축되는 갑옷\"",
            Name = "사슬갑옷",
            rank = Rank.Default,
            Stat = new StatBonus()
            {
                Defense = 11,
                SpeedPer = -5
            }
        };
        Item item2 = shortsword.Copy();
        item2.AddStat.Add(new StatBonus() { Power = 100, SpeedPer = 100, AttackSpeedPer = 100 });
        AddItem(item2);
        GameObject.Find("DropItem_ChainArmor").GetComponent<DropItem>().item = chainchestplate.Copy();
        GameObject.Find("BasicTreasure").GetComponent<ChestBase>().item = greatsword.Copy();
    }

    public enum Hand
    {
        LeftHand,
        RightHand
    }
}
