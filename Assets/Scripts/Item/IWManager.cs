using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IWManager : Singleton<IWManager>
{
    [Header("���콺��������")]
    [SerializeField]
    RectTransform window;
    [SerializeField]
    RectTransform window2;
    [SerializeField]
    TextMeshProUGUI itemname;
    [SerializeField]
    TextMeshProUGUI itemabt;
    [SerializeField]
    TextMeshProUGUI itemlore;
    [SerializeField]
    Image itemimage;

    [Header("ȹ��������")]
    [SerializeField]
    GameObject getwindow;
    [SerializeField]
    TextMeshProUGUI getitemname;
    [SerializeField]
    Image getitemimage;

    ItemSlot prevslot;
    Item previtem;

    public void DisableItem()
    {
        if (window.gameObject.activeSelf)
        {
            window.gameObject.SetActive(false);
        }
    }
    public void DisableItem2()
    {
        if (getwindow.gameObject.activeSelf)
        {
            getwindow.gameObject.SetActive(false);
        }
    }
    public void ShowItem(Item item)
    {
        StopCoroutine("ShowItemCorutine");
        StartCoroutine(ShowItemCorutine(item, 2));
    }

    IEnumerator ShowItemCorutine(Item item, float duration)
    {
        if (item != null)
        {
            getwindow.gameObject.SetActive(true);
            getitemname.text = ColorManager.GetRankColor(item.rank) + item.Name;
            getitemimage.sprite = Resources.Load<Sprite>("Item/" + item.ItemText);
            getitemimage.SetNativeSize();
        }
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return 0;
        }
        getwindow.gameObject.SetActive(false);
    }

    public void ShowItem(ItemSlot slot)
    {
        if(slot != null && slot.item != null)
        {
            window.gameObject.SetActive(true);
            if (prevslot == null || prevslot != slot || previtem != slot.item)
            {
                prevslot = slot;
                previtem = slot.item;
                Item item = slot.item;
                string s = ColorManager.GetRankColor(item.rank);
                itemname.text = s + item.Name;
                StatBonus stat = item.Stat.Copy();
                if (item.category == ItemSlot.Category.MainWeapon)
                {
                    itemabt.text = "���ݷ� : <color=#fffc3d>" + stat.MinDmg + " ~ " + stat.MaxDmg + "\n<color=#ffffff>�ʴ� ���� Ƚ�� : <color=#fffc3d>" + stat.AttackSpeed;
                    stat.MinDmg = 0;
                    stat.MaxDmg = 0;
                    stat.AttackSpeed = 0;
                }
                else if (item.category == ItemSlot.Category.Accessory && stat.Defense > 0)
                {
                    itemabt.text = "���� : <color=#fffc3d>" + stat.Defense;
                    stat.Defense = 0;
                }
                else
                {
                    itemabt.text = "";
                }
                itemimage.sprite = Resources.Load<Sprite>("Item/" + item.ItemText);
                itemimage.SetNativeSize();
                string lore = "<color=#c8c8c8>";
                switch (item.rank)
                {
                    case Rank.Default:
                        lore += "�Ϲ� ������\n";
                        break;
                    case Rank.Rare:
                        lore += "��� ������\n";
                        break;
                    case Rank.Unique:
                        lore += "��� ������\n";
                        break;
                    case Rank.Lengendary:
                        lore += "���� ������\n";
                        break;
                }
                switch (item.category)
                {
                    case ItemSlot.Category.MainWeapon:
                        lore += "�� ����\n";
                        break;
                    case ItemSlot.Category.SubWeapon:
                        lore += "���� ����\n";
                        break;
                    case ItemSlot.Category.Accessory:
                        lore += "�׼�����\n";
                        break;
                    default:
                        lore += "�׼�����\n";
                        break;
                }
                lore += "<color=#b1fffc>" + item.Description;
                stat.Add(item.AddStat);
                string addstat = "";
                float f = stat.MinDmg;
                if (f != 0)
                {
                    addstat += (f < 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + ColorManager.RankWhite + " �ּ� ������\n";
                }
                f = stat.MaxDmg;
                if (f != 0)
                {
                    addstat += (f < 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + ColorManager.RankWhite + " �ִ� ������\n";
                }
                f = stat.AttackSpeed;
                if (f != 0)
                {
                    addstat += (f < 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + ColorManager.RankWhite + " ���� �ӵ�\n";
                }
                f = stat.AttackSpeedPer;
                if (f != 0)
                {
                    addstat += (f < 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + "%" + ColorManager.RankWhite + " ���� �ӵ�\n";
                }
                f = stat.SpeedPer;
                if (f != 0)
                {
                    addstat += (f < 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + "%" + ColorManager.RankWhite + " �̵� �ӵ�\n";
                }
                f = stat.Power;
                if (f != 0)
                {
                    addstat += (f < 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + ColorManager.RankWhite + " ����\n";
                }
                f = stat.Defense; 
                if (f != 0)
                {
                    addstat += (f < 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + ColorManager.RankWhite + " ����\n";
                }
                f = stat.Crit;
                if (f != 0)
                {
                    addstat += (f < 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + ColorManager.RankWhite + " ũ��Ƽ��\n";
                }
                f = stat.CritDmgPer;
                if (f != 0)
                {
                    addstat += (f < 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + "%" + ColorManager.RankWhite + " ũ��Ƽ�� �����\n";
                }
                itemlore.text = addstat + lore;
                window.position = slot.GetComponent<RectTransform>().position;
                Canvas.ForceUpdateCanvases();
                window.anchoredPosition += new Vector2(window.position.x > 960 ? 0 : 700, window.position.y > 540 ? 0 : window2.rect.height);
            }
        }
    }
}
