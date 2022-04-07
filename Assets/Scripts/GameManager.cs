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

	[Header("[UI - HP]")]
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

	int dashcount;

	[Header("[Player]")]
	[SerializeField]
	Player player;

	protected override void Awake()
	{
	}
	void Update()
	{
		cursor.transform.position = Input.mousePosition;
		HealthChange();
	}

	public void DashChange()
    {
		if(dashcount != player.maxdash)
        {
			int dc = Dashbars.Count;
			int chai = player.maxdash - dc;
			if (player.maxdash > dc)
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
                if (i < player.maxdash)
                {
					Dashbars[i].gameObject.SetActive(true);
                }
                else
				{
					Dashbars[i].gameObject.SetActive(false);
				}
            }
			Dashbarright.GetComponent<RectTransform>().anchoredPosition = new Vector3(12 + player.maxdash * 54, 0, 0);
			Dashbarright.transform.SetAsLastSibling();
			dashcount = player.maxdash;
        }
		for(int i = 0; i < player.maxdash; i++)
        {
            if (i < player.dash)
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
		float f = player.hp / player.maxhp;
		Hpbar.fillAmount = f;
		Hpwave.GetComponent<RectTransform>().anchoredPosition = new Vector3(-126f + (300 * f), 0, 0);
		Hptext.text = player.hp + " / " + player.maxhp;
    }
}
