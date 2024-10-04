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
    [SerializeField] bool canFire = false;

    public void Movement(InputAction.CallbackContext ctx)
    {
        _moveDirection = ctx.action.ReadValue<Vector2>();
        rb.velocity = new Vector2(_moveDirection.x * moveSpeed, 0);
    }
    public void Fire(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
            Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }
}
