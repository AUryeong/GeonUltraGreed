using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IWManager : Singleton<IWManager>
{
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

    ItemSlot prevslot;

    public void DisableItem()
    {
        if (window.gameObject.activeSelf)
        {
            window.gameObject.SetActive(false);
        }
    }

    public void ShowItem(ItemSlot slot)
    {
        if(slot != null && slot.item != null)
        {
            window.gameObject.SetActive(true);
            if (prevslot == null || prevslot != slot)
            {
                prevslot = slot;
                Item item = slot.item;
                string s = "";
                switch (item.rank)
                {
                    case Rank.Default:
                        s = ColorManager.RankWhite;
                        break;
                    case Rank.Rare:
                        s = ColorManager.RankBlue;
                        break;
                    case Rank.Unique:
                        s = ColorManager.RankYellow;
                        break;
                    case Rank.Lengendary:
                        s = ColorManager.RankPink;
                        break;
                }
                itemname.text = s + item.Name;
                StatBonus stat = item.GetStat();
                if (item.category == ItemSlot.Category.MainWeapon)
                {
                    itemabt.text = "���ݷ� : <color=#fffc3d>" + stat.MinDmg + " ~ " + stat.MaxDmg + "\n<color=#ffffff>�ʴ� ���� Ƚ�� : <color=#fffc3d>" + stat.AttackSpeed;
                    stat.MinDmg = 0;
                    stat.MaxDmg = 0;
                    stat.AttackSpeed = 0;
                }
                itemimage.sprite = Resources.Load<Sprite>("Item/" + item.ItemText);
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
                    addstat += (f > 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + ColorManager.RankWhite + " �ּ� ������\n";
                }
                f = stat.MaxDmg;
                if (f != 0)
                {
                    addstat += (f > 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + ColorManager.RankWhite + " �ִ� ������\n";
                }
                f = stat.AttackSpeed;
                if (f != 0)
                {
                    addstat += (f > 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + ColorManager.RankWhite + " ���� �ӵ�\n";
                }
                f = stat.AttackSpeedPer;
                if (f != 0)
                {
                    addstat += (f > 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + ColorManager.RankWhite + "%% ���� �ӵ�\n";
                }
                f = stat.SpeedPer;
                if (f != 0)
                {
                    addstat += (f > 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + ColorManager.RankWhite + "%% �̵� �ӵ�\n";
                }
                f = stat.Power;
                if (f != 0)
                {
                    addstat += (f > 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + ColorManager.RankWhite + " ����\n";
                }
                f = stat.Defense; 
                if (f != 0)
                {
                    addstat += (f > 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + ColorManager.RankWhite + " ����\n";
                }
                f = stat.Crit;
                if (f != 0)
                {
                    addstat += (f > 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + ColorManager.RankWhite + " ũ��Ƽ��\n";
                }
                f = stat.CritDmgPer;
                if (f != 0)
                {
                    addstat += (f > 0 ? ColorManager.BufJupduRed : ColorManager.BufJupduGreen) + f + ColorManager.RankWhite + "%% ũ��Ƽ�� �����\n";
                }
                itemlore.text = addstat + lore;
                window.position = slot.GetComponent<RectTransform>().position;
                window.anchoredPosition += new Vector2(window.position.x > 960 ? 0 : 700, window.position.y > 540 ? 0 : window2.rect.height);
            }
        }
    }
}
