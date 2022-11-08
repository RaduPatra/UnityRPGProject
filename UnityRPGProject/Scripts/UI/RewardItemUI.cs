using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardItemUI : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public Image itemImage;
    
    [SerializeField] private AttributeBaseSO itemNameAttribute;
    [SerializeField] private AttributeBaseSO spriteAttribute;

    public void SetItem(ItemWithAttributes item)
    {
        var itemName = item.GetAttribute<StringAttributeData>(itemNameAttribute)?.value;
        var itemIcon = item.GetAttribute<SpriteAttributeData>(spriteAttribute)?.value;
        
        itemNameText.text = itemName;
        itemImage.sprite = itemIcon;
    }
}