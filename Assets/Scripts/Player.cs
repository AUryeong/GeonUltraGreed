using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject lookat;
    [SerializeField]
    private GameObject sprite;
    public float Speed;
    void Start()
    {
        
    }

    void Moving()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
            sprite.transform.localScale = Vector3.one;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * Speed);
            sprite.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * Time.deltaTime * 20);
        }
    }

    void MouseMoving()
    {
        Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - sprite.transform.position;
        lookat.transform.localPosition = Vector3.Lerp(lookat.transform.localPosition, new Vector3(Mathf.Clamp(vector.normalized.x/2, -0.17f, 0.17f), Mathf.Clamp(vector.normalized.y / 2, -0.12f, 0.12f), 0), Time.deltaTime * 10);
    }

    void FixedUpdate()
    {
        Moving();
        MouseMoving();
    }
}
