using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
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
                StartCoroutine(DialogTyping());
            }
            else if (dialogui.activeSelf)
            {
                dialogui.SetActive(false);
                return;
            }
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
                if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space))
                {
                    typingindex = dialog.text.Length;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space))
                {
                    if(dialog.chooses != null && dialog.chooses.Count > 0)
                    {
                    }
                    dialog = null;
                    break;
                }
            }
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
    [XmlElement("Text")]
    public string text = "none";

    [XmlElement("Name")]
    public string name = "none";

    [XmlArray("ChooseList")]
    [XmlArrayItem("Choose")]
    public List<Choose> chooses = null;
}

public class Choose
{
    [XmlAttribute("Text")]
    public string text;

    [XmlElement("Dialog")]
    public List<Dialog> dialogs = new List<Dialog>();
}