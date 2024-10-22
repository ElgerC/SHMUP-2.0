using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "sceneObjectScriptableObject",menuName ="ScriptableObjects")]
public class sceneObjectScriptableObject : ScriptableObject
{
    public List<Sprite> sprites = new List<Sprite>();
}
