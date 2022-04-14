using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.GetComponent<UnitBase>() != null)
        {
            AttackSuccess();
            collision.GetComponent<UnitBase>().Damaged(damage);
        }
    }
    protected virtual void AttackSuccess()
    {

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
