using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    [SerializeField] float throwForce;
    [SerializeField] float distanceThrown;
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionRadiusChangeRate;
    [SerializeField] float damage;
    [SerializeField] float secondsToBlow;

    private CircleCollider2D explosion;

    private bool isExpanding;

    private List<Enemy> enemiesAffected;


    // Start is called before the first frame update
    void Start()
    {
        explosion = gameObject.GetComponent<CircleCollider2D>();
        explosion.enabled = false;
        isExpanding = false;

        enemiesAffected = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isExpanding && explosion.radius < explosionRadius)
        {
            explosion.radius += explosionRadiusChangeRate;
        }
    }

    private void Explode()
    {
        explosion.enabled = true;
        isExpanding = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (!enemiesAffected.Contains(enemy))
            {
                enemiesAffected.Add(enemy);
            }
        }
    }

    private void DealDamage()
    {
        foreach (Enemy enemy in enemiesAffected)
        {
            enemy.TakeDamage(damage);
        }
    }
}
