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

    bool hasStart = false;
    protected override void Awake()
    {
        health = (4 * WaveManager.instance.curWaveC) + 12;
        base.Awake();
        Vector2 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        leftBorder = lowerLeft.x;
        rightBorder = upperRight.x;
        rb = GetComponent<Rigidbody2D>();
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
        if (!hasStart)
        {
            ChangeDirection(StrtDirect);
            hasStart = true;
        }
        if (canSht)
            StartCoroutine(Shoot());
    }
    protected override IEnumerator Shoot()
    {
        return base.Shoot();
    }
    private void ChangeDirection(float x)
    {
        direction = new Vector3(x, 0, 0);
        rb.velocity = direction;
    }
}
