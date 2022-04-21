using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.gameObject != null && collision.gameObject.name == "Player Event")
        {
            Player.Instance.Inven.AddItem(item);
            IWManager.Instance.ShowItem(item);
            gameObject.SetActive(false);
        }
    }
}
