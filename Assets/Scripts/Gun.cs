using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform bulletSource;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] Sprite ketchupBottle;
    [SerializeField] Sprite mayoBottle;
    [SerializeField] Sprite mustardBottle;

    private WeaponTypes gunType = WeaponTypes.KETCHUP;

    private SpriteRenderer spriteRenderer;

    public float shotSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
            SetGunType(WeaponTypes.KETCHUP);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetGunType(WeaponTypes.MAYONNAISE);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetGunType(WeaponTypes.MUSTARD);
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

        if (type == WeaponTypes.KETCHUP)
        {
            spriteRenderer.sprite = ketchupBottle;
        }
        else if (type == WeaponTypes.MAYONNAISE)
        {
            spriteRenderer.sprite = mayoBottle;
        }
        else if (type == WeaponTypes.MUSTARD)
        {
            spriteRenderer.sprite = mustardBottle;
        }
    }

    public WeaponTypes GetGunType()
    {
        return gunType;
    }
}
