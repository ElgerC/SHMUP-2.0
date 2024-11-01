using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomeBullet : MonoBehaviour
{
    private GameObject player;
    Rigidbody2D rb;
    [SerializeField] float speed;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = transform.GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
    }
    private void Start()
    {
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
