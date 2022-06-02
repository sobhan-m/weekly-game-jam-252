using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] Sprite ketchupSprite;
    [SerializeField] Sprite mayonnaiseSprite;
    [SerializeField] Sprite mustardSprite;

    private WeaponTypes bulletType = WeaponTypes.KETCHUP;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void ChangeBulletSprite(WeaponTypes type)
    {
        switch (type)
        {
            case WeaponTypes.KETCHUP:
                GetComponent<SpriteRenderer>().sprite = ketchupSprite;
                break;
            case WeaponTypes.MAYONNAISE:
                GetComponent<SpriteRenderer>().sprite = mayonnaiseSprite;
                break;
            case WeaponTypes.MUSTARD:
                GetComponent<SpriteRenderer>().sprite = mustardSprite;
                break;
            default:
                Debug.Log("Debug Bullet.SetBulletType(): Type does not match cases.");
                break;
        }
    }

    public void SetBulletType(WeaponTypes type)
    {
        bulletType = type;
        ChangeBulletSprite(type);
    }

    public WeaponTypes GetBulletType()
    {
        return bulletType;
    }
}
