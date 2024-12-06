using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemy : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float health = 100f; 
    public float explosionRadius = 5f;
    public float explosionDamage = 10f;
    public GameObject explosionEffect; 

    private void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        transform.position += direction * speed * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die(); 
        }
    }

    private void Die()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        DealExplosionDamage();

        Destroy(gameObject);
    }

    private void DealExplosionDamage()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= explosionRadius)
        {
            player.GetComponent<Health>().TakeDamage(10); 
        }
    }
}