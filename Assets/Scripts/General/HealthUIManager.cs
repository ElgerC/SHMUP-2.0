using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Hearts = new List<GameObject>();

    public void ChangeUIHealth(float health)
    {
        if (health > 0)
        {
            for (int i = 0; i < Hearts.Count; i++)
            {
                Hearts[i].SetActive(false);
            }
            for (int i = 0; i < health; i++)
            {
                Hearts[i].SetActive(true);
            }
        }
    }
}
