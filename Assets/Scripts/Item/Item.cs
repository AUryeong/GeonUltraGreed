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

public enum WeaponAttackType
{
    Sword,
    Bullet
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
    public float Strong = 0;
    public float Blocking = 0;
    public float Evade = 0;
    public float Speed = 0;
    public float ReloadSpeed = 0;
    public float ReloadSpeedPer = 0;
    public float FixedDamage = 0;
    public float GoldBonusPer = 0;
    public StatBonus Add(StatBonus bonus)
    {
        Power += bonus.Power;
        Defense += bonus.Defense;
        Crit += bonus.Crit;
        MinDmg += bonus.MinDmg;
        MaxDmg += bonus.MaxDmg;
        CritDmgPer += bonus.CritDmgPer;
        AttackSpeed += bonus.AttackSpeed;
        AttackSpeedPer += bonus.AttackSpeedPer;
        SpeedPer += bonus.SpeedPer;
        DashDmgPer += bonus.DashDmgPer;
        Strong += bonus.Strong;
        Blocking += bonus.Blocking;
        Evade += bonus.Evade;
        Speed += bonus.Speed;
        ReloadSpeed += bonus.ReloadSpeed;
        ReloadSpeedPer += bonus.ReloadSpeedPer;
        FixedDamage += bonus.FixedDamage;
        GoldBonusPer += bonus.GoldBonusPer;
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
            DashDmgPer = this.DashDmgPer,
            Strong = this.Strong,
            Blocking = this.Blocking,
            Evade = this.Evade,
            Speed = this.Speed,
            ReloadSpeed = this.ReloadSpeed,
            ReloadSpeedPer = this.ReloadSpeedPer,
            FixedDamage = this.FixedDamage,
            GoldBonusPer = this.GoldBonusPer
        };
    }
}

public class Item
{
    public StatBonus Stat = new StatBonus();

    public StatBonus AddStat = new StatBonus();

    public ItemSlot.Category category = ItemSlot.Category.Inventory;

    public Rank rank = Rank.Default;

    public WeaponAttackType AttackType = WeaponAttackType.Sword;

    public string ItemText = "그 없";

    public string Name = "그 없";

    public string Description = "에러에러";

    public string AttackEffect = "ShortSword";

    public string HitEffect = "SlashFX";

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
            AttackType = this.AttackType,
            AttackEffect = this.AttackEffect,
            HitEffect = this.HitEffect
        };
    }
}
