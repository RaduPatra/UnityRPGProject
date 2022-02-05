using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Transform itemIcon;
    [SerializeField] private Transform tempDragParent;
    [SerializeField] private Image tempIcon;
    private Vector3 initPosition;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private ISlot thisSlot;
    private ISlot draggedSlot;


    private void Awake()
    {
        thisSlot = GetComponent<ISlot>();
        // thisSlot2 = GetComponent<InventorySlotUI>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsLeftButton(eventData)) return;
        initPosition = transform.position;
        lastPointerData = eventData;
        if (eventData.pointerDrag.GetComponent<ISlot>().InventorySlot.itemStack.item == null)
        {
            CancelDrag();
            return;
        }

        Debug.Log("OnBeginDrag " + gameObject);

        originalParent = transform.parent;
        canvasGroup.blocksRaycasts = false;
        //set parent to object at bottom of hierarchy so object being dragged renders on top
        tempIcon.sprite = itemIcon.gameObject.GetComponentInChildren<Image>().sprite;
        tempIcon.gameObject.SetActive(true);
        transform.SetParent(tempDragParent);
        
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!IsLeftButton(eventData)) return;
        itemIcon.transform.position = Input.mousePosition;
    }


    public void OnDrop(PointerEventData eventData)
    {
        if (!IsLeftButton(eventData)) return;
        draggedSlot = eventData.pointerDrag.GetComponent<ISlot>();
        // var draggedSlot2 = eventData.pointerDrag.GetComponent<InventorySlotUI>();

        if (!(draggedSlot.CanAcceptSlot(thisSlot.InventorySlot) &&
              thisSlot.CanAcceptSlot(draggedSlot.InventorySlot))) return;


        /*
        (draggedSlot.InventorySlot, thisSlot.InventorySlot) = (thisSlot.InventorySlot, draggedSlot.InventorySlot);
        
        (draggedSlot2.InventoryUI.inventory.ItemList[draggedSlot2.SlotIndex],
                thisSlot2.InventoryUI.inventory.ItemList[thisSlot2.SlotIndex]) =
            
            (thisSlot2.InventoryUI.inventory.ItemList[thisSlot2.SlotIndex],
                draggedSlot2.InventoryUI.inventory.ItemList[draggedSlot2.SlotIndex]);*/
        // (draggedSlot.inv)

        // thisSlot.SwapWith(draggedSlot);
        thisSlot.InventorySlot.SwapContentsWith(draggedSlot.InventorySlot);

        thisSlot.DropResponse();
        draggedSlot.DropResponse();

        Debug.Log("OnDrop1 " + transform.parent.gameObject);
        Debug.Log("OnDrop2 " + eventData.pointerDrag);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        Debug.Log("OnEndDrag " + gameObject);
        itemIcon.transform.position = initPosition;
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(originalParent);
        tempIcon.gameObject.SetActive(false);
    }

    private bool IsRightButton(PointerEventData eventData)
    {
        return eventData.button == PointerEventData.InputButton.Right;
    }

    private bool IsLeftButton(PointerEventData eventData)
    {
        return eventData.button == PointerEventData.InputButton.Left;
    }

    private PointerEventData lastPointerData;

    private void CancelDrag()
    {
        if (lastPointerData != null)
        {
            lastPointerData.pointerDrag = null;
        }
    }
}