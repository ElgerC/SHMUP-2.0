using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDropsScriptableObj", menuName = "ScriptableObjects/EnemyDrops")]
public class EnemyDrops : ScriptableObject
{
    public List<GameObject> Drops = new List<GameObject>();
}
