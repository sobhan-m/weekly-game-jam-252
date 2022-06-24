using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasEnemy : MonoBehaviour, IHealth
{
    [Header("Base Traits")]

    [SerializeField] float maxHealth = 100f;
    [SerializeField] float maxSpeed = 3f;

    private float currentHealth;
    private float currentSpeed;

    [Header("Gas")]

    [SerializeField] GameObject gasParticles;
    [SerializeField] float gasCyclePeriod = 5f;
    [SerializeField] float gasDamage = 10f;

    private bool isGasActive;
    private CircleCollider2D gasCollider;

    private Player player;
    private Rigidbody2D rb;
    private Animator animator;


    //=====================================
    // Unity
    //=====================================

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        gasCollider = gameObject.GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentSpeed = maxSpeed;
        currentHealth = maxHealth;
        isGasActive = false;

        InvokeRepeating("CycleGas", 0, gasCyclePeriod);
    }

    private void Update()
    {
        Chase();
        Weaken();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        IHealth objectWithHealth = collision.gameObject.GetComponent<IHealth>();
        if (objectWithHealth != null && collision.gameObject.GetComponent<GasEnemy>() == null)
        {
            objectWithHealth.TakeDamage(gasDamage * Time.deltaTime);
        }
    }

    //=====================================
    // Enemy
    //=====================================

    private void Weaken()
    {
        float healthPercent = GetCurrentHealth() / GetMaxHealth();
        currentSpeed = Mathf.Clamp(healthPercent * maxSpeed, maxSpeed / 2, maxSpeed);
    }

    private void Chase()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
            rb.velocity = new Vector2(direction.x, direction.y) * currentSpeed;
        }
    }

    //=====================================
    // Health
    //=====================================

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeHeal(float healAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healAmount);
    }

    public void TakeDamage(float damageAmount)
    {
        if (!IsDead())
        {
            animator.SetTrigger("takeDamage");

            currentHealth = Mathf.Max(0, currentHealth - damageAmount);

            if (IsDead())
            {
                Die();
            }
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public bool IsDead()
    {
        return currentHealth <= Mathf.Epsilon;
    }

    //=====================================
    // Gas
    //=====================================

    private void CycleGas()
    {
        isGasActive = !isGasActive;

        gasCollider.enabled = isGasActive;
        gasParticles.SetActive(isGasActive);
    }
}
