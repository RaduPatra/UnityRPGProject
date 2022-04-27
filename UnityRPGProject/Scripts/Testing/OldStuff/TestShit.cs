/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestShit : MonoBehaviour
{
    public Inventory hotbarInventory;
    public Inventory mainInventory;
    public EquipmentInventory equipmentInventory;
    public int index;
    public int index2;
    public ItemType type;

    [ContextMenu("test swap")]
    void TestSwap()
    {
        equipmentInventory.equipmentArmorSlots[type].NewSwapTest(hotbarInventory.ItemList[index]);
    }
    
    [ContextMenu("test swap same type")]
    void TestSwapSameType()
    {
        mainInventory.ItemList[index2].NewSwapTest(hotbarInventory.ItemList[index]);
    }
    
    [ContextMenu("test swap coll")]
    void TestSwapSameColl()
    {
        hotbarInventory.ItemList[index2].NewSwapTest(hotbarInventory.ItemList[index]);
    }
}*/