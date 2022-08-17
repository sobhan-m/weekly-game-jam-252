using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{

    private void Start()
    {
        ApplyLoadedData();
        WriteSave();
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

        if (File.Exists(path))
        {
            SaveModel save = JsonUtility.FromJson<SaveModel>(File.ReadAllText(path));
            Debug.Log("SaveSystem.ReadSave(): save = " + save);
            return save;
        }
        else
        {
            Debug.Log("SaveSystem.ReadSave(): No save found.");
            return null;
        }
    }

    public static string GetSavedLevel()
    {
        return SaveSystem.ReadSave().currentLevel;
    }

    public static void ApplyLoadedData()
    {
        SaveModel save = ReadSave();

        if (save == null)
        {
            return;
        }


        Player player = FindObjectOfType<Player>();
        GrenadeSource grenadeSource = FindObjectOfType<GrenadeSource>();

        if (!player || !grenadeSource)
        {
            Debug.LogError("SaveSystem.ApplyLoadedData(): Player and GrenadeSource cannot be null.");
        }

        Debug.Log("SaveSystem.ApplyLoadedData(): save = " + save);

        player.SetCurrentHealth(save.playerCurrentHealth);
        grenadeSource.SetCurrentGrenades(save.currentNumOfGrenades);
    }

    public static bool IsThereASave()
    {
        string path = Application.persistentDataPath + "save.json";

        return File.Exists(path);
    }

    public static void DeleteSave()
    {
        string path = Application.persistentDataPath + "save.json";

        File.Delete(path);
    }
}
