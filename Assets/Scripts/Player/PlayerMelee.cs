using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [SerializeField] float damage = 50f;
    [SerializeField] float meleeRadius = 1f;
    [SerializeField] float meleeCooldown = 0.5f;
    [SerializeField] Transform meleeAttackPoint;

    private PauseSystem pauseSystem;
    private float timeToNextAttack;

    private void Awake()
    {
        pauseSystem = FindObjectOfType<PauseSystem>();
    }

    private void Start()
    {
        timeToNextAttack = Time.time;
    }

    private void Update()
    {
        if (pauseSystem.IsPaused())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time >= timeToNextAttack)
        {
            MeleeAttack();
        }
    }

    private void MeleeAttack()
    {
        timeToNextAttack = Time.time + meleeCooldown;


        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(meleeAttackPoint.position, meleeRadius);
        
        foreach (Collider2D target in hitTargets)
        {
            IHealth targetHealth = target.GetComponent<IHealth>();
            if (targetHealth != null && target.gameObject != gameObject)
            {
                targetHealth.TakeDamage(damage);
            }
        }
    }



}
