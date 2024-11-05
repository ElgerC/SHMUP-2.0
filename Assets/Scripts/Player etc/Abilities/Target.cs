using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private GeneralEnemyScript[] enemieScripts;

    private Vector3 nearestPos;
    private GameObject target;
    public float upgradeIndex;

    private void findNearestEnemy()
    {
        enemieScripts = FindObjectsOfType<GeneralEnemyScript>();
        Debug.Log(enemieScripts.Length);
        if (enemieScripts.Length > 0)
        {
            float healthTarget = 0;

            for (int i = 0; i < enemieScripts.Length; i++)
            {
                if (enemieScripts[i].curHealth >= healthTarget)
                {
                    if (target != null)
                        target.GetComponent<GeneralEnemyScript>().dmgPercentage = 1;
                    healthTarget = enemieScripts[i].curHealth;
                    target = enemieScripts[i].gameObject;
                    enemieScripts[i].dmgPercentage = 1f + (0.1f*upgradeIndex);
                }
            }
        }

    }
    private void FixedUpdate()
    {
        if (target == null)
        findNearestEnemy();
        if (target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.1f*upgradeIndex);
    }
}
