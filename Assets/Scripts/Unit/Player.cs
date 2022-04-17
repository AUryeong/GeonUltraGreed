using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UnitBase
{
    [SerializeField]
    private GameObject sprite;
    [SerializeField]
    private GameObject Dashsprite;
    [SerializeField]
    private SpriteRenderer AttackSprite;
    public PlayerInven Inven;
    private Rigidbody2D rigid;


    private static Player instance = null;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(Player)) as Player;
            }
            return instance;
        }
    }

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

    [Header("Status")]
    public float Speed;
    public float JumpSpeed;
    public int JumpMax;
    public int DashMax;
    public int Dash;
    public int DashCooltime;
    Vector2 Dashing;
    float Dashing2;
    bool Dashjansang;
    float DashCooldown;
    int jump;
    bool jumping;
    int jumpadd;
    int attackindex;
    float attackCooltime;

    protected override void Start()
    {
        jump = JumpMax;
        Inven.Init();
    }
    
    public StatBonus GetStat()
    {
        StatBonus statBonus = new StatBonus();
        statBonus.Add(Inven.GetStat());
        statBonus.AttackSpeed += statBonus.AttackSpeed * statBonus.AttackSpeedPer / 100;
        return statBonus;
    }

    public bool IsActable()
    {
        return !Inven.gameObject.activeSelf;
    }

    void CheckMoving(float deltaTime)
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RaycastHit2D rayhit = Physics2D.Raycast(transform.position, Vector2.right, 1.03f, LayerMask.GetMask("Platform"));
            RaycastHit2D rayhit2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1.0f), Vector2.right, 1.03f, LayerMask.GetMask("Platform"));
            RaycastHit2D rayhit3 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1.0f), Vector2.right, 1.03f, LayerMask.GetMask("Platform"));
            if (rayhit.collider == null && rayhit2.collider == null && rayhit3.collider == null)
            {
                transform.Translate(Vector3.right * deltaTime * Speed / 100 * 6);
            }
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RaycastHit2D rayhit = Physics2D.Raycast(transform.position, Vector2.left, 1.03f, LayerMask.GetMask("Platform"));
            RaycastHit2D rayhit2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1.0f), Vector2.left, 1.03f, LayerMask.GetMask("Platform"));
            RaycastHit2D rayhit3 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1.0f), Vector2.left, 1.03f, LayerMask.GetMask("Platform"));
            if (rayhit.collider == null && rayhit2.collider == null && rayhit3.collider == null)
            {
                transform.Translate(Vector3.left * deltaTime * Speed / 100 * 6);
            }
        }
    }

    void MouseMoving()
    {
        Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - sprite.transform.position;
        sprite.transform.localScale = new Vector3((vector.x < 0) ? -1 : 1, 1, 1);
    }

    void CheckHandChanged()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Inven.hand = PlayerInven.Hand.LeftHand;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Inven.hand = PlayerInven.Hand.RightHand;
        }
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            Inven.hand = (PlayerInven.Hand)(((int)Inven.hand + 1) % 2);
        }
    }

    void CheckJumping()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            if (jump != 0 && jumpadd == 0 && !jumping)
            {
                jump--;
                jumpadd = 20;
                jumping = true;
                Rigid.velocity = new Vector2(Rigid.velocity.x, 0);
                Rigid.AddForce(Vector3.up * 600 * JumpSpeed);
            }
            else if (jumpadd > 0)
            {
                Rigid.AddForce(Vector3.up * JumpSpeed * 10);
                jumpadd--;
            }
        }
        else
        {
            jumping = false;
            if (jumpadd > 0)
            {
                jumpadd = 0;
            }
        }
    }

    void CheckDashing(float deltaTime)
    {
        if (Dashing2 > 0)
        {
            Dashing2 -= deltaTime;
            RaycastHit2D rayhit = Physics2D.BoxCast(Rigid.position, new Vector2(1.8f, 1.8f), 0, Dashing, 0.25f, LayerMask.GetMask("Platform"));
            if (rayhit.collider == null)
            {
                Rigid.velocity = new Vector2(0, 0);
                transform.Translate(Dashing * deltaTime);
                if (Dashing2 <= 0)
                {
                    Rigid.AddForce(new Vector2(Dashing.x, Dashing.y * 10));
                }
                if (Dashing2 < 0.05f && !Dashjansang)
                {
                    GameObject obj = PoolManager.Instance.Init(Dashsprite, 0.2f);
                    obj.transform.position = sprite.transform.position;
                    Dashjansang = true;
                }
            }
            else
            {
                Dashing2 = 0;
            }
        }
        if (Input.GetMouseButtonDown(1) && Dash > 0)
        {
            Rigid.velocity = new Vector2(0, 0);
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;
            Vector3 vector2 = Quaternion.AngleAxis(angle - 45, Vector3.forward) * Vector3.one * 20;
            Debug.Log(vector2);
            Dashing = vector2;
            Dashing2 = 0.1f;
            Dashjansang = false;
            GameObject obj = PoolManager.Instance.Init(Dashsprite, 0.2f);
            obj.transform.position = sprite.transform.position;
            Dash--;
            GameManager.Instance.DashChange();
        }
    }

    void CheckUI()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Inven.gameObject.SetActive(true);
        }
    }
    void FixedUpdate()
    {
        if (IsActable())
        {
            CheckJumping();
        }
    }

    void CheckJumped()
    {
        if (Rigid.velocity.y <= 0)
        {
            RaycastHit2D rayhit = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, LayerMask.GetMask("Platform"));
            RaycastHit2D rayhit2 = Physics2D.Raycast(new Vector2(transform.position.x - 0.75f, transform.position.y), Vector2.down, 1.2f, LayerMask.GetMask("Platform"));
            RaycastHit2D rayhit3 = Physics2D.Raycast(new Vector2(transform.position.x + 0.75f, transform.position.y), Vector2.down, 1.2f, LayerMask.GetMask("Platform"));
            if (rayhit.collider != null || rayhit2.collider != null || rayhit3.collider != null)
            {
                jump = JumpMax;
            }
            else if (jump == JumpMax)
            {
                jump--;
            }
        }
    }
    protected override void Update()
    {
        float time = Time.deltaTime;
        if (IsActable())
        {
            MouseMoving();
            CheckMoving(time);
            CheckDashing(time);
            CheckUI();
            CheckMainItem(time);
        }
        CheckDashed(time);
        CheckJumped();
        CheckHandChanged();
        CheckMainItem();
    }

    void CheckMainItem()
    {
        Item item = Inven.GetHands()[0].item;
        if (item != null)
        {
            Sprite sprite = Resources.Load<Sprite>("Item/" + item.ItemText + "0");
            if(sprite == null)
            {
                sprite = Resources.Load<Sprite>("Item/" + item.ItemText);
            }
            if (AttackSprite.sprite != sprite)
            {
                AttackSprite.sprite = sprite;
            }
        }
        else
        {
            Sprite sprite = Resources.Load<Sprite>("Item/None");
            if (AttackSprite.sprite != sprite)
            {
                AttackSprite.sprite = sprite;
            }
        }
    }

    void CheckMainItem(float deltaTime)
    {
        if (attackCooltime > 0)
        {
            attackCooltime -= deltaTime;
        }
        Item item = Inven.GetHands()[0].item;
        if (item != null)
        {
            Vector3 target = Rigid.position;
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
            if (Input.GetMouseButtonDown(0) && attackCooltime <= 0)
            {
                attackindex = (attackindex + 1) % 2;
                GameObject obj = PoolManager.Instance.Init(Resources.Load<GameObject>("Swing/" + item.ItemText));
                obj.GetComponent<Attack>().damage = Random.Range(GetStat().MinDmg, GetStat().MaxDmg + 1);
                obj.transform.position = transform.position;
                obj.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                obj.transform.Translate(Vector2.up * 3);
                attackCooltime = 1/ GetStat().AttackSpeed;
                /*CameraFilter_EarthQuake quake = camera.gameObject.AddComponent<CameraFilter_EarthQuake>();
                quake.X = 0.2f;
                quake.Y = 0.13f;
                AutoScriptDestruct a = camera.gameObject.AddComponent<AutoScriptDestruct>();
                a.targetScript = quake;
                a.time = 0.1f;*/
            }
            AttackSprite.transform.rotation = Quaternion.AngleAxis(angle + (attackindex * ((Mathf.Abs(angle) > 90) ? -135 : 135)), Vector3.forward);
            AttackSprite.flipY = (mouse.x - target.x < 0);
        }
    }
    void CheckDashed(float deltaTime)
    {
        if (DashCooldown >= DashCooltime)
        {
            Dash++;
            DashCooldown = 0;
            GameManager.Instance.DashChange();
        }
        else if (Dash < DashMax)
        {
            DashCooldown += deltaTime;
        }
    }

}