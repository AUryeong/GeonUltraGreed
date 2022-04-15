using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.GetComponent<UnitBase>() != null)
        {
            AttackSuccess(collision.GetComponent<UnitBase>());
        }
    }
    protected virtual void AttackSuccess(UnitBase unit)
    {
        unit.Damaged(damage);
    }

    protected virtual void EndAttack()
    {
        gameObject.SetActive(false);
    }
    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }
}
