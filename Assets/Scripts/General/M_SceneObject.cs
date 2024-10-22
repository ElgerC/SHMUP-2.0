using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class M_SceneObject : MonoBehaviour
{
    [SerializeField] protected sceneObjectScriptableObject sceneObjData;
    private List<Sprite> sprites = new List<Sprite>();

    protected virtual void Awake()
    {
        M_SceneManager.instance.sceneObjects.Add(this);
        if (GetComponent<Sprite>() != null)
            GetComponent<SpriteRenderer>().sprite = sceneObjData.sprites[M_SceneManager.instance.sceneObjSpriteIndex];
        else
            GetComponentInChildren<SpriteRenderer>().sprite = sceneObjData.sprites[M_SceneManager.instance.sceneObjSpriteIndex];
    }
}
