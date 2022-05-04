using System;
using TMPro;
using UnityEngine;


public interface IInitializable
{
    public void Initialize();
}

public class ItemPickupPreviewUI : MonoBehaviour, IInitializable
{
    [SerializeField] private ItemEventChannel itemPickupPreviewEventChannel;
    [SerializeField] private ItemEventChannel itemPickupExitPreviewEventChannel;
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private AttributeBaseSO itemNameAttribute;

    public void Initialize()
    {
        itemPickupPreviewEventChannel.Listeners += ShowPreview;
        itemPickupExitPreviewEventChannel.Listeners += HidePreview;
    }

    private void ShowPreview(ItemWithAttributes item)
    {
        gameObject.SetActive(true);
        var itemName = item.GetAttribute<StringAttributeData>(itemNameAttribute)?.value;
        if (itemName != null)
        {
            statText.text = "Press E to Pickup - " + itemName;
        }
    }

    private void HidePreview(ItemWithAttributes item)
    {
        statText.text = "";
        gameObject.SetActive(false);
    }
}