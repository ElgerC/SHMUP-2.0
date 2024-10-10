using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //General variables
    public Rigidbody2D rb;

    //Movement variables
    public float moveSpeed;
    private Vector2 _moveDirection;

    //Bullet variables
    [SerializeField] GameObject bullet;
    [SerializeField] bool canFire = false;

    //Health variables
    [SerializeField] float health;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
    }
    //Movement
    public void Movement(InputAction.CallbackContext ctx)
    {
        _moveDirection = ctx.action.ReadValue<Vector2>();
        rb.velocity = new Vector2(_moveDirection.x * moveSpeed, 0);
    }
    //Shooting
    public void Fire(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
            Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        health -= 1;
        if (health <= 0)
            Destroy(gameObject);
    }
}
