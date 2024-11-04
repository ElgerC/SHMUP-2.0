using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : ProjectileScript
{
    [SerializeField] private float Health;
    [SerializeField] private float spriteFadeNum;
    private SpriteRenderer spriteRenderer;
    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteFadeNum = spriteRenderer.color.a / Health;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Health--;
        spriteRenderer.color = new Vector4(0, 162, 255, spriteRenderer.color.a - spriteFadeNum);
        if(Health <= 0)
        {
            base.OnTriggerEnter2D(collision);
        }
    }
}
