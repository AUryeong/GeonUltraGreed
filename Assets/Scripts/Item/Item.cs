using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rank
{
    Default,
    Rare,
    Unique,
    Lengendary
}

public class StatBonus
{
    public int MinDmg;
    public int MaxDmg;
    public int Defense;
    public float AttackSpeed;
    public int Power;
    public int Crit;
    public int CritDmg;
}

public class Item
{
    public virtual StatBonus GetStat()
    {
        return new StatBonus()
        {
            MinDmg = 0,
            MaxDmg = 0,
            Defense = 0,
            AttackSpeed = 0,
            Power = 0,
            Crit = 0,
            CritDmg = 0
        };
    }

    public virtual ItemSlot.Category category
    {
        get
        {
            return ItemSlot.Category.Inventory;
        }
    }

    public virtual string ItemText
    {
        get
        {
            return "그 없";
        }
    }

    public virtual string Name
    {
        get
        {
            return "그 없";
        }
    }

    public virtual string Description
    {
        get
        {
            return "에러에러";
        }
    }
}

public class ShortSword : Item
{
    public override StatBonus GetStat()
    {
        return new StatBonus()
        {
            MinDmg = 8,
            MaxDmg = 10,
            Defense = 0,
            AttackSpeed = 3.03f,
            Power = 0,
            Crit = 0,
            CritDmg = 0
        };
    }

    public override ItemSlot.Category category
    {
        get
        {
            return ItemSlot.Category.MainWeapon;
        }
    }

    public override string ItemText
    {
        get
        {
            return "ShortSword";
        }
    }

    public override string Name
    {
        get
        {
            return "숏 소드";
        }
    }

    public override string Description
    {
        get
        {
            return "가볍고 주무르기 편한 검";
        }
    }
}