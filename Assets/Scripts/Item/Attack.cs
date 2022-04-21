using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    public List<GameObject> targets = new List<GameObject>();
    
    protected virtual void OnEnable()
    {
        targets.Clear();
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.GetComponent<UnitBase>() != null && !targets.Contains(collision.gameObject))
        {
            targets.Add(collision.gameObject);
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
