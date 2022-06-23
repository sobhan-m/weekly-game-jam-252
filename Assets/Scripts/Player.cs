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
    private float timeToNextDash;
    private bool isImmune;

    private Rigidbody2D playerRigidBody;

    private Camera mainCamera;

    //=============================
    // Unity
    //=============================

    void Start()
    {
        currentHealth = maxHealth;

        timeToNextDash = Time.time;
        isImmune = false;

        playerRigidBody = GetComponent<Rigidbody2D>();

        mainCamera = Camera.main;
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

        playerRigidBody.velocity = new Vector2(x * speed, y * speed);
    }

    private void Aim()
    {
        // Find angle.
        Vector3 mouse = Input.mousePosition;
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(gameObject.transform.position);
        float angleInRad = Mathf.Atan2(mouse.y - screenPosition.y, mouse.x - screenPosition.x);
        float angleInDeg = angleInRad * Mathf.Rad2Deg - 90;

        // Rotate the torch.
        playerRigidBody.MoveRotation(angleInDeg);
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > timeToNextDash)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            playerRigidBody.AddForce(new Vector2(x, y) * dashSpeed, ForceMode2D.Impulse);
            
            timeToNextDash = Time.time + dashCooldown;

            StartCoroutine(TriggerDashImmunity());
        }
        

    }

    private IEnumerator TriggerDashImmunity()
    {
        isImmune = true;
        Physics2D.IgnoreLayerCollision(6, 8, true);

        yield return new WaitForSeconds(immunityDuration);

        isImmune = false;
        Physics2D.IgnoreLayerCollision(6, 8, false);
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
