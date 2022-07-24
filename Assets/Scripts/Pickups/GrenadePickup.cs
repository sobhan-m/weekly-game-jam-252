using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour, IPickup
{
    [SerializeField] int numOfGrenadesToAdd = 3;

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
        GrenadeSource grenadeSource = player.gameObject.GetComponentInChildren<GrenadeSource>();
        if (grenadeSource != null)
        {
            grenadeSource.AddGrenades(numOfGrenadesToAdd);
        }
    }
}
