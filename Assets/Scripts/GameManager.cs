using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

	[SerializeField]
	Image cursor;

	[SerializeField]
	Texture2D invisiblecursor;

	protected override void Awake()
	{
		Cursor.SetCursor(invisiblecursor, Vector2.zero, CursorMode.Auto);
	}
	void Update()
	{
		cursor.transform.position = Input.mousePosition;
	}

}
