using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;

//inventory holder
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private Vector3 dropOffset;
    [SerializeField] private ItemEventChannel itemDropEventChannel;
    [SerializeField] private ItemEventChannel itemPickupEventChannel;
    [SerializeField] private BoolEventChannel toggleInventoryFullEventChannel;

    [SerializeField] private Inventory mainInventory;
    [SerializeField] private Inventory hotbarInventory;

    private float currentInvFullTimer;

    [SerializeField] private float hideInvFullTime = 3f;
    
    /*public Inventory MainInventory => mainInventory;
    public Inventory HotbarInventory => hotbarInventory;*/

    private void Awake()
    {
        SaveSystem.OnInitSaveData += InitSaveData;
        SaveSystem.OnBeforeLoad += InitLoadInventory;
    }

    private void OnEnable()
    {
        itemDropEventChannel.Listeners += DropItemOnGround;
        itemPickupEventChannel.BoolListeners += PickUp;
    }

    private void OnDisable()
    {
        itemDropEventChannel.Listeners -= DropItemOnGround;
        itemPickupEventChannel.BoolListeners -= PickUp;
    }

    private void OnDestroy()
    {
        SaveSystem.OnInitSaveData -= InitSaveData;
        SaveSystem.OnBeforeLoad -= InitLoadInventory;
    }

    private void Start()
    {
        Debug.Log("inv holder start");
    }

//
    private void InitLoadInventory(SaveData data)
    {
        mainInventory.Init(data.inventorySaveData);
        hotbarInventory.Init(data.hotbarInventorySaveData);
    }

    private void InitSaveData()
    {
        Debug.Log("inv holder OnBeforeLoadTest");
        SaveData.Current.inventorySaveData = mainInventory.lazyInventoryContainer.ValueNoInit;
        SaveData.Current.hotbarInventorySaveData = hotbarInventory.lazyInventoryContainer.ValueNoInit;
    }

    public Action<ItemWithAttributes> OnPickup;

    public bool PickUp(ItemWithAttributes item)
    {
        if (!item.isStackable) return hotbarInventory.TryAddToEmptySlot(item) || mainInventory.TryAddToEmptySlot(item);
        if (hotbarInventory.TryAddToStack(item)) return true;
        if (mainInventory.TryAddToStack(item)) return true;

        return hotbarInventory.TryAddToEmptySlot(item) || mainInventory.TryAddToEmptySlot(item);
    }

    public void RegisterOnPickup(Action<ItemWithAttributes> pickupHandler)
    {
        mainInventory.OnPickup += pickupHandler;
        hotbarInventory.OnPickup += pickupHandler;
    }

    public void UnRegisterOnPickup(Action<ItemWithAttributes> pickupHandler)
    {
        mainInventory.OnPickup -= pickupHandler;
        hotbarInventory.OnPickup -= pickupHandler;
    }

//move this to item dropper component and add animation on drop
    //------
    public AttributeBaseSO dropPrefabAttr;


    private void DropItemOnGround(ItemWithAttributes item)
    {
        DropItemOnGroundAtTransform(item, dropPrefabAttr, transform);
    }

    public static void DropItemOnGroundAtTransform(ItemWithAttributes item, AttributeBaseSO dropPrefabAttr,
        Transform dropTransform, float dropYOffset = 0)
    {
        var attr = item.GetAttribute<GameObjectData>(dropPrefabAttr);

        if (attr != null && attr.value != null)
        {
            var droppedObject = Instantiate(attr.value, dropTransform.position + new Vector3(0,dropYOffset,0),
                attr.value.transform.rotation);
            var groundItem = droppedObject.GetComponent<GroundItem>();
            var objectId = droppedObject.GetComponent<GameObjectId>();
            objectId.GenerateNewId();

            // SaveData.Current.droppedGroundItems.Add(objectId.Id,
            //     groundItem.groundItemData);
        }
    }

//------------------
    public bool IsItemInInventory(ItemWithAttributes item)
    {
        var isItemInMainInv = mainInventory.ItemList.Any(slot => slot.GetItem() == item);
        var isItemInHotbarInv = hotbarInventory.ItemList.Any(slot => slot.GetItem() == item);
        return isItemInHotbarInv || isItemInMainInv;
    }

    public void RemoveItemFromInventory(ItemWithAttributes item)
    {
        if (RemoveItem(item, mainInventory)) return;
        if (RemoveItem(item, hotbarInventory)) return;

        bool RemoveItem(ItemWithAttributes item, Inventory inventory)
        {
            foreach (var slot in inventory.ItemList.Where(slot => slot.GetItem() == item))
            {
                slot.RemoveItem();
                return true;
            }

            return false;
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