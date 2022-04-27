using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

[Serializable]
public class UIStatInfo
{
    public StatType statType;
    public string statName;
    public TextMeshProUGUI statText;
}

public class StatsUI : MonoBehaviour
{
    [SerializeField] private StatModifierEventChannel onStatModifierChangeEventChannel;
    [SerializeField] private List<UIStatInfo> stats;

    private void Awake()
    {
        Debug.Log("stats ui awake");
        // foreach (var stat in stats)
        // {
        //     stat.statText.text = stat.statName + " : " + "0";
        // }
        // onStatModifierChangeEventChannel.Listeners += UpdateStatModifier;
    }

    private void Start()
    {
        Debug.Log("stats ui start");
    }

    public void Setup()
    {
        Debug.Log("stats ui setup");
        // foreach (var stat in stats)
        // {
        //     stat.statText.text = stat.statName + " : " + "0";
        // }
        onStatModifierChangeEventChannel.Listeners += UpdateStatModifier;
    }

    private void UpdateStatModifier(StatModifier statModifier)
    {
        // Debug.Log("stats ui update");
        var statInfo = stats.Find(x => x.statType == statModifier.type);
        statInfo.statText.text = statInfo.statName + " : " + statModifier.value;
    }
}