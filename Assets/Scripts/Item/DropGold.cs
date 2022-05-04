using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGold : MonoBehaviour
{
    public int money;
    public bool getgold;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!getgold && collision != null && collision.collider != null && (collision.collider.gameObject.layer == LayerMask.NameToLayer("Platform") || collision.collider.gameObject.layer == LayerMask.NameToLayer("Floatform")))
        {
            getgold = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject != null && collision.gameObject.name == "Player Event" && getgold)
        {
            Player.Instance.Inven.money += money;
            GameManager.Instance.ShowBoundText(money + "G", gameObject.transform.position, new Color(1, 199 / 255f, 0));
            gameObject.SetActive(false);
        }
    }
}
