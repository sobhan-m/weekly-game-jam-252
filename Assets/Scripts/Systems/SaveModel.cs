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

    public override string ToString() 
    {
        return string.Format("SaveMode: playerCurrentHealth = {0}, currentNumOfGrenades = {1}, currentLevel = {2}", playerCurrentHealth, currentNumOfGrenades, currentLevel);
    }
}
