using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEnter : MonoBehaviour
{
    public DoorArrow doorArrow;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.collider != null && collision.transform.tag == "Player")
        {
            GameManager.Instance.EnterMap(doorArrow);
        }
    }
}
