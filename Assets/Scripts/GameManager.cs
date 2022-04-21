using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
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

	public DropItem dropitem;
	int Dashcount;
	Player player;

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

	public static string NumberComma(int num)
    {
		return string.Format("{0:#,0}", num);
    }

	void CursorChange()
	{
		cursor.transform.position = Input.mousePosition;
		if(Player.Instance.IsActable())
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
		if(Dashcount != player.DashMax)
        {
			int dc = Dashbars.Count;
			int chai = player.DashMax - dc;
			if (player.DashMax > dc)
            {
				for(int i = 0; i < chai; i++)
                {
					Image image = GameObject.Instantiate<Image>(Dashbarbase);
					image.transform.SetParent(Dashbarleft.transform);
					image.GetComponent<RectTransform>().anchoredPosition = new Vector3(33 + (i+dc) * 54, 0, 0);
					image.transform.SetAsLastSibling();
					Dashbars.Add(image);
				}
            }
			for(int i = 0; i< dc; i++)
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
		for(int i = 0; i < player.DashMax; i++)
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
		Hptext.text = player.Hp + " / " + player.MaxHp;
    }
}
