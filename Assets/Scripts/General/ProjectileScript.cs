using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileScript : MonoBehaviour
{
    [SerializeField] float speedX;
    [SerializeField] float speedY;
    Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _rb.velocity = new Vector2(speedX,speedY);
        Destroy(gameObject, 5);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnHit();
        Destroy(gameObject);
    }

    protected virtual void OnHit()
    {

    }
}
