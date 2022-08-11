using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeSource : MonoBehaviour
{

    [SerializeField] GameObject grenadePrefab;
    [SerializeField] float throwSpeed;
    [SerializeField] float spinSpeed = 45f;
    [SerializeField] int maxGrenades = 3;

    private int spinDirection;
    private int currentGrenades;

    private PauseSystem pauseSystem;

    private void Awake()
    {
        pauseSystem = FindObjectOfType<PauseSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        spinDirection = 1;
        currentGrenades = maxGrenades;
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseSystem.IsPaused())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowGrenade();
        }
    }

    private void ThrowGrenade()
    {
        if (currentGrenades > 0)
        {
            GameObject grenade = Instantiate(grenadePrefab, gameObject.transform.position, gameObject.transform.rotation);

            Rigidbody2D rb = grenade.GetComponent<Rigidbody2D>();
            rb.AddForce(gameObject.transform.up * throwSpeed, ForceMode2D.Impulse);
            rb.AddTorque(spinSpeed * spinDirection);
            spinDirection = -1 * spinDirection;

            grenade.GetComponent<GasGrenade>().TriggerExplosion();

            --currentGrenades;
        }
    }

    public void AddGrenades(int numToAdd)
    {
        currentGrenades = Mathf.Min(maxGrenades, currentGrenades + numToAdd);
    }

    public int  GetCurrentGrenades()
    {
        return currentGrenades;
    }

    public void SetCurrentGrenades(int num)
    {
        currentGrenades = Mathf.Clamp(num, 0, maxGrenades);
    }
}
