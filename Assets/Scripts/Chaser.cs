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

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = maxSpeed;
        player = FindObjectOfType<Player>();
        currentEnemy = gameObject.GetComponent<Enemy>();
    }

    private void Update()
    {
        Chase();
        Weaken();
    }

    private void Weaken()
    {
        float healthPercent = currentEnemy.GetCurrentHP() / currentEnemy.GetMaxHP();
        currentSpeed = Mathf.Clamp(healthPercent * maxSpeed, maxSpeed/2, maxSpeed);
    }

    private void Chase()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.TakeDamage(damage);
        }
    }
}
