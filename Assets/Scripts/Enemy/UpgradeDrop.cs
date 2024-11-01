using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeDrop : Drops
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<PlayerScript>().Upgrade();
        Destroy(gameObject);
    }
}
