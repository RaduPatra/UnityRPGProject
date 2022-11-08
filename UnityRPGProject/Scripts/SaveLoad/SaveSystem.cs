using System;
using System.IO;
using System.Net;
using Polytope;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem
{
    public const string FileDirectory = "/SaveData/";
    public const string FileName = "SaveFile.sav";
    public static Action<SaveData> OnLoad;
    public static Action OnInitSaveData;
    public static Action<SaveData> OnBeforeLoad;

    private static string SceneFilePath => Application.persistentDataPath + FileDirectory + "SceneData_" +
                                           SceneManager.GetActiveScene().name + ".sav";

    public static void Reset()
    {
        OnLoad = null;
        OnInitSaveData = null;
        OnBeforeLoad = null;
    }

    public static void Save()
    {
        var dirPath = Application.persistentDataPath + FileDirectory;
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        var filePath = dirPath + FileName;
        var jsonData = JsonUtility.ToJson(SaveData.Current, true);

        File.WriteAllText(filePath, jsonData);
    }

    public static void SaveActiveScene()
    {
        var dirPath = Application.persistentDataPath + FileDirectory;
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        var sceneJsonData = JsonUtility.ToJson(SaveData.Current.sceneData, true);
        File.WriteAllText(SceneFilePath, sceneJsonData);
    }
    public static void SaveScene(string sceneName)
    {
        var dirPath = Application.persistentDataPath + FileDirectory;
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        var sceneJsonData = JsonUtility.ToJson(SaveData.Current.sceneData, true);
        File.WriteAllText(SceneFilePath, sceneJsonData);
    }

    public static void ResetData()
    {
        var dirPath = Application.persistentDataPath + FileDirectory;
        if (!Directory.Exists(dirPath)) return;
        SaveData.ResetCurrent();
        // UnityEditor.FileUtil.DeleteFileOrDirectory(dirPath);
        // AssetDatabase.Refresh();
        // Directory.Delete(dirPath);
        DeleteDirectory(dirPath);
       /*var di = new DirectoryInfo(dirPath);
        foreach (var file in di.GetFiles())
        {
            file.Delete(); 
        }*/
        
        // Directory.Delete(dirPath, true);
        // Directory.CreateDirectory(dirPath);

        // var filePath = dirPath + FileName;
        // File.WriteAllText(filePath, string.Empty);
    }

    public static void DeleteDirectory(string target_dir)
    {
        string[] files = Directory.GetFiles(target_dir);
        string[] dirs = Directory.GetDirectories(target_dir);

        foreach (string file in files)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            File.Delete(file);
        }

        foreach (string dir in dirs)
        {
            DeleteDirectory(dir);
        }

        Directory.Delete(target_dir, false);
    }
    public static SaveData Load()
    {
        var filePath = Application.persistentDataPath + FileDirectory + FileName;
        var saveData = new SaveData();
        if (File.Exists(filePath))
        {
            saveData = GetDataFromFile<SaveData>();
            var sceneData = GetDataFromFile<SceneSpecificData>();
            saveData.sceneData = sceneData;

            OnBeforeLoad?.Invoke(saveData);
            OnLoad?.Invoke(saveData);
        }

        return saveData;
    }

    public static T GetDataFromFile<T>() where T : new()
    {
        var saveData = new T();

        var filePath = saveData switch
        {
            SaveData mainSave => Application.persistentDataPath + FileDirectory + FileName,
            SceneSpecificData specificData => SceneFilePath,
            _ => ""
        };

        if (File.Exists(filePath))
        {
            var jsonText = File.ReadAllText(filePath);
            saveData = JsonUtility.FromJson<T>(jsonText);
        }

        return saveData;
    }
}