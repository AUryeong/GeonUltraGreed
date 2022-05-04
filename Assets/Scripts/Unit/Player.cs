using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UnitBase
{
    [SerializeField]
    private SpriteRenderer sprite;
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
    public float JumpSpeed;
    public int JumpMax;
    public int DashMax;
    public int Dash;
    public int DashCooltime;
    StatBonus stat;

    public StatBonus Stat
    {
        get { return stat; }
    }

    public Vector2 Dashing;
    public float Dashing2;
    bool Dashjansang;
    float DashCooldown;
    int jump;
    bool jumping;
    int jumpadd;
    int AttackIndex;
    float attackCooltime;
    float hurtinv;
    List<UnitBase> DashDamageUnits = new List<UnitBase>();
    public GameObject FButton;
    public FButtonUnitBase FButtonUnit;
    BoxCollider2D colider2d;

    protected override void Start()
    {
        jump = JumpMax;
        Inven.Init();
        colider2d = GetComponent<BoxCollider2D>();
        StatChange();
    }
    
    public StatBonus GetStat()
    {
        StatBonus statBonus = new StatBonus()
        {
            Power = 5,
            Defense = 5,
            Crit = 7,
            CritDmgPer = 100,
            Evade = 5,
            Speed = 6,
            SpeedPer = 100,
            DashDmgPer = 50
        };
        statBonus.Add(Inven.GetStat());
        return statBonus;
    }

    public bool IsActable()
    {
        return !Inven.gameObject.activeSelf;
    }

    public float GetDefenseDamage()
    {
        return stat.Defense / 100;
    }

    public override void Damaged(float damage)
    {
        if (hurtinv <= 0)
        {
            damage -= damage * GetDefenseDamage();
            damage -= stat.Strong;
            base.Damaged(damage < 0 ? 0 : damage);
            GameManager.Instance.HealthChange();
            GameManager.Instance.CameraEarthQuake(0.3f, 0.165f, 0.131f);
            hurtinv = 0.7f;
            sprite.color = new Color(1, 1, 1, 0.7f);
            AttackSprite.color = new Color(1, 1, 1, 0.7f);
        }
        
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
                transform.Translate(Vector3.right * deltaTime * (stat.Speed * (stat.SpeedPer) / 100));
            }
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RaycastHit2D rayhit = Physics2D.Raycast(transform.position, Vector2.left, 1.03f, LayerMask.GetMask("Platform"));
            RaycastHit2D rayhit2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1.0f), Vector2.left, 1.03f, LayerMask.GetMask("Platform"));
            RaycastHit2D rayhit3 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1.0f), Vector2.left, 1.03f, LayerMask.GetMask("Platform"));
            if (rayhit.collider == null && rayhit2.collider == null && rayhit3.collider == null)
            {
                transform.Translate(Vector3.left * deltaTime * (stat.Speed * (stat.SpeedPer) / 100));
            }
        }
    }

    void MouseMoving()
    {
        Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - sprite.transform.position;
        sprite.transform.localScale = new Vector3((vector.x < 0) ? -1 : 1, 1, 1);
        Vector3 vector2 = AttackSprite.transform.localPosition;
        float x = Mathf.Abs(vector2.x);
        AttackSprite.transform.localPosition = new Vector3((vector.x < 0) ? -x : x, vector2.y, 0);
    }

    void CheckHandChanged()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote) || (Inven.hand == PlayerInven.Hand.RightHand && Input.GetKeyDown(KeyCode.Alpha1)) || (Inven.hand == PlayerInven.Hand.LeftHand && Input.GetKeyDown(KeyCode.Alpha2)))
        {
            Inven.hand = (PlayerInven.Hand)(((int)Inven.hand + 1) % 2);
            StatChange();
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

    void CheckF()
    {
        if (FButtonUnit != null && Input.GetKeyDown(KeyCode.F))
        {
            FButtonUnit.OnF();
            FButton.gameObject.SetActive(false);
        }
    }

    void CheckDashing(float deltaTime)
    {
        if (Dashing2 > 0)
        {
            Dashing2 -= deltaTime;
            RaycastHit2D[] rayhit2 = Physics2D.BoxCastAll(Player.Instance.transform.position, new Vector2(2.5f, 2.5f), 0, Player.Instance.Dashing, 0.25f, LayerMask.GetMask("Enemy"));

            for (int i = 0; i < rayhit2.Length; i++)
            {
                if (rayhit2[i].collider != null)
                {
                    EnemyBase unit = rayhit2[i].collider.gameObject.GetComponent<EnemyBase>();
                    if (unit != null && !DashDamageUnits.Contains(unit))
                    {
                        DashDamageUnits.Add(unit);
                        StatBonus stat = Stat;
                        float damage = (Random.Range(stat.MinDmg, stat.MaxDmg)) * (100 + stat.Power) / 100 * stat.DashDmgPer / 100;
                        if (stat.Crit >= Random.Range(0f, 100f))
                        {
                            damage = (Random.Range(stat.MinDmg, stat.MaxDmg) ) * 2 * (100 + stat.Power) / 100 * stat.DashDmgPer / 100;
                            GameManager.Instance.ShowBoundText(Mathf.Round(damage).ToString(), unit.transform.position, Color.yellow);
                        }
                        else
                        {
                            GameManager.Instance.ShowBoundText(Mathf.Round(damage).ToString(), unit.transform.position, Color.white);
                        }
                        unit.Damaged(damage);
                    }
                }
            }
            RaycastHit2D rayhit = Physics2D.BoxCast(Rigid.position, colider2d.size, 0, Dashing, 0.25f, LayerMask.GetMask("Platform"));
            if (rayhit.collider == null)
            {
                Rigid.velocity = new Vector2(0, 5);
                transform.Translate(Dashing * deltaTime);
            }
            if (Dashing2 <= 0)
            {
                Rigid.AddForce(new Vector2(Dashing.x, Dashing.y * 10));
                gameObject.layer = 8;
            }
            if (Dashing2 < 0.05f && !Dashjansang)
            {
                GameObject obj = PoolManager.Instance.Init(Dashsprite, 0.2f);
                obj.transform.position = sprite.transform.position;
                Dashjansang = true;
            }
        }
        if (Input.GetMouseButtonDown(1) && Dash > 0)
        {
            Rigid.velocity = new Vector2(0, 5);
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;
            Vector3 vector2 = Quaternion.AngleAxis(angle - 45, Vector3.forward) * Vector3.one * 20;
            Dashing = vector2;
            Dashing2 = 0.1f;
            DashDamageUnits.Clear();
            Dashjansang = false;
            GameObject obj = PoolManager.Instance.Init(Dashsprite, 0.2f);
            obj.transform.position = sprite.transform.position;
            Dash--;
            gameObject.layer = 15;
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
            string[] layer = new string[2]
            {
                "Platform",
                "Floatform"
            };
            RaycastHit2D rayhit = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, LayerMask.GetMask(layer));
            RaycastHit2D rayhit2 = Physics2D.Raycast(new Vector2(transform.position.x - 0.75f, transform.position.y), Vector2.down, 1.2f, LayerMask.GetMask(layer));
            RaycastHit2D rayhit3 = Physics2D.Raycast(new Vector2(transform.position.x + 0.75f, transform.position.y), Vector2.down, 1.2f, LayerMask.GetMask(layer));
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
            CheckF();
            CheckMainItem(time);
        }
        CheckDashed(time);
        CheckJumped();
        CheckHandChanged();
        CheckMainItem();
        CheckTime(time);
    }

    void CheckTime(float deltaTime)
    {
        if(hurtinv > 0)
        {
            hurtinv -= deltaTime;
            if(hurtinv <= 0)
            {
                sprite.color = new Color(1, 1, 1, 1);
                AttackSprite.color = new Color(1, 1, 1, 1);
            }
        }
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

    public void StatChange()
    {
        stat = GetStat();
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
                if(item.AttackType == WeaponAttackType.Sword)
                {
                    AttackIndex++;
                    AttackIndex %= 2;
                    AttackSprite.sortingOrder = (AttackIndex == 0) ? 2 : 4;
                    GameObject obj = PoolManager.Instance.Init(Resources.Load<GameObject>("Swing/" + item.ItemText));
                    if (obj != null)
                    {
                        obj.transform.position = transform.position;
                        obj.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                        obj.transform.Translate(Vector2.up * (1 + AttackSprite.sprite.bounds.size.y));
                        RaycastHit2D[] array = Physics2D.BoxCastAll((obj.transform.position+transform.position)/2, obj.GetComponent<BoxCollider2D>().size, angle-90, Vector3.zero, 0, LayerMask.GetMask("Enemy"));
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (array[i].collider != null && array[i].transform.tag == "Enemy")
                            {
                                EnemyBase enemy = array[i].collider.gameObject.GetComponent<EnemyBase>();
                                if (enemy != null)
                                {
                                    float damage = (Random.Range(stat.MinDmg, stat.MaxDmg )) * (100 + stat.Power) / 100;
                                    if (stat.Crit >= Random.Range(0f, 100f))
                                    {
                                        damage = (Random.Range(stat.MinDmg, stat.MaxDmg) * 2) * (100 + stat.Power) / 100;
                                        GameManager.Instance.ShowBoundText(Mathf.Round(damage).ToString(), enemy.transform.position, Color.yellow);
                                    }
                                    else
                                    {
                                        GameManager.Instance.ShowBoundText(Mathf.Round(damage).ToString(), enemy.transform.position, Color.white);
                                    }
                                    enemy.Damaged(damage);
                                    GameObject obj2 = PoolManager.Instance.Init(Resources.Load<GameObject>("FX/" + item.HitEffect));
                                    if(obj2 != null)
                                    {
                                        Vector2 size = enemy.GetComponent<BoxCollider2D>().size;
                                        float f = (size.x > size.y) ? size.y : size.x;
                                        obj2.transform.localScale = new Vector3(f, f, f);
                                        obj2.transform.position = enemy.transform.position;
                                        obj2.transform.rotation = obj.transform.rotation;
                                    }
                                }
                            }
                        }
                    }
                    attackCooltime = 1 / stat.AttackSpeed;
                    GameManager.Instance.CameraEarthQuake(0.1f, 0.055f, 0.1f);
                }
            }
            float lastangle = angle + (AttackIndex * ((Mathf.Abs(angle) > 90) ? -135 : 135));
            if (item.AttackType == WeaponAttackType.Bullet)
            {
                lastangle = angle + 90;
            }
             AttackSprite.transform.rotation = Quaternion.AngleAxis(lastangle, Vector3.forward);
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