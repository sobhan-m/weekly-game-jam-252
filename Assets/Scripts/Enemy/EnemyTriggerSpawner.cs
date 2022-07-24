using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemiesToSpawn;

    private void Awake()
    {
        foreach (GameObject enemy in enemiesToSpawn)
        {
            enemy.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() == null)
            return;

        foreach (GameObject enemy in enemiesToSpawn)
        {
            if (enemy != null)
            {
                enemy.SetActive(true);
            }
        }
    }
}
