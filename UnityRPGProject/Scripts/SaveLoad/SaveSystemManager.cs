using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class SaveSystemManager : MonoBehaviour
{
    public static SaveSystemManager Instance { get; set; }
    public ItemDatabase itemDb;
    public CategoryDatabase categoryDb;
    public QuestDatabase questDb;
    public DialogueDatabase dialogueDb;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        // itemDb = Resources.Load<ItemDatabase>("ItemDatabase");
        // categoryDb = Resources.Load<CategoryDatabase>("CategoryDatabase");
    }

    private void Start()
    {
        Debug.Log("load");
        SaveSystem.OnInitSaveData?.Invoke();

        //if save is empty, first initialize the data, then save it
        var saveData = SaveSystem.GetDataFromFile<SaveData>();
        if (saveData.isEmpty)
        {
            Debug.Log("save data is empty");
            SaveData.Current.isEmpty = false;
            SaveData.Current.lastLoadedScene = SceneManager.GetActiveScene().name;
            Save();
        }

        var sceneData = SaveSystem.GetDataFromFile<SceneSpecificData>();
        if (sceneData.isEmpty)
        {
            Debug.Log("scene data is empty");
            SaveData.Current.sceneData.isEmpty = false;
            SaveSystem.SaveActiveScene();
        }

        Load();
    }

    [ContextMenu("Save Data Test")]
    public void Save()
    {
        // SaveData.Current.lastLoadedScene = SceneManager.GetActiveScene().name;
        SaveSystem.Save();
        SaveSystem.SaveActiveScene();
    }

    [ContextMenu("Load Data Test")]
    public void Load()
    {
        var loadedData = SaveSystem.Load();
        SaveData.Current = loadedData;
    }

    [ContextMenu("Reload Scene Test")]
    public void Reload()//call event channel from transition instead
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // var loadedData = SaveSystem.Load();
        // SaveData.Current = loadedData;
    }

    private void OnDestroy()
    {
        Debug.Log("save on dest");
        SaveSystem.Reset();
    }

    [ContextMenu("Debug Test")]
    public void DebugTest()
    {
    }

    public void LoadNextScene()
    {
        
    }
}