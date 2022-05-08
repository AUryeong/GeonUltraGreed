using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

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
    [XmlAttribute]
    public float MinDmg = 0;

    [XmlAttribute]
    public float MaxDmg = 0;

    [XmlAttribute]
    public float Defense = 0;

    [XmlAttribute]
    public float AttackSpeed = 0;

    [XmlAttribute]
    public float AttackSpeedPer = 0;

    [XmlAttribute]
    public int Power = 0;

    [XmlAttribute]
    public float Crit = 0;

    [XmlAttribute]
    public float CritDmgPer = 0;

    [XmlAttribute]
    public float SpeedPer = 0;

    [XmlAttribute]
    public float DashDmgPer = 0;

    [XmlAttribute]
    public float Strong = 0;

    [XmlAttribute]
    public float Blocking = 0;

    [XmlAttribute]
    public float Evade = 0;

    [XmlAttribute]
    public float Speed = 0;

    [XmlAttribute]
    public float ReloadSpeed = 0;

    [XmlAttribute]
    public float ReloadSpeedPer = 0;

    [XmlAttribute]
    public float FixedDamage = 0;

    [XmlAttribute]
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
    [XmlElement("Category")]
    public ItemSlot.Category category = ItemSlot.Category.MainWeapon;

    [XmlElement]
    public StatBonus Stat = new StatBonus();

    [XmlElement("Rank")]
    public Rank rank = Rank.Default;

    [XmlElement]
    public WeaponAttackType AttackType = WeaponAttackType.Sword;

    [XmlAttribute("ID")]
    public string ItemText = "그 없";

    [XmlElement]
    public string Name = "그 없";

    [XmlElement("Lore")]
    public string Description = "에러에러";

    [XmlElement]
    public string HitEffect = "SlashFX";

    [XmlIgnore]
    public StatBonus AddStat = new StatBonus();

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
            HitEffect = this.HitEffect
        };
    }
}
