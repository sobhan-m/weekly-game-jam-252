using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() == null)
        {
            Debug.Log("LevelTrigger.OnTriggerEnter2D(): Nonplayer entered.");
            return;
        }

        if (levelManager == null)
        {
            Debug.Log("LevelTrigger.OnTriggerEnter2D(): No level manager found.");
            return;
        }

        SaveSystem.WriteSave();
        levelManager.LoadNextLevel();
    }
}
