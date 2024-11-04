using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private GeneralEnemyScript[] enemieScripts;
    private GameObject[] enemies;

    private Vector3 nearestPos;
    private void Awake()
    {
        enemieScripts = FindObjectsOfType<GeneralEnemyScript>();
        for (int i = 0; i < enemieScripts.Length; i++)
        {
            enemies[i] = enemieScripts[i].gameObject;
        }        
        findNearestEnemy();
    }
    private void findNearestEnemy()
    {
        nearestPos = enemies[0].transform.position;
        for (int i = 0;i < enemies.Length; i++)
        {
            Vector3 posOption = enemies[i].transform.position;
            if (Vector3.Distance(posOption,transform.position) <= Vector3.Distance(transform.position, nearestPos))
            {
                nearestPos = posOption;
            }
        }
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nearestPos, 1);
    }
}
