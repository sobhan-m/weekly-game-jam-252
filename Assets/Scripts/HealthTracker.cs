using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour
{

    private Player player;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.GetCurrentHP();
    }
}
