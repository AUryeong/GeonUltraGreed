using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ClickableSlot : UISlot
{
    [FormerlySerializedAs("Left Click")]
    [SerializeField]
    private Button.ButtonClickedEvent OnLeftClick = new Button.ButtonClickedEvent();

    public override void OnPointerClick(PointerEventData data)
    {
        if (data.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick.Invoke();
        }
    }
}
