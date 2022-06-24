using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrenadeTracker : MonoBehaviour
{
    private GrenadeSource grenadeSource;
    [SerializeField] TextMeshProUGUI grenadeCounter;

    // Start is called before the first frame update
    void Start()
    {
        grenadeSource = FindObjectOfType<GrenadeSource>();
    }

    // Update is called once per frame
    void Update()
    {
        grenadeCounter.text = grenadeSource.GetCurrentGrenades().ToString();
    }
}
