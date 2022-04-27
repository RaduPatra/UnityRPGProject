using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class SlotAbilityUser : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Inventory hotbarInventory;
    public ItemSlotEventChannel itemUseEventChannel;
    
    private EquipmentManager equipmentManager;
    private InventoryHolder inventoryHolder;
    private EffectManager effectManager;

    private void Awake()
    {
        equipmentManager = GetComponent<EquipmentManager>();
        inventoryHolder = GetComponent<InventoryHolder>();
        effectManager = GetComponent<EffectManager>();
        // itemUseEventChannel.Listeners += UseItem;
        itemUseEventChannel.Listeners += UseItemTest;
        inputManager.hotbarUseAction += HotbarUseStarted;
    }

    private void Start()
    {
        // itemUnequipEventChannel.Listeners += UnequipTest;
    }

    private void HotbarUseStarted(int slotIndex)
    {
        var slot = hotbarInventory.ItemList[slotIndex];
        // UseItem(slot);
        UseItemTest(slot);
    }
    /*private void UseItem(InventorySlot slot)//right click action (main)
    {
        effectManager.ConsumeItem(slot);
        equipmentManager.ToggleUsableItemAction(slot);
        equipmentManager.EquipArmorAction(slot);
    }*/

    private void UseItemTest(InventorySlot slot)
    {
        var item = slot.GetItem();
        if (item == null) return;
        foreach (var category in item.GetCategoryAncestors())
        {
            var useAction = category.categoryUseAction;
            if (useAction == null) continue;
            useAction.Execute(gameObject, slot);
        }
    }

    public void UnequipTest(InventorySlot slot)
    {
        inventoryHolder.PickUp(slot.itemStack.item);
        slot.RemoveItem();
    }
}