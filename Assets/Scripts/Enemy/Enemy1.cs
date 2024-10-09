using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    private void Update()
    {
        switch (state)
        {
            case States.moving:
                if (canSht)
                {
                    Instantiate(bullet,transform.position,Quaternion.Euler(0,0,180));
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
