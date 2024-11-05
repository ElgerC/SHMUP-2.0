using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : ProjectileScript
{
    private Collider2D[] colliders;
    public float blastRange;
    [SerializeField] private LayerMask enemyLayer;
    protected override void OnHit()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, blastRange,enemyLayer);
        for(int i = 0; i < colliders.Length; i++)
        {
            Debug.Log(colliders[i].name);
            colliders[i].GetComponent<GeneralEnemyScript>().TakeDmg(1);
        }
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, blastRange);
    }
}
