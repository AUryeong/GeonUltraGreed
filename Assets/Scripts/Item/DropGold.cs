using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGold : MonoBehaviour
{
    public int money;
    public float getgold;
    void Update()
    {
        if (getgold > 0)
        {
            getgold -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject != null && collision.gameObject.name == "Player Event" && getgold <= 0)
        {
            Player.Instance.Inven.money += money;
            gameObject.SetActive(false);
        }
    }
}
