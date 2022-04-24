using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skel : EnemyBase
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
    }

    protected override void Update()
    {
        base.Update();
        if (agro)
        {
            if (Vector3.Distance(Player.Instance.transform.position, transform.position) < 1.5f && !attacking)
            {
                attacking = true;
                swordanimator.SetTrigger("Slash");
                animator.SetBool("isWalking", true);
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

    public void Attack()
    {
        RaycastHit2D[] array = Physics2D.BoxCastAll(transform.position, swordanimator.GetComponent<SpriteRenderer>().bounds.size, Vector3.zero, 0f, LayerMask.GetMask("Player"));
        for (int i = 0; i < array.Length; i++)
        {
            if (array[0].collider != null && array[0].transform.tag == "Player")
            {
                Player player = array[0].collider.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    player.Damaged(Random.Range(4, 7 + 1f));
                }
            }
        }
    }

    public void EndAttack()
    {
        attacking = false;
        animator.SetBool("isWalking", false);
    }
}
