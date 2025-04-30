using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvSlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int slotIndex;
    public INVisual inventoryUI;

    private Image draggedIcon;

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin drag");
        var slot = inventoryUI.inventory.slots[slotIndex];
        if (slot.item != null)
        {
            inventoryUI.StartDragging(slotIndex, slot.item.icon);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("dragging");
        inventoryUI.UpdateDragPosition(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag");
        inventoryUI.StopDragging();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
