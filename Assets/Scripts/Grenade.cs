using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] float explosionRadius = 2f;
    [SerializeField] float damagePerSecond = 20f;
    [SerializeField] float secondsToMaxRadius = 0.5f;
    [SerializeField] float secondsToEnd = 2f;
    [SerializeField] float stopSensitivity = 0.25f;

    [SerializeField] GameObject particles;

    private CircleCollider2D gasCollider;
    private Rigidbody2D rb;

    private float explosionRadiusChangeRate;
    private bool isTriggered;

    //===============================
    // Unity
    //===============================

    void Awake()
    {
        gasCollider = gameObject.GetComponent<CircleCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        isTriggered = false; // Must be in Awake() because it is immediately triggered when thrown.
    }

    private void Start()
    {
        particles.SetActive(false);
        explosionRadiusChangeRate = explosionRadius / secondsToMaxRadius;
    }

    void Update()
    {
        Expand();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IHealth creature = collision.gameObject.GetComponent<IHealth>();
        DealDamage(creature);
    }

    //===============================
    // Grenade
    //===============================

    public void TriggerExplosion()
    {
        isTriggered = true;
        Destroy(gameObject, secondsToEnd + secondsToMaxRadius);
    }

    private void DealDamage(IHealth creature)
    {
        if (creature != null)
        {
            creature.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }

    private void Expand()
    {
        if (isTriggered && HasStoppedMoving() && gasCollider.radius < explosionRadius)
        {
            Debug.Log("Is Expanding");
            gasCollider.radius += explosionRadiusChangeRate * Time.deltaTime;
        }

        if (isTriggered && HasStoppedMoving() && particles.activeSelf == false)
        {
            particles.SetActive(true);
        }
    }

    private bool HasStoppedMoving()
    {
        return rb.velocity.magnitude <= Mathf.Epsilon + stopSensitivity;
    }
}
