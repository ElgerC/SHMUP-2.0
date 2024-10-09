using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : GeneralEnemyScript
{
    [SerializeField] Vector2 startPos;
    [SerializeField] Vector2 endPos;
    private bool isMoving = false; 
    private void Update()
    {
        if(state == States.moving && !isMoving)
        {
            isMoving = true;
            StartCoroutine(Movement());
        }
        IEnumerator Movement()
        {
            yield return new WaitForSeconds(1);
        }
    }
}
