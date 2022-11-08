
    using System;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class RewardChestUI : MonoBehaviour
    {
        [SerializeField] private ItemEventChannel showChestLootEventChannel;
        
        [SerializeField] private ItemEventChannel hideChestLootEventChannel;

        [SerializeField] private GameObject chestPickupUI;
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemDescText;
        [SerializeField] private Image itemImage;
        
        [SerializeField] private AttributeBaseSO itemNameAttribute;
        [SerializeField] private AttributeBaseSO itemDescAttribute;
        [SerializeField] private AttributeBaseSO itemIconAttribute;


        
        
        private void Awake()
        {
            showChestLootEventChannel.Listeners += ShowChestLoot;
            hideChestLootEventChannel.Listeners += HideChestLoot;
        }

        private void OnDestroy()
        {
            showChestLootEventChannel.Listeners -= ShowChestLoot;
            hideChestLootEventChannel.Listeners -= HideChestLoot;
        }

        private void ShowChestLoot(ItemWithAttributes item)
        {
            chestPickupUI.SetActive(true);
            
            var itemName = item.GetAttribute<StringAttributeData>(itemNameAttribute)?.value;
            if (itemName != null)
            {
                itemNameText.text = itemName;
                // chestPickupUI.GetComponentInChildren<TextMeshProUGUI>().text = itemName;
            }
            
            var itemDesc = item.GetAttribute<StringAttributeData>(itemDescAttribute)?.value;
            if (itemDesc != null)
            {
                itemDescText.text = itemDesc;
            }

            var itemIcon = item.GetAttribute<SpriteAttributeData>(itemIconAttribute)?.value;
            if (itemDesc != null)
            {
                itemImage.sprite = itemIcon;
            }
            
        }
        private void HideChestLoot(ItemWithAttributes item)
        {
            chestPickupUI.SetActive(false);
        }
    }
