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

    [Header("UI - ELSE")]
    [SerializeField]
    TextMeshPro boundtext;
    [SerializeField]
    Transform uptransform;

    public DropItem dropitem;
    int Dashcount;
    Player player;

    public void CameraEarthQuake(float X, float Y, float time)
    {
        CameraFilter_EarthQuake quake = Camera.main.gameObject.AddComponent<CameraFilter_EarthQuake>();
        quake.X = X;
        quake.Y = Y;
        AutoScriptDestruct a = Camera.main.gameObject.AddComponent<AutoScriptDestruct>();
        a.targetScript = quake;
        a.time = time;
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
            gameObject.transform.SetParent(uptransform);
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
