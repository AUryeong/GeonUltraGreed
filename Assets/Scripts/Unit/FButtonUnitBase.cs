using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FButtonUnitBase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject != null && collision.gameObject.name == "Player Event2")
        {
            Player.Instance.FButton.SetActive(true);
            Player.Instance.FButton.transform.position = GetComponent<Rigidbody2D>().position + GetComponent<BoxCollider2D>().offset + new Vector2(0, gameObject.GetComponent<BoxCollider2D>().size.y + 0.6f);
            Player.Instance.FButtonUnit = this;
        }
    }

    public virtual void OnF()
    {

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject != null && collision.gameObject.name == "Player Event2" && Player.Instance.FButtonUnit == this)
        {
            Player.Instance.FButtonUnit = null;
            Player.Instance.FButton.SetActive(false);
        }
    }
}
