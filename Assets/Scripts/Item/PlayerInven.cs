using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInven : MonoBehaviour
{
    [SerializeField]
    private ItemSlot copy;

    [SerializeField]
    GameObject[] gameObjects;

    void Start()
    {
        foreach (GameObject obj in gameObjects)
        {
            ItemSlot copies = GameObject.Instantiate<ItemSlot>(copy, obj.transform);
            copies.transform.localPosition = Vector3.zero;
            copies.ShowItem(null);
            if (!copies.transform.parent.gameObject.name.Contains("Inven"))
            {
                copies.isinven = false;
            }
        }
    }

}
