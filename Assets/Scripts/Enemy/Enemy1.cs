using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy1 : GeneralEnemyScript
{
    //animation
    Animator animator;
    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        base.Awake();
    }

    protected override void Movement()
    {
        Shoot();
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
