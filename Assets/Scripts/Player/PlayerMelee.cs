using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [SerializeField] float damage = 50f;
    [SerializeField] float meleeRadius = 1f;
    [SerializeField] float meleeCooldown = 0.5f;
    [SerializeField] Transform meleeAttackPoint;
    [SerializeField] float pusbackForce = 5f;

    private PauseSystem pauseSystem;
    private float timeToNextAttack;
    private Animator animator;

    private void Awake()
    {
        pauseSystem = FindObjectOfType<PauseSystem>();
        animator = GetComponent<Animator>();
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
        animator.SetTrigger("triggerPunch");


        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(meleeAttackPoint.position, meleeRadius);
        
        foreach (Collider2D target in hitTargets)
        {
            IHealth targetHealth = target.GetComponent<IHealth>();
            if (targetHealth != null && target.gameObject != gameObject)
            {
                targetHealth.TakeDamage(damage);

                // Knockback. Doesn't work at the moment.
                /*Rigidbody2D targetRB = target.gameObject.GetComponent<Rigidbody2D>();
                if (targetRB == null || targetRB.gameObject == gameObject)
                {
                    return;
                }
                Vector2 differenceVector = gameObject.transform.position - target.transform.position;
                targetRB.AddForce(differenceVector * pusbackForce * 100, ForceMode2D.Impulse);*/
            }
            

            
        }
    }



}
