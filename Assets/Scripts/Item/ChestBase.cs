using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBase : FButtonUnitBase
{
    public Sprite opensprite;

    public override void OnF()
    {
        this.GetComponent<SpriteRenderer>().sprite = opensprite;
    }
}
