using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [System.Serializable]
    public class Waves
    {
        public Rounds[] rounds;
    }

    [System.Serializable]
    public class Rounds
    {
        public int enemy1C;
        public int enemy2C;
        public int enemy3C;
        public bool boss;
    }

    [SerializeField]
    private Waves[] waves;

    public int curWaveC = 1;
    public int curRoundC = 1;
    public int enemyC;

    [SerializeField] private GameObject Enemy1;
    [SerializeField] private GameObject Enemy2;
    [SerializeField] private GameObject Enemy3;
    [SerializeField] private GameObject Boss;

    [SerializeField] float delay;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        StartCoroutine(SpawnGroup(delay));
    }

    IEnumerator SpawnGroup(float curDelay)
    {
        Rounds curRound = waves[curWaveC - 1].rounds[curRoundC-1];
        SpawnSpcfEnemy(curRound.enemy1C, Enemy1);
        SpawnSpcfEnemy(curRound.enemy2C, Enemy2);
        SpawnSpcfEnemy(curRound.enemy3C, Enemy3);
        
        if (curRound.boss)
            SpawnSpcfEnemy(1, Boss);

        yield return new WaitForSeconds(curDelay);
    }

    private void SpawnSpcfEnemy(int amount, GameObject enemy)
    {
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < amount; i++)
        {
            if (enemy == Enemy1 || enemy == Boss)
                pos = ChooseEnemyPosHor();
            else if (enemy == Enemy2 || enemy == Enemy3)
                pos = ChooseEnemyPosVer();

            Instantiate(enemy, transform.position, Quaternion.identity);
            enemy.GetComponent<GeneralEnemyScript>().EndPos = pos;
        }
    }
    private Vector3 ChooseEnemyPosHor()
    {
        Vector2 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        return new Vector3(UnityEngine.Random.Range(lowerLeft.x, upperRight.x), upperRight.y - 1, 0);
    }
    private Vector3 ChooseEnemyPosVer()
    {
        Vector2 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        return new Vector3(lowerLeft.x,UnityEngine.Random.Range(upperRight.y,upperRight.y-3),0);
    }
}
