using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FButtonUnitBase : MonoBehaviour
{
    float distance;
    protected virtual void Start()
    {
        Vector2 vector = GetComponent<SpriteRenderer>().size;
        distance = vector.x*2;
    }
    protected virtual void Update()
    {
        Player player = Player.Instance;
        if (player.FButtonUnit != this && CanShowF() && Vector2.Distance(player.transform.position, transform.position) < distance)
        {
            player.FButton.gameObject.SetActive(true);
            player.FButton.transform.position = transform.position + (new Vector3(0, GetComponent<SpriteRenderer>().size.y + player.FButton.size.y/2));
            player.FButtonUnit = this;
        }
        else if (player.FButtonUnit == this && Vector2.Distance(player.transform.position, transform.position) >= distance)
        {
            player.FButtonUnit = null;
            player.FButton.gameObject.SetActive(false);
        }
    }

    public virtual void OnF()
    {
    }

    protected virtual bool CanShowF()
    {
        return true;
    }
}
