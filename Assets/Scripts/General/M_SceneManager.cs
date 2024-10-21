using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_SceneManager : MonoBehaviour
{
    public List<M_SceneObject> sceneObjects = new List<M_SceneObject>();

    public static M_SceneManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = new M_SceneManager();
        }

        
        foreach (var item in sceneObjects)
        {
            Debug.Log(item.gameObject.name);
        }
    }
    private void Update()
    {
        sceneObjects = FindObjectsOfType<M_SceneObject>().ToList();
    }
}
