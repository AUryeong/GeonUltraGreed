using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SwordSkel : EnemyBase
{
    [SerializeField]
    private Animator swordanimator;

    private Animator animator;

    private bool attacking;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        attacking = false;
        Speed = Speed * Random.Range(0.9f, 1.1f);
    }

    protected override void Update()
    {
        base.Update();
        if (agro)
        {
            RaycastHit2D cast = Physics2D.BoxCast(transform.position + (left ? Vector3.left : Vector3.right), Vector3.one * 1.7f, 90, Vector3.zero, 0, LayerMask.GetMask("Player"));
            if (cast.collider != null && cast.transform.tag == "Player" && !attacking)
            {
                attacking = true;
                swordanimator.SetTrigger("Slash");
                animator.SetBool("isWalking", false);
            }
        }
    }

    protected override void CheckMove(float deltaTime)
    {
        if (!attacking)
        {
            base.CheckMove(deltaTime);
        }
    }

    protected override void CheckDirection(float deltaTime)
    {
        if (!attacking)
        {
            base.CheckDirection(deltaTime);
        }
    }

    public void Attack()
    {
        RaycastHit2D[] array = Physics2D.BoxCastAll(transform.position + (left ? Vector3.left : Vector3.right), Vector3.one * 2f, 90, Vector3.zero, 0, LayerMask.GetMask("Player"));
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].collider != null && array[i].transform.tag == "Player")
            {
                Player player = array[i].collider.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    player.Damaged(Random.Range(4f, 7f));
                }
            }
        }
    }

    public void EndAttack()
    {
        attacking = false;
        animator.SetBool("isWalking", true);
    }
}
