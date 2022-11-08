using System;
using UnityEngine;


public class SingletonTesting : Singleton<SingletonTesting>
{
    public SaveData saveData;
    public ItemDatabase itemDb;
    public CategoryDatabase categoryDb;

    public override void Awake()
    {
        base.Awake();

        saveData = new SaveData();

        if (itemDb == null) itemDb = Resources.Load<ItemDatabase>("ItemDatabase");
        if (categoryDb == null) categoryDb = Resources.Load<CategoryDatabase>("CategoryDatabase");
    }


    [ContextMenu("Save Data Test")]
    public void Save()
    {
        // SaveSystem.Save(saveData);
    }

    [ContextMenu("Load Data Test")]
    public void Load()
    {
        //GET DATA FROM SAVE FILE
        //LOAD IT INTO CURRENT saveData
        var loadedData = SaveSystem.Load();
        saveData = loadedData;
    }


    [ContextMenu("Debug Test")]
    public void DebugTest()
    {
    }
}