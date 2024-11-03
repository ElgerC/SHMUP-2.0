using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class M_SceneManager : MonoBehaviour
{
    public List<GameObject> sceneObjects = new List<GameObject>();

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
        foreach (var obj in sceneObjects)
        {
            if (obj.tag != "Player")
            {
                obj.GetComponent<M_SceneObject>().ReasignSprite();
            }
            else
            {
                obj.GetComponent<PlayerScript>().ReasignSprite();               
            }
                
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
