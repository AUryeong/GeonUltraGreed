using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_Envyrok : Npc
{
    public override void OnF()
    {
        base.OnF();
        DialogManager.Instance.AddDialog(XmlManager.Instance.FindDialogCharacter("Envyrok", "1"));
    }
}
