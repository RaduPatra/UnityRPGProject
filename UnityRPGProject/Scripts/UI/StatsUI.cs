using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Users;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

[Serializable]
public class UIStatInfo
{
    public StatType statType;
    public string statName;
    public TextMeshProUGUI statText;
}

public class StatsUI : MonoBehaviour, IInitializable
{
    [SerializeField] private StatModifierEventChannel onStatModifierChangeEventChannel;
    [SerializeField] private List<UIStatInfo> stats;

    public void Initialize()
    {
        Debug.Log("stats ui init");
        onStatModifierChangeEventChannel.Listeners += UpdateStatModifier;
    }

    public void Destroy()
    {
        onStatModifierChangeEventChannel.Listeners -= UpdateStatModifier;
    }

    private void UpdateStatModifier(StatModifier statModifier)
    {
        // Debug.Log("stats ui update");
        var statInfo = stats.Find(x => x.statType == statModifier.type);
        if (statInfo == null) return;
        statInfo.statText.text = statInfo.statName + " : " + statModifier.value;
    }
}