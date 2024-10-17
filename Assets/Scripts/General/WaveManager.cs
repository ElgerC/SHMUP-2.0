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
        SpawnSpcfEnemy(waves[curWaveC - 1].rounds[curRoundC].enemy1C, Enemy1);
        SpawnSpcfEnemy(waves[curWaveC - 1].rounds[curRoundC].enemy2C, Enemy2);
        SpawnSpcfEnemy(waves[curWaveC - 1].rounds[curRoundC].enemy3C, Enemy3);

        yield return new WaitForSeconds(curDelay);
    }

    private void SpawnSpcfEnemy(int amount, GameObject enemy)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }


}
