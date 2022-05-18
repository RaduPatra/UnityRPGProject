
    using System;
    using TMPro;
    using UnityEngine;

    public class RewardUI : MonoBehaviour
    {
        
        [SerializeField] private ItemEventChannel showChestLootEventChannel;
        
        [SerializeField] private ItemEventChannel hideChestLootEventChannel;

        [SerializeField] private GameObject chestPickupUI;
        
        [SerializeField] private AttributeBaseSO itemNameAttribute;


        
        
        private void Awake()
        {
            showChestLootEventChannel.Listeners += ShowChestLoot;
            hideChestLootEventChannel.Listeners += HideChestLoot;
        }

        private void ShowChestLoot(ItemWithAttributes item)
        {
            chestPickupUI.SetActive(true);
            
            var itemName = item.GetAttribute<StringAttributeData>(itemNameAttribute)?.value;
            if (itemName != null)
            {
                chestPickupUI.GetComponentInChildren<TextMeshProUGUI>().text = "Loot item " + itemName;
            }
        }
        private void HideChestLoot(ItemWithAttributes item)
        {
            chestPickupUI.SetActive(false);
        }
        
        
        

        

    }
