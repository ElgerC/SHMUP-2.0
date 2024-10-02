using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : ProjectileScript
{
    protected override void OnHit()
    {
        Debug.Log("hit");
    }
}
