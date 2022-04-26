using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Faction
{
    Player,
    Enemy
}
public class Bullet : MonoBehaviour
{
    public float speed;
    public float range;
    public float rangetime;
    public float damage;
    public string HitEffect;
    public Faction faction = Faction.Player;
    
    protected float moverange;
    protected float movetime;
    protected BoxCollider2D boxCollider2D;

    protected virtual void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    protected virtual void Damaged(UnitBase unit)
    {
        unit.Damaged(damage);   
    }

    protected virtual void FireCollision()
    {
        string[] layer = new string[2]
        {
                "Player",
                "PlayerDash"
        };
        RaycastHit2D[] array = Physics2D.BoxCastAll(transform.position, boxCollider2D.size, gameObject.transform.rotation.z, Vector3.zero, 0, (faction == Faction.Player) ? LayerMask.GetMask("Enemy") : LayerMask.GetMask(layer));
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].collider != null && array[i].transform.tag == (((faction == Faction.Player) ? "Enemy" : "Player")))
            {
                UnitBase unit = array[i].collider.gameObject.GetComponent<UnitBase>();
                if (unit != null)
                {
                    Damaged(unit);
                    Effect();
                    return;
                }
            }
        }
        array = Physics2D.BoxCastAll(transform.position, boxCollider2D.size, gameObject.transform.rotation.z, Vector3.zero, 0, LayerMask.GetMask("Platform"));
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].collider != null)
            {
                Effect();
                return;
            }
        }
    }

    void Effect()
    {
        gameObject.SetActive(false);
        GameObject obj2 = PoolManager.Instance.Init(Resources.Load<GameObject>("FX/" + HitEffect));
        obj2.transform.localScale = Vector3.one;
        obj2.transform.position = transform.position;
        obj2.transform.localRotation = transform.localRotation;
        moverange = 0;
        movetime = 0;
    }

    protected virtual void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        FireCollision();
        if (range > 0)
        {
            moverange += speed;
            if (moverange >= range)
            {
                Effect();
                return;
            }
        }
        if (rangetime > 0)
        {
            movetime += Time.deltaTime;
            if (movetime >= rangetime)
            {
                Effect();
                return;
            }
        }
    }
}
