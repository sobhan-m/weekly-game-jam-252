using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasGrenade : MonoBehaviour
{
    [SerializeField] float explosionRadius = 2f;
    [SerializeField] float damagePerSecond = 20f;
    [SerializeField] float secondsToMaxRadius = 0.5f;
    [SerializeField] float secondsToEnd = 2f;
    [SerializeField] float secondsToTrigger = 0.25f;

    [SerializeField] GameObject particles;

    private CircleCollider2D gasCollider;
    private Rigidbody2D rb;

    private float timeToTrigger;
    private float explosionRadiusChangeRate;
    private bool isTriggered;

    private PauseSystem pauseSystem;

    //===============================
    // Unity
    //===============================

    void Awake()
    {
        gasCollider = gameObject.GetComponent<CircleCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        pauseSystem = FindObjectOfType<PauseSystem>();

        isTriggered = false; // Must be in Awake() because it is immediately triggered when thrown.
    }

    private void Start()
    {
        particles.SetActive(false);
        explosionRadiusChangeRate = explosionRadius / secondsToMaxRadius;
        timeToTrigger = Time.time + secondsToTrigger;
    }

    void Update()
    {
        if (pauseSystem.IsPaused())
        {
            return;
        }

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
        if (isTriggered && HasPassedTriggerTime() && gasCollider.radius < explosionRadius)
        {
            gasCollider.radius += explosionRadiusChangeRate * Time.deltaTime;
        }

        if (isTriggered && HasPassedTriggerTime() && particles.activeSelf == false)
        {
            particles.SetActive(true);
        }
    }

    private bool HasPassedTriggerTime()
    {
        return Time.time > timeToTrigger;
    }
}
