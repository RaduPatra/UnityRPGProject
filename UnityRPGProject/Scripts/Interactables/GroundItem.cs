using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GroundItem : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemWithAttributes item;
    [SerializeField] private StringEventChannel interactPreviewEventChannel;
    [SerializeField] private VoidEventChannel interactExitPreviewEventChannel;
    [SerializeField] private AttributeBaseSO itemNameAttribute;

    public ItemWithAttributes Item { get; set; }

    public GroundItemData groundItemData { get; set; }


    private void Awake()
    {
        groundItemData = new GroundItemData
        {
            position = transform.position,
            rotation = transform.rotation,
            itemId = item.Id
        };
        // Debug.Log("awake ground item");
        SaveSystem.OnLoad += LoadGroundItem;
        // SaveSystem.OnInitSaveData += InitSave;
        SaveData.Current.sceneData.droppedGroundItems[GetComponent<GameObjectId>().Id] = groundItemData;

    }

    /*private void InitSave()
    {
        SaveData.Current.droppedGroundItems[GetComponent<GameObjectId>().Id] = groundItemData;
    }*/

    private void OnDestroy()
    {
        SaveSystem.OnLoad -= LoadGroundItem;
        // SaveSystem.OnInitSaveData -= InitSave;

    }

    private void Start() //data gets updated when it instantiates
    {
        // InitSave();
    }

    private void LoadGroundItem(SaveData saveData)
    {
        Debug.Log("load ground item");
        var objectId = GetComponent<GameObjectId>().Id;
        Destroy(gameObject);
        /*if (saveData.collectedGroundItems.Contains(objectId) || saveData.droppedGroundItems.ContainsKey(objectId))
        {
            Destroy(gameObject);
        }*/
    }


    public void Interact(Interactor user)
    {
        var inventoryHolder = user.GetComponent<InventoryHolder>();
        if (inventoryHolder == null) return;
        var pickupSuccess = inventoryHolder.PickUp(item);
        if (pickupSuccess)
        {
            var objectId = GetComponent<GameObjectId>().Id;
            // SaveData.Current.collectedGroundItems.Add(objectId);
            var droppedItems = SaveData.Current.sceneData.droppedGroundItems;
            if (droppedItems.ContainsKey(objectId))
            {
                droppedItems.Remove(objectId);
            }

            Destroy(gameObject);
            Debug.Log(item.name);
            InteractExit(user);
            return;
        }

        inventoryHolder.HandleInventoryFull();
    }

    public void InteractPreview(Interactor user)
    {
        var previewText = "";
        var itemName = item.GetAttribute<StringAttributeData>(itemNameAttribute)?.value;
        if (itemName != null)
            previewText = "Press E to Pickup - " + itemName;

        interactPreviewEventChannel.Raise(previewText);
    }

    public void InteractExit(Interactor user)
    {
        interactExitPreviewEventChannel.Raise();
    }
}

[Serializable]
public class GroundItemData
{
    public Vector3 position;
    public Quaternion rotation;
    public string itemId;
}