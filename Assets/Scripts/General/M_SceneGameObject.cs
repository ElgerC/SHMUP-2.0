using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_SceneGameObject : M_SceneObject
{
    [SerializeField] private List<GameObject> objects = new List<GameObject>();
    protected override void Start()
    {
        sceneManager = M_SceneManager.instance;
        sceneManager.sceneObjects.Add(gameObject);

        ReasignSprite();
    }
    public override void ReasignSprite()
    {
        int m_index = sceneManager.sceneObjSpriteIndex;
        for (int i = 0; i < objects.Count; i++)
        {
            if (i != m_index)
                objects[i].SetActive(false);
            else
                objects[i].SetActive(true);
        }
    }
}
