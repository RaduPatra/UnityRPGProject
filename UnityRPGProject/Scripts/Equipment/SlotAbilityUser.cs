using System;
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
        effectManager.ConsumeItem(slot);
        equipmentManager.ToggleWeaponAction(slot);
        equipmentManager.EquipArmorAction(slot);
    }

    public void UnequipTest(InventorySlot slot)
    {
        inventoryHolder.PickUp(slot.itemStack.item);
        slot.ResetSlot();
    }
}