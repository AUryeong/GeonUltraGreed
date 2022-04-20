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
    public virtual void Damaged(float damage)
    {
        Hp -= damage;
    }

    
   protected virtual void Update()
    {
        
    }
}
