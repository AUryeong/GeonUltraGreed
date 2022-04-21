using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGold : MonoBehaviour
{
    public int money;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject != null && collision.gameObject.name == "Player Event")
        {
            Player.Instance.Inven.money += money;
            gameObject.SetActive(false);
        }
    }
}
