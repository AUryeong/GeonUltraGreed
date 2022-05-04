using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBase : FButtonUnitBase
{
    [SerializeField]
    Sprite opensprite;
    [SerializeField]
    int money;
    [SerializeField]
    int randommoney;
    protected bool open = false;
    public Item item;
    public override void OnF()
    {
        if (!open)
        {
            open = true;
            GetComponent<SpriteRenderer>().sprite = opensprite;
            GameObject obj = PoolManager.Instance.Init(Resources.Load<GameObject>("DropItem/DropItem"));
            obj.GetComponent<DropItem>().ShowItem(item);
            obj.GetComponent<DropItem>().getitem = false;
            obj.transform.position = transform.position;
            obj.name = "DropItem_" + item.ItemText;
            obj.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 400);
            int getmoney = money + Random.Range(0, randommoney + 1);
            int billion = getmoney / 100;
            int coin = (getmoney % 100) / 10;
            for(int i = 0; i < billion; i++)
            {
                GameObject billionobj = PoolManager.Instance.Init(Resources.Load<GameObject>("DropItem/DropBullion")); 
                Idong(billionobj);

            }
            for (int i = 0; i < coin; i++)
            {
                GameObject coinobj = PoolManager.Instance.Init(Resources.Load<GameObject>("DropItem/DropGold"));
                Idong(coinobj);
            }
        }
    }

    void Idong(GameObject obj)
    {
        obj.transform.position = transform.position + new Vector3(0, 0.1f, 0);
        obj.GetComponent<DropGold>().getgold = false;
        obj.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * 800);
    }
    
    protected override bool CanShowF()
    {
        return !open;
    }
}
