using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{

    public int MaxHp;

    public int Hp;
    protected virtual void Start()
    {
        Hp = MaxHp;
    }
    public virtual void Damaged(int damage)
    {
        Hp -= damage;
    }

    
   protected virtual void Update()
    {
        
    }
}
