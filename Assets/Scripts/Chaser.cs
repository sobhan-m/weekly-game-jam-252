using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField] float maxSpeed = 3f;
    [SerializeField] float damage = 10f;
    private float currentSpeed;

    private Player player;
    private Enemy currentEnemy;
    private Rigidbody2D rb;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        currentEnemy = gameObject.GetComponent<Enemy>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = maxSpeed;
        
    }

    private void Update()
    {
        Chase();
        Weaken();
    }

    private void Weaken()
    {
        float healthPercent = currentEnemy.GetCurrentHealth() / currentEnemy.GetMaxHealth();
        currentSpeed = Mathf.Clamp(healthPercent * maxSpeed, maxSpeed/2, maxSpeed);
    }

    private void Chase()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            rb.velocity = new Vector2(direction.x, direction.y) * currentSpeed;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IHealth objectWithHealth = collision.gameObject.GetComponent<IHealth>();
        if (objectWithHealth != null && collision.gameObject.GetComponent<Chaser>() == null)
        {
            objectWithHealth.TakeDamage(damage * Time.deltaTime);
        }
    }
}
