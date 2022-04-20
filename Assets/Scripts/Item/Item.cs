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
    public float MinDmg = 0;
    public float MaxDmg = 0;
    public float Defense = 0;
    public float AttackSpeed = 0;
    public float AttackSpeedPer = 0;
    public int Power = 0;
    public float Crit = 0;
    public float CritDmgPer = 0;
    public float SpeedPer = 0;
    public StatBonus Add(StatBonus bonus)
    {
        this.Power += bonus.Power;
        this.Defense += bonus.Defense;
        this.Crit += bonus.Crit;
        this.MinDmg += bonus.MinDmg;
        this.MaxDmg += bonus.MaxDmg;
        this.CritDmgPer += bonus.CritDmgPer;
        this.AttackSpeed += bonus.AttackSpeed;
        this.AttackSpeedPer += bonus.AttackSpeedPer;
        this.SpeedPer += bonus.SpeedPer;
        return this;
    }
}

public class Item
{
    public virtual StatBonus GetStat()
    {
        return new StatBonus();
    }

    public StatBonus AddStat = new StatBonus();

    public virtual ItemSlot.Category category
    {
        get
        {
            return ItemSlot.Category.Inventory;
        }
    }

    public virtual Rank rank
    {
        get
        {
            return Rank.Default;
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
            AttackSpeed = 3.03f
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
            return "\"가볍고 휘두르기 편한 검\"";
        }
    }
}