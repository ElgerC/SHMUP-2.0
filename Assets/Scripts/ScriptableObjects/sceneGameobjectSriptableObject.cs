using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "sceneGameobjectSriptableObject", menuName = "ScriptableObjects/SceneGameObjects")]
public class sceneGameobjectSriptableObject : ScriptableObject
{
    public List<GameObject> Objects = new List<GameObject>();
}
