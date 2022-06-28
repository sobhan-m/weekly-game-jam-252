using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour, IHealth
{
    [Header("Base Traits")]

    [SerializeField] float maxHealth = 100f;
    [SerializeField] float speed = 3f;

    private float currentHealth;

    [Header("Melee Attack")]

    [SerializeField] float meleeDamage = 30f;
    [SerializeField] float attacksPerSecond = 2f;
    [SerializeField] float meleeRadius = 0.5f;
    [SerializeField] Transform meleeDamagePoint;

    private float nextAttackTime;

    private Player player;
    private Rigidbody2D rb;
    private Animator animator;
    private PauseSystem pauseSystem;


    //=====================================
    // Unity
    //=====================================

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        pauseSystem = FindObjectOfType<PauseSystem>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        nextAttackTime = Time.time;
    }

    private void Update()
    {
        if (pauseSystem.IsPaused())
        {
            return;
        }

        Chase();
        MeleeAttack();
    }

    //=====================================
    // Enemy
    //=====================================

    private void Chase()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
            rb.velocity = new Vector2(direction.x, direction.y) * speed;
        }
    }

    private float FindDistanceToPlayer()
    {
        if (player == null)
            return 9999999f;

        return (player.transform.position - transform.position).magnitude;
    }

    private float FindMeleeRange()
    {
        return (meleeDamagePoint.position - transform.position).magnitude + meleeRadius;
    }

    private void MeleeAttack()
    {
        if (FindDistanceToPlayer() <= FindMeleeRange() && Time.time >= nextAttackTime)
        {
            Collider2D[] hitTargets = Physics2D.OverlapCircleAll(meleeDamagePoint.position, meleeRadius);

            foreach(Collider2D target in hitTargets)
            {
                IHealth targetHealth = target.GetComponent<IHealth>();
                if (targetHealth != null && target.gameObject != gameObject)
                {
                    animator.SetTrigger("MeleeAttack");

                    targetHealth.TakeDamage(meleeDamage);
                    nextAttackTime = Time.time + 1f/attacksPerSecond;
                }
            }
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
            currentHealth = Mathf.Max(0, currentHealth - damageAmount);

            animator.SetTrigger("GetHurt");

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
}
