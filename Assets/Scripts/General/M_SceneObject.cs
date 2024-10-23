using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class M_SceneObject : MonoBehaviour
{
    [SerializeField] protected sceneObjectScriptableObject sceneObjData;
    private List<Sprite> sprites = new List<Sprite>();
    M_SceneManager sceneManager;
    SpriteRenderer spriteRenderer;
    protected virtual void Start()
    {
        sceneManager = M_SceneManager.instance;
        sceneManager.sceneObjects.Add(this);

        if (GetComponent<Sprite>() != null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        else
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        ReasignSprite();
    }
    public void ReasignSprite()
    {
        spriteRenderer.sprite = sceneObjData.sprites[sceneManager.sceneObjSpriteIndex];        
    }

    
}
