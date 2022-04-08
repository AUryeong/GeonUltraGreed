using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInven : MonoBehaviour
{
    [SerializeField]
    private GameObject baseui;
    [SerializeField]
    private GameObject mainweapon1;
    [SerializeField]
    private GameObject subweapon1;
    [SerializeField]
    private GameObject mainweapon2;
    [SerializeField]
    private GameObject subweapon2;
    [SerializeField]
    private List<GameObject> slots = new List<GameObject>();
    [SerializeField]
    private List<GameObject> accesories = new List<GameObject>();

    public int money;
    public List<Item> inventories = new List<Item>(15);
    public List<AccessoryItem> accessories = new List<AccessoryItem>(4);
    public List<MainWeaponItem> weapons = new List<MainWeaponItem>(2);
    public List<SubWeaponItem> subweapons = new List<SubWeaponItem>(2);



}
