using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy2 : GeneralEnemyScript
{
    private bool isMovingDown = true;
    public bool canMoveDown;
    Rigidbody2D rb;
    [SerializeField] float Speed;
    Vector3 direction;
    Vector3 startDirection;
    float rightBorder;
    float leftBorder;

    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        base.Awake();
        
        rightBorder = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;
        leftBorder = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1)).x;

        Speed = 1 + (WaveManager.instance.curWaveC / 4);
    }
    protected override void Start()
    {   
        base.Start();
        while(startDirection.z == 0)
        {
            startDirection = new Vector3(0, 0, Random.Range(-2000, 2000)).normalized;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0,0,startDirection.z*90));
        direction = new Vector2(transform.rotation.z, 0);
    }
    protected override void Movement()
    {
        rb.velocity = -direction.normalized * Speed;
        if (canMoveDown)
        {
            if (isMovingDown && transform.position.x >= rightBorder)
            {
                isMovingDown = false;
                transform.position = new Vector3(transform.position.x, transform.position.y - 1);
            }
            else if (!isMovingDown && transform.position.x <= leftBorder)
            {
                isMovingDown = true;
            }
        }
    }
}
