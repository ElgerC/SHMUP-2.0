using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_SceneImage : M_SceneObject
{
    private Image image;
    protected override void Start()
    {
        image = GetComponent<Image>();
        sceneManager = M_SceneManager.instance;
        sceneManager.sceneObjects.Add(gameObject);

        ReasignSprite();
    }
    public override void ReasignSprite()
    {
        image.sprite = sceneObjData.sprites[sceneManager.sceneObjSpriteIndex];
    }

}
