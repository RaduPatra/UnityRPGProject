using System;
using UnityEngine;

public class GroundItemLoader : MonoBehaviour
{
    // public GameObject groundItemPrefab;
    [SerializeField] private AttributeBaseSO equipmentPickupAttribute;

    private void Awake()
    {
        SaveSystem.OnLoad += LoadItems;
    }

    private void OnDestroy()
    {
        SaveSystem.OnLoad -= LoadItems;

    }

    private void LoadItems(SaveData saveData)
    {
        var droppedItems = saveData.sceneData.droppedGroundItems;
        var db = SaveSystemManager.Instance.itemDb;
        foreach (var item in droppedItems)
        {
            var objectId = item.Key;
            var groundData = item.Value;

            var itemData = db.GetById(groundData.itemId);
            var equipmentPickupPrefab = itemData.GetAttribute<GameObjectData>(equipmentPickupAttribute)?.value;
            if (equipmentPickupPrefab == null)
            {
                Debug.LogError("Cant load item - Equipment Prefab not assigned on" + itemData);
                return;
            }
 
            var groundGO = Instantiate(equipmentPickupPrefab, groundData.position, groundData.rotation);
            var groundItemObjectId = groundGO.GetComponent<GameObjectId>();
            var groundItem = groundGO.GetComponent<GroundItem>();


            groundItemObjectId.Id = objectId;
            groundItem.Item = itemData;
        }
    }
}