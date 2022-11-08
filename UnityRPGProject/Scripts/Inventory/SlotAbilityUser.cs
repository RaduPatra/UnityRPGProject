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
        Debug.Log("hb awake");
    }

    private void OnEnable()
    {
        itemUseEventChannel.Listeners += UseItem;
        inputManager.hotbarUseAction += HotbarUseStarted;
    }

    private void HotbarUseStarted(int slotIndex)
    {
        Debug.Log("hb use " + slotIndex);
        var slot = hotbarInventory.ItemList[slotIndex];
        UseItem(slot);
    }

    private void OnDisable()
    {
        itemUseEventChannel.Listeners -= UseItem;
        inputManager.hotbarUseAction -= HotbarUseStarted;
        Debug.Log("hb destr");
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
}