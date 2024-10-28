using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDrop : Drops
{
    [SerializeField] private float ShieldDur;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<PlayerScript>().Shield(ShieldDur);
        Destroy(gameObject);
    }
}
