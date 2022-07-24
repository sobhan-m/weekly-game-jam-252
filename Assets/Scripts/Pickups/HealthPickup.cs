using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour, IPickup
{
    [SerializeField] float healAmount = 25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Pickup(player);
            Destroy(gameObject);
        }
    }

    public void Pickup(Player player)
    {
        Heal(player);
    }


    private void Heal(IHealth personToBeHealed)
    {
        personToBeHealed.TakeHeal(healAmount);
    }
}
