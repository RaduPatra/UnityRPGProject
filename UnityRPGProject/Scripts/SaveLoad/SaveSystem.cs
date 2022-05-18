using System;
using System.IO;
using UnityEngine;

public class SaveSystem
{
    public const string FileDirectory = "/SaveData/";
    public const string FileName = "SaveFile.sav";
    public static Action<SaveData> OnLoad;

    public static void Save(SaveData data)
    {
        var dirPath = Application.persistentDataPath + FileDirectory;
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        var filePath = dirPath + FileName;
        var jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, jsonData);
    }

    public static SaveData Load()
    {
        var filePath = Application.persistentDataPath + FileDirectory + FileName;
        var saveData = new SaveData();
        if (File.Exists(filePath))
        {
            var jsonText = File.ReadAllText(filePath);
            saveData = JsonUtility.FromJson<SaveData>(jsonText);
            OnLoad?.Invoke(saveData);
        }
        return saveData;
    }

    public static void Reset()
    {
        OnLoad = null;
    }
}