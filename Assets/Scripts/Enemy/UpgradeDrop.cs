using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeDrop : Drops
{
    [SerializeField] private float value;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerScript playerScript = collision.GetComponent<PlayerScript>();
        playerScript.Upgrade();
        playerScript.ReasignSprite();
        playerScript.AddCharge(value,gameObject.name);
        Destroy(gameObject);
    }
}
