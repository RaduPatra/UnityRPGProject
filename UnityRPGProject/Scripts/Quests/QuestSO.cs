using System;
using System.Collections.Generic;
using System.Xml;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest", order = 6)]
public class QuestSO : ScriptableObject, IDatabaseItem
{
    public string questName;
    public List<QuestStepSO> questSteps;
    public List<ItemWithAttributes> rewardItems;

    [field: SerializeField, ReadOnly]
    public string Id { get; private set; }

    private void Awake()
    {
#if UNITY_EDITOR
        if (Id.IsNullOrWhitespace())
        {
            Id = Guid.NewGuid().ToString();
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
#endif
        
    }
    
}