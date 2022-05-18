using UnityEngine;
using UnityEngine.Rendering;


public class SaveSystemManager : MonoBehaviour
{
    public static SaveSystemManager Instance { get; set; }
    public ItemDatabase itemDb;
    public CategoryDatabase categoryDb;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        // itemDb = Resources.Load<ItemDatabase>("ItemDatabase");
        // categoryDb = Resources.Load<CategoryDatabase>("CategoryDatabase");
    }

    [ContextMenu("Save Data Test")]
    public void Save()
    {
        SaveSystem.Save(SaveData.Current);
    }

    [ContextMenu("Load Data Test")]
    public void Load()
    {
        var loadedData = SaveSystem.Load();
        SaveData.Current = loadedData;
    }

    // [ContextMenu("Reset Event Test")]
    public void ResetTest()
    {
        SaveSystem.Reset();
    }

    [ContextMenu("Debug Test")]
    public void DebugTest()
    {
    }
}
