using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_SceneObject : MonoBehaviour
{
    [SerializeField] protected sceneObjectScriptableObject sceneObjData;
    M_SceneManager sceneManager;
    SpriteRenderer spriteRenderer;
    protected virtual void Start()
    {
        sceneManager = M_SceneManager.instance;
        sceneManager.sceneObjects.Add(gameObject);

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
    private void OnDestroy()
    {
        sceneManager.sceneObjects.Remove(gameObject);
    }


}
