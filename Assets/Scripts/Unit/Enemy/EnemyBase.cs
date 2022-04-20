using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : UnitBase
{
    public float AgroDistance;
    public float Speed;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private bool agro;
    private float hitui;
    private float hitx;
    protected override void Update()
    {
        float deltaTime = Time.deltaTime;
        if(Hp <= 0)
        {
            gameObject.SetActive(false);
        }
        if (agro)
        {
            if (Player.Instance.transform.position.x - transform.position.x > 0)
            {
                transform.localScale = Vector3.one;
                spriteRenderer.transform.parent.localScale = new Vector3(-1, 1, 1);
                transform.Translate(Vector3.right * deltaTime * Speed);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
                spriteRenderer.transform.parent.localScale = Vector3.one;
                transform.Translate(Vector3.left * deltaTime * Speed);
            }
        }
        else
        {
            if (Vector3.Distance(Player.Instance.transform.position, transform.position) < AgroDistance)
            {
                agro = true;
                Update();
            }
        }
        if(hitui > 0)
        {
            hitui -= deltaTime;
            if (!spriteRenderer.transform.parent.gameObject.activeSelf)
            {
                spriteRenderer.transform.parent.gameObject.SetActive(true);
            }
            spriteRenderer.size = new Vector2(hitx * Hp / MaxHp, spriteRenderer.size.y) ;
            spriteRenderer.transform.localPosition = new Vector3(hitx/2 -(hitx/2 * Hp / MaxHp), 0, 0);
        }
        else
        {
            if (spriteRenderer.transform.parent.gameObject.activeSelf)
            {
                spriteRenderer.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    public override void Damaged(float damage)
    {
        base.Damaged(damage);
        hitui = 2;
    }
    protected override void Start()
    {
        base.Start();
        hitx = spriteRenderer.size.x;
    }
    protected virtual void FixedUpdate()
    {

    }
}
