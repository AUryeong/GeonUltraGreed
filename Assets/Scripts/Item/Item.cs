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
    public int AttackSpeed;
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

    public string ItemText = "None";

    public virtual string Name
    {
        get
        {
            return "�� ��";
        }
    }

    public virtual string Description
    {
        get
        {
            return "��������";
        }
    }
}
public class MainWeaponItem : Item
{
    public virtual void Attack()
    {

    }
}

public class SubWeaponItem : Item
{

    public virtual void Attack()
    {

    }
}

public class AccessoryItem : Item
{
}
