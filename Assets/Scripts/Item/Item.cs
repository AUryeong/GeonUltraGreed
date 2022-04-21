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
    public float DashDmgPer = 0;
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
        DashDmgPer += bonus.DashDmgPer;
        return this;
    }
    public StatBonus Copy()
    {
        return new StatBonus()
        {
            Power = this.Power,
            Defense = this.Defense,
            Crit = this.Crit,
            MinDmg = this.MinDmg,
            MaxDmg = this.MaxDmg,
            CritDmgPer = this.CritDmgPer,
            AttackSpeed = this.AttackSpeed,
            AttackSpeedPer = this.AttackSpeedPer,
            SpeedPer = this.SpeedPer,
            DashDmgPer = this.DashDmgPer
        };
    }
}

public class Item
{
    public StatBonus Stat = new StatBonus();

    public StatBonus AddStat = new StatBonus();

    public ItemSlot.Category category = ItemSlot.Category.Inventory;

    public Rank rank = Rank.Default;

    public string ItemText = "그 없";

    public string Name = "그 없";

    public string Description = "에러에러";

    public Item Copy()
    {
        return new Item()
        {
            Stat = this.Stat.Copy(),
            category = this.category,
            rank = this.rank,
            ItemText = this.ItemText,
            Name = this.Name,
            Description = this.Description,
            AddStat = this.AddStat.Copy(),
        };
    }
}
