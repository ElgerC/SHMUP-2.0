using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GeneralEnemyScript : MonoBehaviour
{
    public enum states
    {
        spawning,
        moving,
        dying,
    }

    public states state;
    [SerializeField] Vector2 nextPos;
    [SerializeField] float health;

    private void Awake()
    {
        state = states.spawning;
    }
    public void spawned()
    {
        state = states.moving;
    }

    private void Update()
    {
        switch (state)
        {
            case states.moving:
                    
                break;
            case states.dying:
                break;
            default: break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        health -= 1;
        if (health <= 0)
            Destroy(gameObject);
    }
}
