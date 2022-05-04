using System;
using TMPro;
using UnityEngine;

public class ItemInfoUI : MonoBehaviour, IInitializable
{
    [SerializeField] private AttributeBaseSO itemNameAttribute;
    [SerializeField] private AttributeBaseSO itemStatsAttribute;
    [SerializeField] private TextMeshProUGUI textPrefab;
    [SerializeField] private Transform itemInfoTextParent;

    public void Initialize()
    {
    }

    public void ShowItemInfo(ItemWithAttributes item)
    {
        itemInfoTextParent.Clear();
        
        var itemName = item.GetAttribute<StringAttributeData>(itemNameAttribute)?.value;
        if (itemName != null)
        {
            var textObject = Instantiate(textPrefab, itemInfoTextParent);
            textObject.text = "Name: " + itemName;
        }

        var itemStats = item.GetAttribute<StatModifierListData>(itemStatsAttribute)?.value;
        if (itemStats != null)
        {
            foreach (var statModifier in itemStats)
            {
                var textObject = Instantiate(textPrefab, itemInfoTextParent);
                textObject.text = statModifier.type + " : " + statModifier.value;
            }
        }
    }

    public void HideItemInfo()
    {
        itemInfoTextParent.Clear();
    }

}