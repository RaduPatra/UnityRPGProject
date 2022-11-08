using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventories/EquipmentLazyTest", order = 1)]
public class EquipmentInventory : SerializedScriptableObject
{
    [HideReferenceObjectPicker]
    public LazyValueTest<Dictionary<ItemCategory, InventorySlot>> equipmentSlots =
        new LazyValueTest<Dictionary<ItemCategory, InventorySlot>>();

    [ContextMenu("Load item ids")]
    public void LoadItemIds()
    {
        foreach (var invItem in equipmentSlots.ValueNoInit)
        {
            var slot = invItem.Value;
            if (slot.itemStack != null)
            {
                if (slot.itemStack.item != null)
                {
                    slot.itemStack.itemId = slot.itemStack.item.Id;
                }
            }
        }
    }

    [ContextMenu("refresh asset test")]
    public void RefreshAssetTest()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
#endif
        // AssetDatabase.Refresh();
    }
}

