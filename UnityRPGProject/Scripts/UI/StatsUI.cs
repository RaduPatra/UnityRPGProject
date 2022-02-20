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
    [SerializeField] private CharacterAttributes characterAttributes;
    [SerializeField] private TextMeshProUGUI attackStatText;
    [SerializeField] private TextMeshProUGUI defenceStatText;
    [SerializeField] private VoidEventChannel onStatsChangeEventChannel;
    [SerializeField] private StatModifierEventChannel onStatModifierChangeEventChannel;
    [SerializeField] private List<UIStatInfo> stats;

    private void OnEnable()
    {
        onStatsChangeEventChannel.VoidListeners += UpdateStats;
        onStatModifierChangeEventChannel.Listeners += UpdateStatModifier;
        // UpdateStats();
    }
    private void UpdateStats()
    {
        attackStatText.text = "Attack: " + characterAttributes.characterAttackDamage;
        defenceStatText.text = "Defence: " + characterAttributes.characterDefence;
    }
    
    private void UpdateStatModifier(StatModifier statModifier)
    {
        var statInfo = stats.Find(x => x.statType == statModifier.type);
        statInfo.statText.text = statInfo.statName + " : " + statModifier.value;
    }
}