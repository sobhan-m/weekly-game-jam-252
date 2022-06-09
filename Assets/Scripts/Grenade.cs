using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] float explosionRadius;
    [SerializeField] float damage;
    [SerializeField] float secondsToBlow;
    [SerializeField] float secondsAfterExplosion = 0.25f;
    [SerializeField] float stopSensitivity = 0.25f;

    [SerializeField] GameObject particles;

    private CircleCollider2D explosion;
    private Rigidbody2D rb;

    private float explosionRadiusChangeRate;

    private bool shouldExplode;
    private bool hasExploded;
    private bool isTriggered;

    private List<Enemy> enemiesAffected;


    // Start is called before the first frame update
    void Awake()
    {
        explosion = gameObject.GetComponent<CircleCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        enemiesAffected = new List<Enemy>();

        explosionRadiusChangeRate = explosionRadius / secondsToBlow;

        shouldExplode = false;
        hasExploded = false;
        isTriggered = false;

        particles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Expand();
        Explode();
    }

    public void TriggerExplosion()
    {
        isTriggered = true;
    }

    private void Explode()
    {
        if (shouldExplode)
        {
            DealDamage();
            Destroy(gameObject, secondsAfterExplosion);
            shouldExplode = false;
            hasExploded = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
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
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }  
        }
    }

    private void Expand()
    {
        if (isTriggered && rb.velocity.magnitude <= Mathf.Epsilon + stopSensitivity && explosion.radius < explosionRadius)
        {
            explosion.radius += explosionRadiusChangeRate * Time.deltaTime;
            particles.SetActive(true);
        }
        else if (!hasExploded && explosion.radius >= explosionRadius)
        {
            shouldExplode = true;
        }
    }
}
