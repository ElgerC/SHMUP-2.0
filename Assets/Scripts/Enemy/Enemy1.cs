using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy1 : GeneralEnemyScript
{
    //Shooting variables
    [SerializeField] float delay;
    bool canSht = true;
    [SerializeField] GameObject bullet;

    //animation
    Animator animator;
    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        base.Awake();
    }
    protected override void Update()
    {
        base.Update();
        switch (state)
        {
            case States.spawning:
                break;
            case States.moving:
                if (canSht)
                {
                    float projectiles = 1 + Mathf.Round(0);
                    for (int i = 0; i < projectiles; i++)
                        Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 180));
                    StartCoroutine(Shooting());
                }
                break;
            case States.dying:
                break;
            default: break;
        }
    }
    IEnumerator Shooting()
    {
        canSht = false;
        yield return new WaitForSeconds(delay);
        canSht = true;
    }
    protected override void Death()
    {
        animator.SetTrigger("Die");
        state = States.dying;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
