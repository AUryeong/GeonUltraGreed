using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ClickableSlot : UISlot
{
    [SerializeField]
    private Button.ButtonClickedEvent OnLeftClick = new Button.ButtonClickedEvent();

    private Action action;


    public override void OnPointerClick(PointerEventData data)
    {
        if (data.button == PointerEventData.InputButton.Left)
        {
            if(OnLeftClick != null)
            {
                OnLeftClick.Invoke();
            }
            if (action != null)
            {
                action.Invoke();
            }
        }
    }
}
