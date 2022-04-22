using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{
    public Item item;
    protected override void AttackSuccess(UnitBase unit)
    {
        if(unit.tag == "Enemy")
        {
            base.AttackSuccess(unit);
            GameObject obj = PoolManager.Instance.Init(Resources.Load<GameObject>("FX/" + item.HitEffect));
            float f = (unit.GetComponent<BoxCollider2D>().size.x > unit.GetComponent<BoxCollider2D>().size.y) ? unit.GetComponent<BoxCollider2D>().size.y : unit.GetComponent<BoxCollider2D>().size.x;
            obj.transform.localScale = new Vector3(f, f,f);
            obj.transform.position = unit.transform.position;
            obj.transform.localRotation = gameObject.transform.localRotation;
        }
    }
}
