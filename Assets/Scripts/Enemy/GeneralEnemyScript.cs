using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class GeneralEnemyScript : M_SceneObject
{
    //General states
    private Animator animator;

    //State variables
    public enum States
    {
        spawning,
        moving,
        dying,
    }
    public States state;
    private bool movingToStart = false;

    //Health
    public float health;
    public float curHealth;
    public float dmgPercentage = 1;

    //StartMovement
    public Vector3 EndPos;
    
    //Shooting variables
    protected bool canSht = true;
    [SerializeField] protected float delay;
    [SerializeField] protected GameObject bullet;

    //Deaths variables
    [SerializeField] EnemyDrops EnemyDropsScriptObj;
    private List<GameObject> enemyDrops;
    [SerializeField] float dropChance;
    [SerializeField] protected int Value;

    protected virtual void Awake()
    {
        enemyDrops = EnemyDropsScriptObj.Drops;
        animator = GetComponent<Animator>();
        curHealth = health;
        state = States.spawning;
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
        TakeDmg(1);      
    }
    protected virtual void Death()
    {
        WaveManager.instance.EnemyCountChange(-1);
        if (Random.Range(100, 0) <= dropChance)
        {
            GameObject obj = enemyDrops[Random.Range(0, enemyDrops.Count)];
            Instantiate(obj, transform.position, Quaternion.identity);
        }
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + Value);
        Debug.Log(PlayerPrefs.GetInt("Score") + Value);
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
    protected virtual IEnumerator Shoot()
    {
        if (canSht)
        {
            StartCoroutine(Shooting());
            float projectiles = 1 + Mathf.Round(WaveManager.instance.curWaveC / 2);
            for (int i = 0; i < projectiles; i++)
            {
                Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 180));
                yield return new WaitForSeconds(0.3f);
            }                
            
        }
    }
    protected virtual IEnumerator Shooting()
    {
        canSht = false;
        yield return new WaitForSeconds(delay);
        canSht = true;
    }
    public void TakeDmg(float amount)
    {
        curHealth -= amount*dmgPercentage;
        animator.SetTrigger("DmgFlash");
        if (curHealth <= 0)
            Death();
    }
}
