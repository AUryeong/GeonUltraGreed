using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{

    public int MaxHp;

    public float Hp;
    protected virtual void Start()
    {
        Hp = MaxHp;
    }
    public virtual bool Damaged(float damage)
    {
        Hp -= damage;
        return true;
    }

   protected virtual void Update()
    {

    }

    protected virtual void OnDie()
    {

    }
}
