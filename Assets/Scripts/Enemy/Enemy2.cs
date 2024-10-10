using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy2 : GeneralEnemyScript
{
    [SerializeField] Vector2 startPos;
    [SerializeField] Vector2 endPos;
    private bool isMoving = false; 
    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position,endPos,0.05f);
    }
}
