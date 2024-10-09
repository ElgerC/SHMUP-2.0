using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GeneralEnemyScript : MonoBehaviour
{
    public enum States
    {
        spawning,
        moving,
        dying,
    }

    public States state;
    [SerializeField] float health;

    protected virtual void Awake()
    {
        state = States.spawning;
    }
    public void Spawned()
    {
        state = States.moving;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        health -= 1;
        if (health <= 0)
            Death();
    }
    protected virtual void Death()
    {
        state = States.dying;
        Destroy(gameObject);
    }
}
