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
    float rightBorder;
    float leftBorder;

    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        base.Awake();
        direction = new Vector2(transform.rotation.z, 0);
        rightBorder = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;
        leftBorder = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1)).x;
    }
    protected override void Update()
    {
        base.Update();
        if(state == States.moving)
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
}
