using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy2
{
    protected override void Movement()
    {
        base.Movement();
        StartCoroutine(Shoot());
    }   
}
