using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeDrop : Drops
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerScript playerScript = GetComponent<PlayerScript>();
        playerScript.Upgrade();
        playerScript.ReasignSprite();
        Destroy(gameObject);
    }
}
