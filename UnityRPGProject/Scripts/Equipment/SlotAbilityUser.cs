using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class SlotAbilityUser : MonoBehaviour
{
    private InputManager inputManager;
    private EquipmentManager equipmentManager;
    private EffectManager effectManager;

    [SerializeField]
    private Inventory hotbarInventory;
    public ItemSlotEventChannel itemUseEventChannel;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        equipmentManager = GetComponent<EquipmentManager>();
        effectManager = GetComponent<EffectManager>();
        itemUseEventChannel.Listeners += UseItem;
    }

    private void Update()
    {
        GetHotbarInput();
    }

    private void GetHotbarInput()
    {
        if (inputManager.hotbarInput < 0 || inputManager.hotbarInput >= hotbarInventory.ItemList.Count) return;
        var slot = hotbarInventory.ItemList[inputManager.hotbarInput];
        UseItem(slot);
    }

    private void UseItem(InventorySlot slot)
    {
        equipmentManager.EquipItem(slot);
        effectManager.ConsumeItem(slot);
        // slot.OnUseSlot.Invoke(slot);
    }
}