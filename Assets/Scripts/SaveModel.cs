using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveModel
{
    public float playerCurrentHealth;
    public int currentNumOfGrenades;
    public string currentLevel;

    public SaveModel(Player player, GrenadeSource grenadeSource)
    {
        playerCurrentHealth = player.GetCurrentHealth();
        currentNumOfGrenades = grenadeSource.GetCurrentGrenades();
        currentLevel = SceneManager.GetActiveScene().name;
    }
}
