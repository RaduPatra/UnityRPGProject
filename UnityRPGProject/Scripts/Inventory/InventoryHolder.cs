using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inventory holder
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private Inventory mainInventory;
    [SerializeField] private Inventory hotbarInventory;
    [SerializeField] private Vector3 dropOffset;
    [SerializeField] private ItemEventChannel itemDropEventChannel;
    [SerializeField] private ItemEventChannel itemPickupEventChannel;

    /*public Inventory MainInventory => mainInventory;
    public Inventory HotbarInventory => hotbarInventory;*/

    private void Awake()
    {
        itemDropEventChannel.Listeners += DropItemOnGround;
        itemPickupEventChannel.BoolListeners += PickUp;
    }

    public bool PickUp(ItemWithAttributes item)
    {
        if (!item.isStackable) return hotbarInventory.TryAddToEmptySlot(item) || mainInventory.TryAddToEmptySlot(item);
        if (hotbarInventory.TryAddToStack(item)) return true;
        if (mainInventory.TryAddToStack(item)) return true;

        return hotbarInventory.TryAddToEmptySlot(item) || mainInventory.TryAddToEmptySlot(item);
    }

    public AttributeBaseSO dropPrefabAttr;

    private void DropItemOnGround(ItemWithAttributes item)
    {
        var transform1 = transform;
        var attr = item.GetAttribute<GameObjectData>(dropPrefabAttr);

        if (attr != null)
        {
            Instantiate(attr.value, transform1.position + dropOffset,
                transform1.rotation); //get drop prefab attribute here
        }
    }
}