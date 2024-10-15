using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : GeneralEnemyScript
{
    [SerializeField] private Vector3 direction;
    Rigidbody2D rb;
    public int StrtDirect;

    private float leftBorder;
    private float rightBorder;

    [SerializeField] float offset;

    [SerializeField] private List<GameObject> bullets;
    protected override void Awake()
    {
        base.Awake();
        Vector2 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        leftBorder = lowerLeft.x;
        rightBorder = upperRight.x;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {

        ChangeDirection(StrtDirect);
    }
    protected override void Update()
    {
        base.Update();
        if (direction.x > 0)
        {
            if (transform.position.x > (rightBorder - offset))
                ChangeDirection(-1);
        }
        else if (direction.x < 0)
        {
            if (transform.position.x < (leftBorder + offset))
                ChangeDirection(1);
        }

    }
    protected override void Movement()
    {
        
        if (canSht)
        {
            if (bullet == bullets[0])
            {
                bullet = bullets[1];
            }
            else if (bullet == bullets[1])
            {
                bullet = bullets[0];
            }
        }
        Shoot();

    }
    private void ChangeDirection(float x)
    {
        direction = new Vector3(x, 0, 0);
        rb.velocity = direction;
    }
}
