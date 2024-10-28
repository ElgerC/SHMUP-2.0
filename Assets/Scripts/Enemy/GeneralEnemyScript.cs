using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class GeneralEnemyScript : M_SceneObject
{
    public enum States
    {
        spawning,
        moving,
        dying,
    }

    public States state;
    [SerializeField] float health;
    private Vector3 StartPos;
    public Vector3 EndPos;
    private bool movingToStart = false;
    protected bool canSht = true;
    [SerializeField] float delay;
    [SerializeField] protected GameObject bullet;

    //Drops variables
    [SerializeField] EnemyDrops EnemyDropsScriptObj;
    private List<GameObject> enemyDrops;
    [SerializeField] float dropChance;

    protected virtual void Awake()
    {
        enemyDrops = EnemyDropsScriptObj.Drops;
        state = States.spawning;
        StartPos = transform.position;
    }
    public void Spawned()
    {
        state = States.moving;
    }
    protected virtual void Update()
    {
        switch (state)
        {
            case States.spawning:
                if (!movingToStart)
                {
                    StartCoroutine(movingToStartDes());
                    movingToStart = true;
                }
                break;
            case States.moving:
                Movement();
                break;
            case States.dying:
                break;
        }
    }
    protected virtual void Movement()
    {

    } 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        health -= 1;
        if (health <= 0)
            Death();
    }
    protected virtual void Death()
    {
        WaveManager.instance.EnemyCountChange(-1);
        if (Random.Range(100, 0) <= dropChance)
        {
            GameObject obj = enemyDrops[Random.Range(0, enemyDrops.Count)];
            Instantiate(obj, transform.position, Quaternion.identity);
        }
        state = States.dying;
        Destroy(gameObject);
    }
    IEnumerator movingToStartDes()
    {
        while (Vector3.Distance(transform.position, EndPos) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, EndPos, 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        transform.position = EndPos;
        state = States.moving;
    }
    protected virtual void Shoot()
    {
        if (canSht)
        {
            float projectiles = 1 + Mathf.Round(0);
            for (int i = 0; i < projectiles; i++)
                Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 180));
            StartCoroutine(Shooting());
        }
    }
    protected virtual IEnumerator Shooting()
    {
        canSht = false;
        yield return new WaitForSeconds(delay);
        canSht = true;
    }
}
