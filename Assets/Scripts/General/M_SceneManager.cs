using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class M_SceneManager : MonoBehaviour
{
    public List<M_SceneObject> sceneObjects = new List<M_SceneObject>();

    public static M_SceneManager instance;

    public int sceneObjSpriteIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void ChangeScene()
    {
        foreach (var item in sceneObjects)
        {
            item.ReasignSprite();
        }
    }

    public void IndexIncrease(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            sceneObjSpriteIndex++;
            if (sceneObjSpriteIndex > 2)
                sceneObjSpriteIndex = 0;
            ChangeScene();
        }
    }
    public void IndexDecrease(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            sceneObjSpriteIndex--;
            if (sceneObjSpriteIndex < 0)
                sceneObjSpriteIndex = 2;
            ChangeScene();
        }
    }
}
