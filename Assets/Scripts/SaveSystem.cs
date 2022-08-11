using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    private static bool isLoadingFromSave = false;

    private void Start()
    {
        if (isLoadingFromSave)
        {
            ApplyLoadedData();
        }
    }

    public static void WriteSave()
    {
        Player player = FindObjectOfType<Player>();
        GrenadeSource grenadeSource = FindObjectOfType<GrenadeSource>();

        if (!player || !grenadeSource)
        {
            Debug.LogError("SaveSystem.WriteSave(): Player and GrenadeSource cannot be null.");
        }

        SaveModel save = new SaveModel(player, grenadeSource);

        string jsonText = JsonUtility.ToJson(save);

        string path = Application.persistentDataPath + "save.json";

        File.WriteAllText(path, jsonText);

        Debug.Log("SaveSystem.WriteSave(): Saved Data " + jsonText);
        Debug.Log("SaveSystem.WriteSave(): Save At: " + path);
    }

    public static SaveModel ReadSave()
    {
        string path = Application.persistentDataPath + "save.json";

        SaveModel save =  JsonUtility.FromJson<SaveModel>(File.ReadAllText(path));

        return save;
    }

    public static string GetSavedLevel()
    {
        isLoadingFromSave = true;
        return SaveSystem.ReadSave().currentLevel;
    }

    public static bool IsLoadingFromSave()
    {
        return isLoadingFromSave;
    }

    public static void ApplyLoadedData()
    {
        isLoadingFromSave = false;

        SaveModel save = ReadSave();

        Player player = FindObjectOfType<Player>();
        GrenadeSource grenadeSource = FindObjectOfType<GrenadeSource>();

        if (!player || !grenadeSource)
        {
            Debug.LogError("SaveSystem.ApplyLoadedData(): Player and GrenadeSource cannot be null.");
        }

        player.SetCurrentHealth(save.playerCurrentHealth);
        grenadeSource.SetCurrentGrenades(save.currentNumOfGrenades);
    }
}
