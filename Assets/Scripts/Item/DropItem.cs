using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public Item item;
    public float getitem;
    void Update()
    {
        if(getitem > 0)
        {
            getitem -= Time.deltaTime;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision != null && collision.gameObject != null && collision.gameObject.name == "Player Event" && getitem <= 0)
        {
            Player.Instance.Inven.AddItem(item);
            IWManager.Instance.ShowItem(item);
            gameObject.SetActive(false);
        }
    }

    public void ShowItem(Item item)
    {
        this.item = item;
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Item/" + item.ItemText);
        Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = S;
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
    }
}
