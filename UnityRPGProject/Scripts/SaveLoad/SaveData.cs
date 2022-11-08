using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData //todo - save & add player health data
{
    private static SaveData current;

    public static SaveData Current
    {
        get { return current ??= new SaveData(); }
        set => current = value;
    }

    public static void ResetCurrent()
    {
        current = null;
    }
    

    public InventoryContainer inventorySaveData = new InventoryContainer();


    // public InventoryContainer2 inventorySaveData2;//test
    public InventoryContainer hotbarInventorySaveData = new InventoryContainer();

    public SerializableDictionary<string, InventorySlot> equipmentArmorSave =
        new SerializableDictionary<string, InventorySlot>();

    public SerializableDictionary<string, InventorySlot> equippedWeaponSave =
        new SerializableDictionary<string, InventorySlot>();

    // public List<string> collectedGroundItems = new List<string>();

    // public SerializableDictionary<string, GroundItemData> droppedGroundItems =
    //     new SerializableDictionary<string, GroundItemData>();

    public List<string> lootedChests = new List<string>();
    public List<string> openedDoors = new List<string>();

    public float healthData;

    public SerializableDictionary<string, float> healthValueData =
        new SerializableDictionary<string, float>();

    public SerializableDictionary<string, bool> characterDeathData =
        new SerializableDictionary<string, bool>();

    /*
    public Vector3 lastCheckPointPositionData;
    */


    public bool isEmpty = true;

    // public List<QuestSaveData> activeQuestsData = new List<QuestSaveData>();
    // public List<QuestSaveData> completedQuestsData = new List<QuestSaveData>();


    public List<ActiveQuest> activeQuestsData = new List<ActiveQuest>();
    public List<ActiveQuest> completedQuestsData = new List<ActiveQuest>();


    /*public SerializableDictionary<string, string> defaultDialogueNodesData =
        new SerializableDictionary<string, string>();*/

    /*public SerializableDictionary<string, CheckPointSaveData> checkpointData =
        new SerializableDictionary<string, CheckPointSaveData>();*/

    public List<string> nonRespawnableEnemies = new List<string>();

    public string followerId = "";

    /*public SerializableDictionary<string, FollowerData> followersData =
        new SerializableDictionary<string, FollowerData>();*/

    public Vector3 lastCheckPointPositionData;

    // private SceneSpecificData sceneData;
    // public SerializableDictionary<string, GroundItemData> TestSceneGroundItem => sceneData.droppedGroundItems;

    [NonSerialized] public SceneSpecificData sceneData = new SceneSpecificData();

    public string lastLoadedScene;
}

[Serializable]
public class SceneSpecificData
{
    //ground 

    public SerializableDictionary<string, GroundItemData> droppedGroundItems =
        new SerializableDictionary<string, GroundItemData>();

    public SerializableDictionary<string, CheckPointSaveData> checkpointData =
        new SerializableDictionary<string, CheckPointSaveData>();
    
    public SerializableDictionary<string, FollowerData> followersData =
        new SerializableDictionary<string, FollowerData>();

    public SerializableDictionary<string, string> defaultDialogueNodesData =
        new SerializableDictionary<string, string>();
    // public Vector3 lastCheckPointPositionData;

    public bool isEmpty = true;
}

[Serializable]
public class FollowerData
{
    public Vector3 followerPosition;
    public Quaternion followerRotation;
}