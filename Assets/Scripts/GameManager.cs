using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GameManager : Singleton<GameManager>
{

    [Header("[Cursor]")]
    [SerializeField]
    Image cursor;
    [SerializeField]
    Texture2D invisiblecursor;

    [Header("[UI - Cursor]")]
    [SerializeField]
    Sprite uicursor;
    [SerializeField]
    Sprite defaultcursor;

    [Header("[UI - Hp]")]
    [SerializeField]
    Image Hpbar;
    [SerializeField]
    Image Hpwave;
    [SerializeField]
    TextMeshProUGUI Hptext;

    [Header("[UI - Dash]")]
    [SerializeField]
    Image Dashbarbase;
    [SerializeField]
    Image Dashbarleft;
    [SerializeField]
    Image Dashbarright;
    [SerializeField]
    List<Image> Dashbars;

    [Header("[UI - EquipWeapon]")]
    [SerializeField]
    RectTransform EW1;
    [SerializeField]
    Image EW1item;
    [SerializeField]
    Image EW1item2;
    [SerializeField]
    RectTransform EW2;
    [SerializeField]
    Image EW2item;
    [SerializeField]
    Image EW2item2;

    [Header("UI - Esc")]
    [SerializeField]
    GameObject escui;
    [SerializeField]
    ClickableSlot extiorrun;
    [SerializeField]
    GameObject exitrealui;

    [Header("UI - ELSE")]
    [SerializeField]
    TextMeshPro boundtext;
    [SerializeField]
    TextMeshProUGUI centertext;
    [SerializeField]
    TextMeshProUGUI moneytext;

    public Map map;
    public List<CameraFilter_EarthQuake> cameraFilter_EarthQuakes = new List<CameraFilter_EarthQuake>();
    int Dashcount;
    Coroutine centertextcoroutine;
    Player player;

    public bool IsEscing()
    {
        return escui.activeSelf;
    }
    public void PressEsc()
    {
        escui.SetActive(!escui.activeSelf);
        exitrealui.SetActive(false);
    }
    public void GameEnd()
    {
        exitrealui.SetActive(true);
    }

    public void ShowCenterText(string text, Color color,float duration = 1f)
    {
        centertext.gameObject.SetActive(true);
        centertext.text = text;
        centertext.color = color;
        if (centertextcoroutine != null)
            StopCoroutine(centertextcoroutine);

        centertextcoroutine = StartCoroutine(removecentertext(duration));
    }
    IEnumerator removecentertext(float duration)
    {
        yield return new WaitForSeconds(duration);
        centertext.gameObject.SetActive(false);
    }
    public void NoGameEnd()
    {
        exitrealui.SetActive(false);
    }
    public void RealGameEnd()
    {
        Application.Quit();
    }
    public void EquipWeaponChange(int i = 0)
    {
        RectTransform obj1 = EW1;
        RectTransform obj2 = EW2;
        if (i == 0)
        {
            obj1 = EW2;
            obj2 = EW1;
        }
        obj2.SetAsLastSibling();
        obj1.DOAnchorPos(new Vector2(-115.1f, 136.2f), 0.5f).SetEase(Ease.OutQuart);
        obj2.DOAnchorPos(new Vector2(-140f, 108.75f), 0.5f).SetEase(Ease.OutQuart);
    }

    
    public void CameraEarthQuake(float X, float Y, float Time)
    {
        cameraFilter_EarthQuakes.Add(new CameraFilter_EarthQuake()
        {
            X = X,
            Y = Y,
            Time = Time
        });
    }
    protected override void Awake()
    {
        base.Awake();
        player = Player.Instance;

        DashChange();
        HealthChange();
    }
    void Update()
    {
        CursorChange();
        CameraControl();
        UpdateUI();
    }

    public void UpdateEquipWeapon()
    {
        ItemSlot slot = player.Inven.MainWeapon[0];
        if (slot != null && slot.item != null)
        {
            EW1item.sprite = Resources.Load<Sprite>("Item/" + slot.item.ItemText);
        }
        else
        {
            EW1item.sprite = Resources.Load<Sprite>("Item/None");
        }
        EW1item.SetNativeSize();
        slot = player.Inven.SubWeapon[0];
        if (slot != null && slot.item != null)
        {
            EW1item2.sprite = Resources.Load<Sprite>("Item/" + slot.item.ItemText);
        }
        else
        {
            EW1item2.sprite = Resources.Load<Sprite>("Item/None");
        }
        EW1item2.SetNativeSize();
        slot = player.Inven.MainWeapon[1];
        if (slot != null && slot.item != null)
        {
            EW2item.sprite = Resources.Load<Sprite>("Item/" + slot.item.ItemText);
        }
        else
        {
            EW2item.sprite = Resources.Load<Sprite>("Item/None");
        }
        EW2item.SetNativeSize();
        slot = player.Inven.SubWeapon[1];
        if (slot != null && slot.item != null)
        {
            EW2item2.sprite = Resources.Load<Sprite>("Item/" + slot.item.ItemText);
        }
        else
        {
            EW2item2.sprite = Resources.Load<Sprite>("Item/None");
        }
        EW2item2.SetNativeSize();
    }
    void UpdateUI()
    {
        moneytext.text = Player.Instance.Inven.money.ToString();
        CheckEsc();
    }
    void CheckEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.PressEsc();
        }
    }
    public void EnterMap(DoorArrow doorArrow)
    {
        if(map != null)
        {
            Map gomap = map.maps[(int)doorArrow];
            if(gomap != null)
            {
                map.gameObject.SetActive(false);
                map = gomap;
                map.gameObject.SetActive(true);
                DoorArrow door;
                switch(doorArrow)
                {
                    case DoorArrow.Left:
                        door = DoorArrow.Right;
                        break;
                    case DoorArrow.Right:
                        door = DoorArrow.Left;
                        break;
                    case DoorArrow.Up:
                        door = DoorArrow.Down;
                        break;
                    case DoorArrow.Down:
                        door = DoorArrow.Up;
                        break;
                    default:
                        return;
                }
                Player.Instance.transform.position = map.transform.position + new Vector3(map.playerPos[(int)door].x, map.playerPos[(int)door].y);
            }
        }
    }
    void CameraControl()
    {
        Vector3 shake = Vector3.zero;
        if (cameraFilter_EarthQuakes.Count > 0)
        {
            List<CameraFilter_EarthQuake> filters = new List<CameraFilter_EarthQuake>();
            foreach (CameraFilter_EarthQuake cameraFilter in cameraFilter_EarthQuakes)
            {
                cameraFilter.Time -= Time.deltaTime;
                if (cameraFilter.Time <= 0)
                {
                    filters.Add(cameraFilter);
                }
                else
                {
                    Vector2 rand = Random.insideUnitCircle;
                    shake += new Vector3(rand.x * cameraFilter.X, rand.y * cameraFilter.Y, -10);
                }
            }
            foreach (CameraFilter_EarthQuake cameraFilter in filters)
            {
                cameraFilter_EarthQuakes.Remove(cameraFilter);
            }
        }
        Vector3 vector = Player.Instance.transform.position;
        Vector3 pos = vector;
        if (vector.x > map.maxvector.x + map.transform.position.x)
        {
            pos.x = map.maxvector.x + map.transform.position.x;
        }
        else if (vector.x < map.minvector.x + map.transform.position.x)
        {
            pos.x = map.minvector.x + map.transform.position.x;
        }
        if (vector.y > map.maxvector.y + map.transform.position.y)
        {
            pos.y = map.maxvector.y + map.transform.position.y;
        }
        else if (vector.y < map.minvector.y + map.transform.position.y)
        {
            pos.y = map.minvector.y + map.transform.position.y;
        }
        Camera.main.transform.position = pos + new Vector3(0,0,-10) + shake;
    }

    IEnumerator BoundTextFadeOut(TextMeshPro tmpro)
    {
        float duration = 0.7f;
        Vector3 pos = tmpro.transform.position + new Vector3(1.1f, 1f, 0);
        tmpro.DOColor(new Color(tmpro.color.r, tmpro.color.g, tmpro.color.b, 0f), duration).SetEase(Ease.InBack);
        tmpro.transform.DOMoveX(pos.x, duration);
        tmpro.transform.DOMoveY(pos.y, duration).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(duration);
        tmpro.gameObject.SetActive(false);
    }

    public void ShowBoundText(string s, Vector3 position, Color color)
    {
        GameObject gameObject = PoolManager.Instance.Init(boundtext.gameObject);
        if (gameObject != null)
        {
            TextMeshPro tmpro = gameObject.GetComponent<TextMeshPro>();
            tmpro.text = s;
            tmpro.color = color;
            gameObject.transform.position = position;
            StartCoroutine(BoundTextFadeOut(tmpro));
        }
    }

    public static string NumberComma(int num)
    {
        return string.Format("{0:#,0}", num);
    }

    void CursorChange()
    {
        cursor.transform.position = Input.mousePosition;
        if (Player.Instance.IsActable())
        {
            if (cursor.sprite != defaultcursor)
            {
                cursor.sprite = defaultcursor;
            }
        }
        else
        {
            if (cursor.sprite != uicursor)
            {
                cursor.sprite = uicursor;
            }
        }
    }

    public void DashChange()
    {
        if (Dashcount != player.DashMax)
        {
            int dc = Dashbars.Count;
            int chai = player.DashMax - dc;
            if (player.DashMax > dc)
            {
                for (int i = 0; i < chai; i++)
                {
                    Image image = GameObject.Instantiate<Image>(Dashbarbase);
                    image.transform.SetParent(Dashbarleft.transform);
                    image.GetComponent<RectTransform>().anchoredPosition = new Vector3(33 + (i + dc) * 54, 0, 0);
                    image.transform.SetAsLastSibling();
                    Dashbars.Add(image);
                }
            }
            for (int i = 0; i < dc; i++)
            {
                if (i < player.DashMax)
                {
                    Dashbars[i].gameObject.SetActive(true);
                }
                else
                {
                    Dashbars[i].gameObject.SetActive(false);
                }
            }
            Dashbarright.GetComponent<RectTransform>().anchoredPosition = new Vector3(12 + player.DashMax * 54, 0, 0);
            Dashbarright.transform.SetAsLastSibling();
            Dashcount = player.DashMax;
        }
        for (int i = 0; i < player.DashMax; i++)
        {
            if (i < player.Dash)
            {
                Dashbars[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                Dashbars[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void HealthChange()
    {
        float f = player.Hp / player.MaxHp;
        Hpbar.fillAmount = f;
        Hpwave.GetComponent<RectTransform>().anchoredPosition = new Vector3(-126f + (300 * f), 0, 0);
        Hptext.text = Mathf.Round(player.Hp) + " / " + player.MaxHp;
    }
}
