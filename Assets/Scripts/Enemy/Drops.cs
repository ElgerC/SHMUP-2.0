using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Drops : MonoBehaviour
{
    private float ScreenBot;
    private Vector3 EndPos;
    private Vector3 startPos;
    [SerializeField] private float tickDelay;
    [SerializeField] private float tickDistance;
    private int ticks = 1;
    private void Awake()
    {
        startPos = transform.position;
        ScreenBot = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).y;
        EndPos = new Vector3(transform.position.x,ScreenBot -1,transform.position.z);
    }
    private void Start()
    {
        StartCoroutine(MoveDown());
    }
    private IEnumerator MoveDown()
    {
        ticks = 1;
        while (transform.position != EndPos) 
        {
            transform.position = Vector3.Lerp(startPos,EndPos,tickDistance*ticks);
            ticks++;
            yield return new WaitForSeconds(tickDelay);
        }
        Destroy(gameObject);
    }
}
