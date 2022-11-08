using System;
using System.Collections.Generic;
using System.Xml;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;


/*public class QuestSO : ScriptableObject
{
    public string questName;
    public List<QuestStepSO> questSteps;

    public void StartQuest(QuestManager questManager)
    {
        if (questManager.ContainsQuest(this)) return;
        questManager.AddQuest(new ActiveQuest(this));
    }
}*/
[CreateAssetMenu(fileName = "New Quest", menuName = "Quest", order = 6)]
public class QuestSO : ScriptableObject, IDatabaseItem
{
    public string questName;
    public List<QuestStepSO> questSteps;
    public List<ItemWithAttributes> rewardItems;

    // [SerializeField, ReadOnly] private string id;
    // [SerializeField, ReadOnly] private string id2;
    // public string Id => id;

    [field: SerializeField, ReadOnly]
    public string Id { get; private set; }

    private void Awake()
    {
#if UNITY_EDITOR
        Debug.Log("test questSo awake");
        Debug.Log(Id);
        if (Id.IsNullOrWhitespace())
        {
            Debug.Log("test questSo awake new id");
            Id = Guid.NewGuid().ToString();
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
#endif
        

    }
    
    
}