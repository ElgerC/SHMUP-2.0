using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
