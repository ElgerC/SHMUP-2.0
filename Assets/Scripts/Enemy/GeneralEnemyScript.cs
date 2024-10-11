using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public Vector3 StartPos;
    private bool movingToStart = false;

    protected virtual void Awake()
    {
        state = States.spawning;
    }
    public void Spawned()
    {
        state = States.moving;
    }
    protected virtual void Update()
    {
        if (state == States.spawning)
        {
            if(!movingToStart)
            {
                StartCoroutine(movingToStartDes());
                movingToStart = true;
            }
            return;
        }
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
    IEnumerator movingToStartDes()
    {
        while (Vector3.Distance(transform.position,StartPos)<0.2)
        {
            transform.position = Vector3.Lerp(transform.position, StartPos, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        state = States.moving;
    }
}
