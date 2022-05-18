using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;

//inventory holder
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private Inventory mainInventory;
    [SerializeField] private Inventory hotbarInventory;
    [SerializeField] private Vector3 dropOffset;
    [SerializeField] private ItemEventChannel itemDropEventChannel;
    [SerializeField] private ItemEventChannel itemPickupEventChannel;
    [SerializeField] private BoolEventChannel toggleInventoryFullEventChannel;

    private float currentInvFullTimer;
    [SerializeField] private float hideInvFullTime = 3f;
    /*public Inventory MainInventory => mainInventory;
    public Inventory HotbarInventory => hotbarInventory;*/

    private void Awake()
    {
        itemDropEventChannel.Listeners += DropItemOnGround;
        itemPickupEventChannel.BoolListeners += PickUp;
        SaveSystem.OnLoad += LoadInventory;
        mainInventory.Setup();
        hotbarInventory.Setup();
    }

    private void Start()
    {
        SaveData.Current.inventorySaveData = mainInventory.inventoryContainer;
        SaveData.Current.hotbarInventorySaveData = hotbarInventory.inventoryContainer;
    }

    public void LoadInventory(SaveData data)
    {
        if (data?.inventorySaveData == null) return;
        SetupInventory(mainInventory,data.inventorySaveData);
        SetupInventory(hotbarInventory,data.hotbarInventorySaveData);
        void SetupInventory(Inventory inventory, InventoryContainer inventoryContainer)
        {
            inventory.inventoryContainer = inventoryContainer;
            inventory.Setup();
            inventory.inventoryChanged?.Invoke();
        }
        
        // mainInventory.RefreshAssetTest();
        // hotbarInventory.RefreshAssetTest();
    }

    public bool PickUp(ItemWithAttributes item)
    {
        bool returnValue;
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

        if (attr != null && attr.value != null)
        {
            var droppedObject = Instantiate(attr.value, transform1.position + dropOffset,
                transform1.rotation);
            var groundItem = droppedObject.GetComponent<GroundItem>();
            var objectId = droppedObject.GetComponent<GameObjectId>();
            objectId.GenerateNewId();
            SaveData.Current.droppedGroundItems.Add(objectId.Id,
                groundItem.groundItemData);
        }
    }
    public void HandleInventoryFull()
    {
        toggleInventoryFullEventChannel.Raise(true);
        currentInvFullTimer = hideInvFullTime;
        invFullToggledOff = false;
    }

    private bool invFullToggledOff;
    private void Update()
    {
        currentInvFullTimer -= Time.deltaTime;
        if (currentInvFullTimer <= 0 && !invFullToggledOff)
        {
            toggleInventoryFullEventChannel.Raise(false);
            invFullToggledOff = true;
        }
    }
}