using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            SaveSystem.WriteSave();
            Debug.Log("SaveTrigger.OnTriggerEnter2D(): Saving game...");
        }
    }
}
