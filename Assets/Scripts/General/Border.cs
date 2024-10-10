using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{

    private float leftBorder;
    private float rightBorder;
    [SerializeField] float offSet;

    // Start is called before the first frame update
    void Awake()
    {
        Vector2 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        leftBorder = lowerLeft.x;
        rightBorder = upperRight.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < leftBorder-offSet)
        {
            transform.position = new Vector2(rightBorder+offSet-0.05f, transform.position.y);
        } else if(transform.position.x > rightBorder+offSet)
        { transform.position = new Vector2(leftBorder-offSet+0.05f, transform.position.y); }
            

    }
}
