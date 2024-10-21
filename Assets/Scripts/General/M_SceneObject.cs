using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class M_SceneObject : MonoBehaviour
{
    [SerializeField] protected Sprite Look1;
    [SerializeField] protected Sprite Look2;
    [SerializeField] protected Sprite Look3;

    protected virtual void Awake()
    {
        M_SceneManager.instance.sceneObjects.Add(this);
    }
}
