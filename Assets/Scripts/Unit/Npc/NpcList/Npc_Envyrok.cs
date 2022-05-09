using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_Envyrok : Npc
{
    bool quest = false;
    public override void OnF()
    {
        List<Dialog> list;
        if (!quest)
        {
            list = XmlManager.Instance.FindDialogCharacter("Envyrok", "1");
            DialogManager.Instance.FindDialog(list, "KillPlayer")[0].dialogEvent = KillPlayer;
            foreach(Dialog dialog in DialogManager.Instance.FindDialog(list, "AcceptQuest"))
            {
                dialog.dialogEvent = AcceptQuest;
            }
        }
        else if ((new List<EnemyBase>(GameManager.Instance.map.GetComponentsInChildren<EnemyBase>())).FindAll((EnemyBase x) => x.gameObject.activeSelf).Count > 0)
        {
            list = XmlManager.Instance.FindDialogCharacter("Envyrok", "2");
        }
        else
        {
            list = XmlManager.Instance.FindDialogCharacter("Envyrok", "3");
        }
        DialogManager.Instance.AddDialog(list);
    }

    public void KillPlayer(Dialog.DialogEventType eventType)
    {
        if(eventType == Dialog.DialogEventType.End)
        {
            Player.Instance.Hp = 0;
            GameManager.Instance.HealthChange();
            GameManager.Instance.CameraEarthQuake(1, 1, 1);
        }
    }

    public void AcceptQuest(Dialog.DialogEventType eventType)
    {
        if (eventType == Dialog.DialogEventType.End)
        {
            quest = true;
        }
    }
}
