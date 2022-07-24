using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHealth
{
    [Header("Basic Details")]
    [SerializeField] float speed = 5f;
    [SerializeField] float maxHealth = 100f;
    private float currentHealth;

    [Header("Dash")]
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float immunityDuration = 0.1f;
    [SerializeField] float dashCooldown = 2f;
    [SerializeField] GameObject dashTrailPrefab;
    [SerializeField] float dashTrailDuration = 0.2f;
    private float timeToNextDash;
    private bool isImmune;
    private float dashX;
    private float dashY;
    private bool shouldDash;

    private Rigidbody2D rb;
    private Animator animator;
    private Camera mainCamera;
    private PauseSystem pauseSystem;

    //=============================
    // Unity
    //=============================

    private void Awake()
    {
        pauseSystem = FindObjectOfType<PauseSystem>();
    }

    void Start()
    {
        currentHealth = maxHealth;

        timeToNextDash = Time.time;
        isImmune = false;
        shouldDash = false;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (pauseSystem.IsPaused())
        {
            return;
        }

        TriggerDash();
    }

    private void FixedUpdate()
    {
        Move();
        Aim();
        Dash();
    }

    //=============================
    // Player
    //=============================

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(x * speed, y * speed);
    }

    private void Aim()
    {
        // Find angle.
        Vector3 mouse = Input.mousePosition;
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(gameObject.transform.position);
        float angleInRad = Mathf.Atan2(mouse.y - screenPosition.y, mouse.x - screenPosition.x);
        float angleInDeg = angleInRad * Mathf.Rad2Deg - 90;

        // Rotate the torch.
        rb.rotation = angleInDeg;
    }

    private void TriggerDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > timeToNextDash)
        {
            dashX = Input.GetAxisRaw("Horizontal");
            dashY = Input.GetAxisRaw("Vertical");

            shouldDash = true;
        }
    }

    private void Dash()
    {
        if (shouldDash)
        {
            animator.SetTrigger("triggerDash");

            TriggerDashTrails();
            CycleDashImmunity();
            Invoke("CycleDashImmunity", immunityDuration);

            rb.MovePosition(transform.position + (new Vector3(dashX, dashY) * dashSpeed));


            timeToNextDash = Time.time + dashCooldown;
            shouldDash = false;
        }
    }

    private void CycleDashImmunity()
    {
        isImmune = !isImmune;
        Physics2D.IgnoreLayerCollision(6, 8, isImmune);
    }

    private void TriggerDashTrails()
    {
        GameObject dashTrails = Instantiate(dashTrailPrefab, transform);
        Destroy(dashTrails, dashTrails.GetComponent<ParticleSystem>().main.duration);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    //=============================
    // Health
    //=============================

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
        if (!IsDead() && !isImmune)
        {
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
}
