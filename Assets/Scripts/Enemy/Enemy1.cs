using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy1 : GeneralEnemyScript
{
    [SerializeField] private List<GameObject> bullets = new List<GameObject>();
    protected override void Awake()
    {
        base.Awake();
    }
    protected virtual void RandomizeBullet()
    {
        bullet = bullets[UnityEngine.Random.Range(0, bullets.Count)];
    }

    protected override void Movement()
    {
        StartCoroutine(Shoot());
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
