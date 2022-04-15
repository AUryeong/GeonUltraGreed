using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{
    protected override void AttackSuccess(UnitBase unit)
    {
        if(unit.tag == "Enemy")
        {
            base.AttackSuccess(unit);
        }
    }
}
