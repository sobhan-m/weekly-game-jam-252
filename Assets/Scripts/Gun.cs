using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform bulletSource;
    [SerializeField] GameObject bulletPrefab;

    public float shotSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject shot = Instantiate(bulletPrefab, bulletSource.position, bulletSource.rotation);
        Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletSource.up * shotSpeed, ForceMode2D.Impulse);
    }
}
