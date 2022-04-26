using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enmy_BowSkel : EnemyBase
{
    [SerializeField]
    Animator bow;

    float angle;

    
    float charging = 0;
    protected override void CheckMove(float deltaTime)
    {
    }
    protected override void CheckDirection(float deltaTime)
    {
        if(charging < 2 && charging > 0)
        {
            base.CheckDirection(deltaTime);
            if (left)
            {
                bow.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                bow.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        if (agro)
        {
            charging += Time.deltaTime;
            if (charging < 2)
            {
                if(charging > 0)
                {
                    Vector3 player = Player.Instance.transform.position;
                    angle = Mathf.Atan2(player.y - transform.position.y, player.x - transform.position.x) * Mathf.Rad2Deg;
                    bow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                }
            }
            else if(charging < 4)
            {
                bow.SetBool("isCharging", true);
            }
            else
            {
                bow.SetTrigger("Shoot");
                bow.SetBool("isCharging", false);
                GameObject obj = PoolManager.Instance.Init(Resources.Load<GameObject>("Swing/Arrow"));
                obj.transform.position = bow.transform.position;
                obj.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                Bullet bullet = obj.GetComponent<Bullet>();
                bullet.speed = 20;
                bullet.range = 0;
                bullet.rangetime = 2;
                bullet.damage = 8;  
                bullet.HitEffect = "ArrowFX";
                bullet.faction = Faction.Enemy; 
                charging = -2;
            }
        }
    }
}
