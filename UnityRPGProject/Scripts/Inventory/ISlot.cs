using UnityEngine.EventSystems;

public interface ISlot : IPointerClickHandler, IPointerEnterHandler
{
    public InventorySlot InventorySlot { get; set; }
    public void UpdateUISlot(InventorySlot slot);
    // public void UpdateInventory(InventorySlot slot);

    // public void SwapWith(ISlot slot);

    // public bool CanAcceptSlot(InventorySlot slot);
}