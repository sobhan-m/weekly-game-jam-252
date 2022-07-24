using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour, IPickup
{
    [Tooltip("Where 1 = 100% means the speed is unchanged. 2 = 200% doubles the speed. 0.5 = 50% halves the speed.")]
    [SerializeField] float newSpeedPercent = 2f;
    [SerializeField] float secondsToReset = 10f;
    private Player player;
    private bool hasBeenUsed;

    private void Start()
    {
        hasBeenUsed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<Player>();
        if (player != null && !hasBeenUsed)
        {
            hasBeenUsed = true;

            Pickup(player);
            Invoke("ResetSpeed", secondsToReset);
            Disable();
        }
    }

    public void Pickup(Player player)
    {
        float currentSpeed = player.GetSpeed();
        player.SetSpeed(currentSpeed * newSpeedPercent);
    }

    private void Disable()
    {
        Destroy(GetComponent<SpriteRenderer>());
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<Collider>());
        Destroy(gameObject, secondsToReset + 0.1f);
    }

    private void ResetSpeed()
    {
        float currentSpeed = player.GetSpeed();
        player.SetSpeed(currentSpeed / newSpeedPercent);
    }
}
