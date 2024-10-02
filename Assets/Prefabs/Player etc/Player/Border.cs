using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{

    private float leftBorder;
    private float rightBorder;
    private float topBorder;
    private float bottomBorder;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        leftBorder = lowerLeft.x;
        rightBorder = upperRight.x;
        bottomBorder = lowerLeft.y;
        topBorder = upperRight.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, leftBorder, rightBorder), Mathf.Clamp(transform.position.y, bottomBorder, topBorder));

    }
}
