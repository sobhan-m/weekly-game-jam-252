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
        Debug.Log("SaveSystem.Start(): isLoadingFromSpace = " + isLoadingFromSave);
        if (isLoadingFromSave)
        {
            ApplyLoadedData();
        }
        WriteSave();
    }

    public static void WriteSave()
    {
        isLoadingFromSave = true;

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

        Debug.Log("SaveSystem.ReadSave(): save = " + save);

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
