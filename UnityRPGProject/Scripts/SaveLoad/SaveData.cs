using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    private static SaveData current;

    public static SaveData Current
    {
        get { return current ??= new SaveData(); }
        set => current = value;
    }

    public InventoryContainer inventorySaveData = new InventoryContainer();
    public InventoryContainer hotbarInventorySaveData = new InventoryContainer();

    public SerializableDictionary<string, InventorySlot> equipmentArmorSave =
        new SerializableDictionary<string, InventorySlot>();

    public SerializableDictionary<string, InventorySlot> equippedWeaponSave =
        new SerializableDictionary<string, InventorySlot>();

    public List<string> collectedGroundItems = new List<string>();

    public SerializableDictionary<string, GroundItemData> droppedGroundItems =
        new SerializableDictionary<string, GroundItemData>();

    public List<string> lootedChests = new List<string>();
}