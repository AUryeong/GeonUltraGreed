using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public virtual int MaxHp
    {
        get
        {
            return 100;
        }
    }
    public virtual float AgroDistance
    {
        get
        {
            return 10f;
        }
    }

    public virtual float Speed
    {
        get
        {
            return 1.5f;
        }
    }


    public int Hp;

    private bool agro;

    protected virtual void Start()
    {
        Hp = MaxHp;
    }
    protected virtual void Update()
    {
        float deltaTime = Time.deltaTime;
        if (agro)
        {
            if (Player.Instance.transform.position.x - transform.position.x > 0)
            {
                transform.localScale = Vector3.one;
                transform.Translate(Vector3.right * deltaTime * Speed);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
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
    }
    protected virtual void FixedUpdate()
    {

    }
}
