using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    private Vector2 _moveDirection;
    [SerializeField] GameObject bullet;

    public InputActionReference move;
    public void Movement()
    {
        _moveDirection = move.action.ReadValue<Vector2>();
        rb.velocity = new Vector2(_moveDirection.x*moveSpeed, 0); 
    }
    public void Fire()
    {
        Instantiate(bullet,new Vector2(transform.position.x,transform.position.y),Quaternion.identity);
    }
}
