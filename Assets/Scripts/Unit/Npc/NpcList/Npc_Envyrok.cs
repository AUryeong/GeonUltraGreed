using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_Envyrok : Npc
{
    bool quest = false;
    List<EnemyBase> enemyList = new List<EnemyBase>();
    protected override void Start()
    {
        base.Start();
        enemyList = (new List<EnemyBase>(GameManager.Instance.map.GetComponentsInChildren<EnemyBase>())).FindAll((EnemyBase x) => x.gameObject.activeSelf);
    }
    public override void OnF()
    {
        List<Dialog> list;
        enemyList = (new List<EnemyBase>(GameManager.Instance.map.GetComponentsInChildren<EnemyBase>())).FindAll((EnemyBase x) => x.gameObject.activeSelf);
        if (enemyList.FindAll((EnemyBase x) => x.Agro).Count > 0)
        {
            GameManager.Instance.ShowCenterText("주변의 적을 처치하고 말을 걸어보자.", Color.yellow);
        }
        else 
        {
            if (!quest)
            {
                list = XmlManager.Instance.FindDialogCharacter("Envyrok", "1");
                DialogManager.Instance.FindDialog(list, "KillPlayer")[0].dialogEvent = KillPlayer;
                foreach (Dialog dialog in DialogManager.Instance.FindDialog(list, "AcceptQuest"))
                {
                    dialog.dialogEvent = AcceptQuest;
                }
            }
            else if (enemyList.Count > 0)
            {
                list = XmlManager.Instance.FindDialogCharacter("Envyrok", "2");
            }
            else
            {
                list = XmlManager.Instance.FindDialogCharacter("Envyrok", "3");
            }
            DialogManager.Instance.AddDialog(list);
        }
    }

    public void KillPlayer(Dialog.DialogEventType eventType)
    {
        if(eventType == Dialog.DialogEventType.End)
        {
            Player.Instance.Hp = 0;
            GameManager.Instance.HealthChange();
            GameManager.Instance.CameraEarthQuake(1, 1, 1);
            Player.Instance.gameObject.SetActive(false);
        }
    }

    public void AcceptQuest(Dialog.DialogEventType eventType)
    {
        if (eventType == Dialog.DialogEventType.End)
        {
            quest = true; 
            if (enemyList.Count <=0)
            {
                DialogManager.Instance.AddDialog(XmlManager.Instance.FindDialogCharacter("Envyrok", "3"));
            }
        }
    }
}
