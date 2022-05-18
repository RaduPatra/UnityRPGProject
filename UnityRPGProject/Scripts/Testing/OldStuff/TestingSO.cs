using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;


[CreateAssetMenu(fileName = "Testing SO", menuName = "Testing SO", order = 1)]

public class TestingSO : SerializedScriptableObject
{
    public  SerializableDictionary<ItemCategory, InventorySlot> equipmentArmorSlots = new SerializableDictionary<ItemCategory, InventorySlot>();
    
    public  Dictionary<ItemCategory, InventorySlot> equipmentArmorSlots2 = new Dictionary<ItemCategory, InventorySlot>();
}