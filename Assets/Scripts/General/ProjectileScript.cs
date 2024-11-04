using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileScript : MonoBehaviour
{
    [SerializeField] float speedX;
    [SerializeField] float speedY;
    Rigidbody2D _rb;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _rb.velocity = transform.up*speedY;
        Destroy(gameObject, 5);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        OnHit();
        Destroy(gameObject);
    }

    protected virtual void OnHit()
    {

    }
}
