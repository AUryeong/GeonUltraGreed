using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : UnitBase
{
    public float AgroDistance;
    public float Speed;

    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    protected bool agro;
    protected float hitui;
    protected float hitx;
    protected bool hitred = true;
    protected Color color;
    protected float hitu;
    protected override void Update()
    {
        float deltaTime = Time.deltaTime;
        if(Hp <= 0)
        {
            OnDie();
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
        if (hitu >0 && hitred)
        {
            hitu -= deltaTime;
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            if (hitu <= 0)
            {
                GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    public override void Damaged(float damage)
    {
        base.Damaged(damage);
        if (hitred)
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            hitu = 0.05f;
        }
        hitui = 2;
    }
    protected override void Start()
    {
        base.Start();
        hitx = spriteRenderer.size.x;
        if (hitred)
        {
            color = GetComponent<SpriteRenderer>().color;
        }
    }
    protected override void OnDie()
    {
        base.OnDie();
        GameObject obj = PoolManager.Instance.Init(Resources.Load<GameObject>("FX/DieFX"));
        float f = ((gameObject.GetComponent<BoxCollider2D>().size.x > gameObject.GetComponent<BoxCollider2D>().size.y) ? gameObject.GetComponent<BoxCollider2D>().size.x : gameObject.GetComponent<BoxCollider2D>().size.y);
        obj.transform.localScale = new Vector3(f, f, f);
        obj.transform.position = gameObject.transform.position;
    }
    protected virtual void FixedUpdate()
    {

    }
}
