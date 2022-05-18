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

    private void Awake()
    {
        itemUseEventChannel.Listeners += UseItem;
        inputManager.hotbarUseAction += HotbarUseStarted;
    }

    private void Start()
    {
        // itemUnequipEventChannel.Listeners += UnequipTest;
    }

    private void HotbarUseStarted(int slotIndex)
    {
        var slot = hotbarInventory.ItemList[slotIndex];
        UseItem(slot);
    }

    private void UseItem(InventorySlot slot)
    {
        var item = slot.GetItem();
        if (item == null) return;
        foreach (var category in item.GetCategoryAncestors())
        {
            var useAction = category.categoryUseAction;//this is hardcoded atm - need to add category actions
            if (useAction == null) continue;
            useAction.Execute(gameObject, slot);
        }
    }

    /*public void UnequipTest(InventorySlot slot)
    {
        inventoryHolder.PickUp(slot.itemStack.item);
        slot.RemoveItem();
    }*/
}