using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform bulletSource;
    [SerializeField] GameObject bulletPrefab;

    private WeaponTypes gunType = WeaponTypes.KETCHUP;

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
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunType = WeaponTypes.KETCHUP;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunType = WeaponTypes.MAYONNAISE;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gunType = WeaponTypes.MUSTARD;
        }
    }

    void Shoot()
    {
        // Creating correct weapon type.
        GameObject shot = Instantiate(bulletPrefab, bulletSource.position, bulletSource.rotation);
        shot.GetComponent<Bullet>().SetBulletType(gunType);

        // Bullet movement.
        Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletSource.up * shotSpeed, ForceMode2D.Impulse);
    }

    public void SetGunType(WeaponTypes type)
    {
        gunType = type;
    }

    public WeaponTypes GetGunType()
    {
        return gunType;
    }
}
