using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject sprite;
    [SerializeField]
    private GameObject dashsprite;
    private Rigidbody2D rigid;

    public Rigidbody2D Rigid
    {
        get
        {
            if (rigid == null)
            {
                rigid = GetComponent<Rigidbody2D>();
            }
            return rigid;
        }
    }

    public float speed;
    public float jumpspeed;
    public int jumpmax;
    public float hp;
    public int maxhp;
    public int maxdash;
    public int dash;
    public int dashcooltime;
    public List<Item> inventories = new List<Item>(15);
    Vector2 dashing;
    float dashing2;
    bool dashjansang;
    float dashcooldown;
    int jump;
    public int jumpadd;

    void Start()
    {
        jump = jumpmax;
    }

    void CheckMoving(float deltaTime)
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RaycastHit2D rayhit = Physics2D.Raycast(transform.position, Vector2.right, 0.85f, LayerMask.GetMask("Platform"));
            RaycastHit2D rayhit2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.7f), Vector2.right, 0.85f, LayerMask.GetMask("Platform"));
            RaycastHit2D rayhit3 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.7f), Vector2.right, 0.85f, LayerMask.GetMask("Platform"));
            if (rayhit.collider == null && rayhit2.collider == null && rayhit3.collider == null)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed / 100 * 6);
            }
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RaycastHit2D rayhit = Physics2D.Raycast(transform.position, Vector2.left, 0.85f, LayerMask.GetMask("Platform"));
            RaycastHit2D rayhit2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.7f), Vector2.left, 0.85f, LayerMask.GetMask("Platform"));
            RaycastHit2D rayhit3 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.7f), Vector2.left, 0.85f, LayerMask.GetMask("Platform"));
            if (rayhit.collider == null && rayhit2.collider == null && rayhit3.collider == null)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed / 100 * 6);
            }
        }
    }

    void MouseMoving()
    {
        Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - sprite.transform.position;
        sprite.transform.localScale = new Vector3((vector.x < 0) ? -1 : 1, 1, 1);
    }

    void CheckJumping()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && jump != 0)
        {
            jump--;
            jumpadd = 100;
            Rigid.velocity = new Vector2(Rigid.velocity.x, 0);
            Rigid.AddForce(Vector3.up * 600 * jumpspeed);
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)) && jumpadd > 0)
        {
            Rigid.AddForce(Vector3.up  * jumpspeed * 4);
            jumpadd--;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Space))
        {
            jumpadd = 0;
        }
        if (Rigid.velocity.y < 0)
        {
            RaycastHit2D rayhit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, LayerMask.GetMask("Platform"));
            RaycastHit2D rayhit2 = Physics2D.Raycast(new Vector2(transform.position.x - 0.75f, transform.position.y), Vector2.down, 1.5f, LayerMask.GetMask("Platform"));
            RaycastHit2D rayhit3 = Physics2D.Raycast(new Vector2(transform.position.x + 0.75f, transform.position.y), Vector2.down, 1.5f, LayerMask.GetMask("Platform"));
            if (rayhit.collider != null || rayhit2.collider != null || rayhit3.collider != null)
            {
                jump = jumpmax;
            }
            else if (jump == jumpmax)
            {
                jump--;
            }
        }
    }

    void CheckDashing(float deltaTime)
    {
        if (dashcooldown >= dashcooltime)
        {
            dash++;
            dashcooldown = 0;
            GameManager.Instance.DashChange();
        }
        else if (dash < maxdash)
        {
            dashcooldown += deltaTime;
        }
        if(dashing2 > 0)
        {
            dashing2 -= deltaTime;
            RaycastHit2D rayhit = Physics2D.Raycast(transform.position, dashing, 1f, LayerMask.GetMask("Platform"));
            if(rayhit.collider == null)
            {
                Rigid.velocity = new Vector2(0, 0);
                transform.Translate(dashing *deltaTime / 1.5f);
                if (dashing2 <= 0)
                {
                    Rigid.AddForce(dashing * 5);
                }
                if (dashing2 < 0.05f && !dashjansang)
                {
                    DashJansang();
                    dashjansang = true;
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && dash > 0)
        {
            Rigid.velocity = new Vector2(0, 0);
            Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position + new Vector3(0,0,10);
            Vector3 vector2 = vector.normalized * Mathf.Rad2Deg;
            dashing = vector2;
            dashing2 = 0.1f;
            dashjansang = false;
            DashJansang();
            dash--;
            GameManager.Instance.DashChange();
        }
    }
    
    void DashJansang()
    {
        GameObject obj = PoolManager.Instance.Init(dashsprite, 0.2f);
        obj.transform.position = sprite.transform.position;
    }

    void Update()
    {
        float time = Time.deltaTime;
        MouseMoving();
        CheckJumping();
        CheckMoving(time);
        CheckDashing(time);
    }
}