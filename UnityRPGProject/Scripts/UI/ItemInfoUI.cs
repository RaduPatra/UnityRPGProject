using System;
using TMPro;
using UnityEngine;
using ExtensionMethods;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour, IInitializable
{
    [SerializeField] private AttributeBaseSO itemNameAttribute;
    [SerializeField] private AttributeBaseSO itemDescAttribute;
    [SerializeField] private AttributeBaseSO itemStatsAttribute;
    [SerializeField] private AttributeBaseSO itemStaggerAttribute;
    [SerializeField] private AttributeBaseSO itemIconAttribute;
    [SerializeField] private GameObject itemInfoTextPrefab;
    [SerializeField] private Transform itemInfoTextParent;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemTitleText;


    public void Initialize()
    {
    }

    public void Destroy()
    {
    }

    public void ShowItemInfo(ItemWithAttributes item)
    {
        itemInfoTextParent.Clear();
        var itemName = item.GetAttribute<StringAttributeData>(itemNameAttribute)?.value;
        if (itemName != null)
        {
            itemTitleText.text = itemName;
            /*
            var prefab = Instantiate(itemInfoTextPrefab, itemInfoTextParent);
            var textObject = prefab.GetComponentInChildren<TextMeshProUGUI>();
            textObject.text = "Name: " + itemName;*/
        }

        var itemDesc = item.GetAttribute<StringAttributeData>(itemDescAttribute)?.value;
        if (itemDesc != null)
        {
            var prefab = Instantiate(itemInfoTextPrefab, itemInfoTextParent);
            var textObject = prefab.GetComponentInChildren<TextMeshProUGUI>();
            textObject.text = "Description: " + itemDesc;
        }

        var itemIcon = item.GetAttribute<SpriteAttributeData>(itemIconAttribute)?.value;
        if (itemIcon != null)
        {
            itemImage.enabled = true;
            itemImage.sprite = itemIcon;
        }

        var itemStats = item.GetAttribute<StatModifierListData>(itemStatsAttribute)?.value;
        if (itemStats != null)
        {
            foreach (var statModifier in itemStats)
            {
                var prefab = Instantiate(itemInfoTextPrefab, itemInfoTextParent);
                var textObject = prefab.GetComponentInChildren<TextMeshProUGUI>();
                // var textObject = Instantiate(textPrefab, itemInfoTextParent);
                textObject.text = statModifier.type + " : " + statModifier.value;
            }
        }

        var itemStaggerDamage = item.GetAttribute<FloatAttributeData>(itemStaggerAttribute)?.value;
        if (itemStaggerDamage != null)
        {
            var prefab = Instantiate(itemInfoTextPrefab, itemInfoTextParent);
            var textObject = prefab.GetComponentInChildren<TextMeshProUGUI>();
            textObject.text = "Weapon Stagger Damage" + " : " + itemStaggerDamage.Value;
        }
    }

    [SerializeField] private string defaultTitleName = "iteminfo";
    public void HideItemInfo()
    {
        itemInfoTextParent.Clear();
        itemImage.enabled = false;
        itemTitleText.text = defaultTitleName;
    }
}