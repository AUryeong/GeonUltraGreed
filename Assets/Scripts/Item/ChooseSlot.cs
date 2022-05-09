using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseSlot : ClickableSlot
{
    public int slot;
    public override void OnPointerClick(PointerEventData data)
    {
        if(data.button == PointerEventData.InputButton.Left)
        {
            DialogManager.Instance.SelectChoose(slot);
        }
    }
}
