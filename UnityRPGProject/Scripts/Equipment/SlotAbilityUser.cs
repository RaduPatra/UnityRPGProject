using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class SlotAbilityUser : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    private EquipmentManager equipmentManager;
    private EffectManager effectManager;

    [SerializeField] private Inventory hotbarInventory;
    public ItemSlotEventChannel itemUseEventChannel;

    private void Awake()
    {
        // inputManager = GetComponent<InputManager>();
        equipmentManager = GetComponent<EquipmentManager>();
        effectManager = GetComponent<EffectManager>();
        itemUseEventChannel.Listeners += UseItem;
        inputManager.hotbarUseAction += HotbarUseStarted;
    }

    private void HotbarUseStarted(int slotIndex)
    {
        var slot = hotbarInventory.ItemList[slotIndex];
        UseItem(slot);
    }

    private void UseItem(InventorySlot slot)
    {
        equipmentManager.EquipItem(slot);
        effectManager.ConsumeItem(slot);
        // slot.OnUseSlot.Invoke(slot);
    }
}