using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy2
{
    [SerializeField] GameObject bullet;
    bool canSht = true;
    [SerializeField] float delay;
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
}
