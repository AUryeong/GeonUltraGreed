using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : UnitBase
{
    public float AgroDistance;
    public float Speed;

    [SerializeField]
    protected SpriteRenderer spriteRenderer;

    protected SpriteRenderer _spriteRenderer;
    protected bool agro;
    protected float hitui;
    protected float hitx;
    protected bool hitred = true;
    protected Color color;
    protected float hitu;
    protected bool left;
    public int money;
    public int randommoney;

    [SerializeField]
    protected float checkmovetime;
    protected float checkmovecooltime;

    protected virtual void CheckHpUI(float deltaTime)
    {
        if (hitui > 0)
        {
            hitui -= deltaTime;
            if (!spriteRenderer.transform.parent.gameObject.activeSelf)
            {
                spriteRenderer.transform.parent.gameObject.SetActive(true);
            }
            spriteRenderer.size = new Vector2(hitx * Hp / MaxHp, spriteRenderer.size.y);
            spriteRenderer.transform.localPosition = new Vector3(hitx / 2 - (hitx / 2 * Hp / MaxHp), 0, 0);
        }
        else
        {
            if (spriteRenderer.transform.parent.gameObject.activeSelf)
            {
                spriteRenderer.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    protected virtual void CheckDirection(float deltaTime)
    {

        checkmovecooltime += deltaTime;
        if (checkmovecooltime >= checkmovetime)
        {
            checkmovecooltime = 0;
            if (Player.Instance.transform.position.x - transform.position.x > 0)
            {
                left = false;
                transform.localScale = Vector3.one;
                spriteRenderer.transform.parent.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                left = true;
                transform.localScale = new Vector3(-1, 1, 1);
                spriteRenderer.transform.parent.localScale = Vector3.one;
            }
        }
    }

    protected virtual void CheckMove(float deltaTime)
    {
        if (agro)
        {
            if (left)
            {
                transform.Translate(Vector3.left * deltaTime * Speed);
            }
            else
            {
                transform.Translate(Vector3.right * deltaTime * Speed);
            }
        }
    }

    protected virtual bool CheckDie()
    {
        return Hp <= 0;
    }

    protected virtual void CheckAgro()
    {
        if (Vector3.Distance(Player.Instance.transform.position, transform.position) < AgroDistance && !agro)
        {
            agro = true;
            Update();
        }
    }

    protected override void Update()
    {
        float deltaTime = Time.deltaTime;
        CheckHpUI(deltaTime);
        if (CheckDie())
        {
            OnDie();
            gameObject.SetActive(false);
            return;
        }
        CheckMove(deltaTime);
        CheckAgro();
        CheckDirection(deltaTime);
        if (hitu > 0 && hitred)
        {
            hitu -= deltaTime;
            _spriteRenderer.color = new Color(255, 0, 0);
            if (hitu <= 0)
            {
                _spriteRenderer.color = color;
            }
        }
    }

    public override bool Damaged(float damage)
    {
        if (hitred)
        {
            _spriteRenderer.color = new Color(255, 0, 0);
            hitu = 0.05f;
        }
        hitui = 2;
        return base.Damaged(damage);
    }
    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        hitx = spriteRenderer.size.x;
        if (hitred)
        {
            color = _spriteRenderer.color;
        }
    }
    protected override void OnDie()
    {
        base.OnDie();
        GameObject obj = PoolManager.Instance.Init(Resources.Load<GameObject>("FX/DieFX"));
        float f = ((gameObject.GetComponent<BoxCollider2D>().size.x > gameObject.GetComponent<BoxCollider2D>().size.y) ? gameObject.GetComponent<BoxCollider2D>().size.x : gameObject.GetComponent<BoxCollider2D>().size.y);
        obj.transform.localScale = new Vector3(f, f, f);
        obj.transform.position = gameObject.transform.position;
        int getmoney = money + Random.Range(0, randommoney + 1);
        int billion = getmoney / 100;
        int coin = (getmoney % 100) / 10;
        for (int i = 0; i < billion; i++)
        {
            GameObject billionobj = PoolManager.Instance.Init(Resources.Load<GameObject>("DropItem/DropBullion"));
            billionobj.transform.position = transform.position + new Vector3(0, 0.1f, 0);
            billionobj.GetComponent<DropGold>().getgold = false;
            billionobj.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * 800);
        }
        for (int i = 0; i < coin; i++)
        {
            GameObject coinobj = PoolManager.Instance.Init(Resources.Load<GameObject>("DropItem/DropGold"));
            coinobj.transform.position = transform.position + new Vector3(0, 0.1f, 0);
            coinobj.GetComponent<DropGold>().getgold = false;
            coinobj.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * 800);
        }
    }
    protected virtual void FixedUpdate()
    {

    }
}
