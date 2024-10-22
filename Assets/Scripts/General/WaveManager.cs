using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [System.Serializable]
    public class Waves
    {
        public Rounds[] rounds;
        public int AmountRounds;
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
    private bool waveActive = false;
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
    private void Update()
    {
        if (enemyC == 0 && !waveActive)
        {
            curWaveC++;
            curRoundC = 1;
            StartCoroutine(SpawnGroup(delay));
            waveActive = true;
        }
    }
    IEnumerator SpawnGroup(float curDelay)
    {
        Rounds curRound = waves[curWaveC - 1].rounds[curRoundC - 1];
        SpawnSpcfEnemy(curRound.enemy1C, Enemy1);
        SpawnSpcfEnemy(curRound.enemy2C, Enemy2);
        SpawnSpcfEnemy(curRound.enemy3C, Enemy3);

        if (curRound.boss)
            SpawnSpcfEnemy(1, Boss);

        yield return new WaitForSeconds(curDelay);
        if (curRoundC < waves[curWaveC - 1].AmountRounds)
        {
            curRoundC++;
            StartCoroutine(SpawnGroup(delay));
        }
        else
            waveActive = false;
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
            enemyC++;
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
        return new Vector3(lowerLeft.x, UnityEngine.Random.Range(upperRight.y, upperRight.y - 3), 0);
    }
    public void EnemyCountChange(int changeAmount)
    {
        enemyC += changeAmount;
    }
}
