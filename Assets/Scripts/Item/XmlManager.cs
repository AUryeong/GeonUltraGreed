using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XmlManager : Singleton<XmlManager>
{
    public List<Item> items = new List<Item>();
    public Dictionary<string, List<DialogList>> dialogCharatcters = new Dictionary<string, List<DialogList>>();
    protected override void Awake()
    {
        if (Instance != this)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            string s = Resources.Load<TextAsset>("Xml/Item").text;
            using (StringReader stringReader = new StringReader(s))
            {
                items = ((ItemListRoot)new XmlSerializer(typeof(ItemListRoot)).Deserialize(stringReader)).itemList;
            }
            s = Resources.Load<TextAsset>("Xml/DialogCharacter").text;
            using (StringReader stringReader = new StringReader(s))
            {
                List<DialogCharatcter> charatcters = ((DialogCharacterRoot)new XmlSerializer(typeof(DialogCharacterRoot)).Deserialize(stringReader)).charatcters;
                foreach (DialogCharatcter charatcter in charatcters)
                {
                    dialogCharatcters.Add(charatcter.id, charatcter.dialogList);
                }
            }
        }
    }
    public Item FindItem(string itemname)
    {
        return items.Find((Item x) => x.ItemText == itemname);
    }
    public List<Dialog> FindDialogCharacter(string charactername, string id = "")
    {
        return dialogCharatcters[charactername].Find((DialogList x) => x.id == id).dialogs;
    }

    public class ItemListRoot
    {
        [XmlElement("Item")]
        public List<Item> itemList;
    }

    public class DialogCharacterRoot
    {
        [XmlElement("Character")]
        public List<DialogCharatcter> charatcters;
    }

    public class DialogCharatcter
    {
        [XmlAttribute("ID")]
        public string id;

        [XmlElement("DialogList")]
        public List<DialogList> dialogList;
    }
    public class DialogList
    {
        [XmlAttribute("ID")]
        public string id = "";

        [XmlElement("Dialog")]
        public List<Dialog> dialogs;
    }
}
