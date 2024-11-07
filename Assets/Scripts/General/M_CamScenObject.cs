using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_CamScenObject : M_SceneObject
{
    [SerializeField] private List<PostProces> Shaders = new List<PostProces>();
    protected override void Start()
    {
        sceneManager = M_SceneManager.instance;
        sceneManager.sceneObjects.Add(gameObject);

        ReasignSprite();
    }
    public override void ReasignSprite()
    {
        switch (sceneManager.sceneObjSpriteIndex)
        {
            case 0:
                Shaders[0].enabled = true;
                Shaders[1].enabled = false;
                break;
            case 1:
                Shaders[0].enabled = true;
                Shaders[1].enabled = true;
                break;
            case 2:
                Shaders[0].enabled = false;
                Shaders[1].enabled = false;
                break;
        }
    }
}
