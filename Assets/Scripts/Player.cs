using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float maxHP = 100f;

    private float hp;

    private Rigidbody2D playerRigidBody;

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;

        playerRigidBody = GetComponent<Rigidbody2D>();

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
        Aim();
    }

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

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public float GetCurrentHP()
    {
        return hp;
    }
}
