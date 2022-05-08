using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : FButtonUnitBase
{
    SpriteRenderer spriteRenderer;
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void Update()
    {
        base.Update();
        float x = transform.position.x - Player.Instance.transform.position.x;
        spriteRenderer.flipX = (x > 0);
    }
}
