using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using System.Xml.Serialization;
using TMPro;

public class DialogManager : Singleton<DialogManager>
{
    Queue<Dialog> dialogs = new Queue<Dialog>();
    Dialog dialog;
    [SerializeField]
    GameObject dialogui;

    [SerializeField]
    TextMeshProUGUI dialogname;

    [SerializeField]
    TextMeshProUGUI dialogtext;

    [SerializeField]
    GameObject chooseuibuttom;

    [SerializeField]
    GameObject chooseuimiddle;

    [SerializeField]
    GameObject chooseuiup;

    List<GameObject> objlist = new List<GameObject>();

    Coroutine coroutine;

    void Start()
    {
    }
    public void AddDialog(List<Dialog> dialogs)
    {
        foreach(Dialog dialog in dialogs)
        {
            this.dialogs.Enqueue(dialog);
        }
    }
    public void AddDialog(string name, string text)
    {
        dialogs.Enqueue(new Dialog()
        {
            name = name,
            text = text
        });
    }
    public List<Dialog> FindDialog(List<Dialog> dialogs, string id)
    {
        List<Dialog> result = new List<Dialog>();
        foreach(Dialog dialog in dialogs)
        {
            if(dialog.id == id)
            {
                result.Add(dialog);
            }
            else if (dialog.chooses != null && dialog.chooses.Count > 0)
            {
                foreach(Choose choose in dialog.chooses)
                {
                    List<Dialog> dialog2 = FindDialog(choose.dialogs, id);
                    if(dialog2 != null)
                    {
                        result.AddRange(dialog2);
                    }
                }
            }
        }
        return result;
    }

    void Update()
    {
        if (dialog == null)
        {
            if (dialogs.Count > 0)
            {
                if (!dialogui.activeSelf)
                {
                    dialogui.SetActive(true);
                }
                dialog = dialogs.Dequeue();
                dialogname.text = dialog.name;
                if (dialog.chooses != null && dialog.chooses.Count > 0)
                {
                    chooseuibuttom.SetActive(true);
                    chooseuibuttom.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = dialog.chooses[dialog.chooses.Count - 1].text;
                    chooseuibuttom.transform.GetChild(0).GetComponent<ChooseSlot>().slot = dialog.chooses.Count-1;
                    objlist.Add(chooseuibuttom);
                    if (dialog.chooses.Count > 2)
                    {
                        for (int i = dialog.chooses.Count - 1; i > 1; i--)
                        {
                            GameObject obj = PoolManager.Instance.Init(chooseuimiddle);
                            obj.SetActive(true);
                            obj.GetComponent<RectTransform>().SetParent(chooseuibuttom.GetComponent<RectTransform>());
                            obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 96 * (dialog.chooses.Count - i), 0);
                            obj.GetComponent<RectTransform>().GetChild(1).GetComponent<TextMeshProUGUI>().text = dialog.chooses[i-1].text;
                            obj.GetComponent<RectTransform>().GetChild(0).GetComponent<ChooseSlot>().slot = i-1;
                            objlist.Add(obj);
                        }
                    }
                    chooseuiup.SetActive(true);
                    chooseuiup.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 96 * (dialog.chooses.Count - 1), 0);
                    chooseuiup.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = dialog.chooses[0].text;
                    chooseuiup.transform.GetChild(0).GetComponent<ChooseSlot>().slot = 0;
                    objlist.Add(chooseuiup);
                }
                else
                {
                    chooseuibuttom.SetActive(false);
                }
                Dialog.DialogEvent dialogEvent = dialog.dialogEvent;
                if (dialogEvent != null)
                {
                    dialogEvent.Invoke(Dialog.DialogEventType.Start);
                }
                coroutine = StartCoroutine(DialogTyping());
            }
            else if (dialogui.activeSelf)
            {
                dialogui.SetActive(false);
                return;
            }
        }
        
    }
    public void SelectChoose(int index)
    {
        if(dialog.chooses.Count > index)
        {
            AddDialog(dialog.chooses[index].dialogs);
            foreach(GameObject obj in objlist)
            {
                obj.SetActive(false);
            }
            objlist.Clear();
            Dialog.DialogEvent dialogEvent = dialog.dialogEvent;
            if (dialogEvent != null)
            {
                dialogEvent.Invoke(Dialog.DialogEventType.End);
            }
            if (coroutine != null)
                StopCoroutine(coroutine);
            dialog = null;
        }
    }

    IEnumerator DialogTyping()
    {
        int typingindex = 0;
        float typingtime = 0;
        while (true)
        {
            if (typingindex != dialog.text.Length)
            {
                typingtime += Time.deltaTime;
                if(typingtime >= 0.05f)
                {
                    typingtime = 0;
                    typingindex++;
                    if (dialog.text[typingindex - 1] == '<')
                    {
                        while(dialog.text[typingindex - 1] != '>')
                        {
                            typingindex++;
                        }
                        typingindex++;
                    }
                }
                if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space)) && typingindex != 0)
                {
                    typingindex = dialog.text.Length;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space))
                {
                    if (dialog.chooses == null || dialog.chooses.Count <= 0)
                    {
                        Dialog.DialogEvent dialogEvent = dialog.dialogEvent;
                        if (dialogEvent != null)
                        {
                            dialogEvent.Invoke(Dialog.DialogEventType.End);
                        }
                        dialog = null;
                        break;
                    }
                }
            }
            typingindex = (typingindex > dialog.text.Length) ? dialog.text.Length : typingindex;
            dialogtext.text = dialog.text.Substring(0, typingindex);
            yield return null;
        }
    }

    public bool Dialoging()
    {
        return dialog != null || dialogs.Count > 0;
    }

}
public class Dialog
{
    [XmlAttribute("ID")]
    public string id = "";

    [XmlElement("Text")]
    public string text = "none";

    [XmlElement("Name")]
    public string name = "none";

    [XmlArray("ChooseList")]
    [XmlArrayItem("Choose")]
    public List<Choose> chooses = null;

    [XmlIgnore]
    public DialogEvent dialogEvent;

    public delegate void DialogEvent(DialogEventType eventType);

    public enum DialogEventType
    {
        Start,
        End
    }
}

public class Choose
{

    [XmlAttribute("Text")]
    public string text;

    [XmlElement("Dialog")]
    public List<Dialog> dialogs = new List<Dialog>();
}

