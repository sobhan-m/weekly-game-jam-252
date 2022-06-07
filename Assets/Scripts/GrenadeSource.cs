using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeSource : MonoBehaviour
{

    [SerializeField] GameObject grenadePrefab;
    [SerializeField] float throwSpeed;
    [SerializeField] float spinSpeed = 45f;
    private int spinDirection;

    // Start is called before the first frame update
    void Start()
    {
        spinDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowGrenade();
        }
    }

    private void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, gameObject.transform.position, gameObject.transform.rotation);

        Rigidbody2D rb = grenade.GetComponent<Rigidbody2D>();
        rb.AddForce(gameObject.transform.up * throwSpeed, ForceMode2D.Impulse);
        rb.AddTorque(spinSpeed*spinDirection);
        spinDirection = -1 * spinDirection;

        grenade.GetComponent<Grenade>().TriggerExplosion();
    }
}
