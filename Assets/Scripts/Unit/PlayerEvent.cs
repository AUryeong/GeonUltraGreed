using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvent : MonoBehaviour
{
    public List<UnitBase> units = new List<UnitBase>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject != null && Player.Instance.Dashing2 > 0)
        {
            Damage(collision.gameObject.GetComponent<EnemyBase>());
        }
    }
    void Damage(EnemyBase unit)
    {
        if (unit != null && unit != Player.Instance && !units.Contains(unit))
        {
            units.Add(unit);
            StatBonus stat = Player.Instance.Stat;
            unit.Damaged((Random.RandomRange(stat.MinDmg, stat.MaxDmg + 1f)) * (100 + stat.Power) / 100 * (50 + stat.DashDmgPer) / 100);
        }
    }

    void Update()
    {
        if (Player.Instance.Dashing2 > 0)
        {
            RaycastHit2D rayhit = Physics2D.BoxCast(Player.Instance.transform.position, new Vector2(2.5f, 2.5f), 0, Player.Instance.Dashing, 0.25f, LayerMask.GetMask("Enemy"));
            if(rayhit.collider != null)
            {
                Damage(rayhit.collider.gameObject.GetComponent<EnemyBase>());

            }
        }
    }
}
