using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SwordSkel_Attack : MonoBehaviour
{
    [SerializeField]
    Enemy_SwordSkel enemy_Skel;
    void Attack()
    {
        enemy_Skel.Attack();
    }

    void EndAttack()
    {
        enemy_Skel.EndAttack();
    }
}
