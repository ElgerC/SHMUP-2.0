using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDropsScriptableObj", menuName = "ScriptableObjects/Player")]
public class PlayerScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class version
    {
        public List<Sprite> versionSprites = new List<Sprite>();
    }
    public List<version> playerVersions = new List<version>();
}
