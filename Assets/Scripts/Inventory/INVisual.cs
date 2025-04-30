using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class INVisual : MonoBehaviour
{
    public GameObject dragIcon;
    private Image dragIconImage;
    private int draggedSlotIndex = -1;
    private int draggedFromSlot = -1;
    public Inventory inventory;
    [System.Serializable]
    public class SlotUI
    {
        public Image icon;
        public TMP_Text itemNameText; // Nome + quantità

        [HideInInspector] public RectTransform slotZoom; 
        [HideInInspector] public Vector3 originalScale;
    }

    public SlotUI[] slotsUI;
    public float zoomFactor = 1.5f;
    public float zoomSpeed = 9f;

    // Start is called before the first frame update
    void Start()
    {
            if (dragIcon != null)
            {
                dragIconImage = dragIcon.GetComponent<Image>();
                dragIcon.SetActive(false);
            }

        for (int i = 0; i < slotsUI.Length; i++)
        {
            if (slotsUI[i].icon != null)
            {
                slotsUI[i].slotZoom = slotsUI[i].icon.GetComponent<RectTransform>();
                slotsUI[i].originalScale = slotsUI[i].slotZoom.localScale;

                EventTrigger trigger = slotsUI[i].icon.GetComponent<EventTrigger>();
                if (trigger == null)
                    trigger = slotsUI[i].icon.gameObject.AddComponent<EventTrigger>();

                EventTrigger.Entry entryEnter = new EventTrigger.Entry();
                entryEnter.eventID = EventTriggerType.PointerEnter;
                int indexEnter = i; // non incasinare la lambda
                entryEnter.callback.AddListener((data) => { OnHoverEnter(indexEnter); });
                trigger.triggers.Add(entryEnter);

                EventTrigger.Entry entryExit = new EventTrigger.Entry();
                entryExit.eventID = EventTriggerType.PointerExit;
                int indexExit = i;
                entryExit.callback.AddListener((data) => { OnHoverExit(indexExit); });
                trigger.triggers.Add(entryExit);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < slotsUI.Length; i++)
        {
            var slot = inventory.slots[i];
            if (slot.item != null)
            {
                slotsUI[i].icon.sprite = slot.item.icon;
                slotsUI[i].icon.enabled = true;

                // gestione stringa nomeOggetto + quantità
                string displayText = slot.item.itemName;
                if (slot.item.isStackable && slot.quantity > 1)
                {
                    displayText += " x" + slot.quantity;
                }
                slotsUI[i].itemNameText.text = displayText;
                slotsUI[i].itemNameText.enabled = true;
            }
            else
            {
                slotsUI[i].icon.enabled = false;
                slotsUI[i].itemNameText.enabled = false;
            }
        }
    }
    void OnHoverEnter(int index)
    {
        StopAllCoroutines();
        StartCoroutine(Zoom(slotsUI[index].slotZoom, slotsUI[index].originalScale * zoomFactor));
    }

    void OnHoverExit(int index)
    {
        StopAllCoroutines();
        StartCoroutine(Zoom(slotsUI[index].slotZoom, slotsUI[index].originalScale));
    }

    IEnumerator Zoom(RectTransform target, Vector3 targetScale)
    {
        Vector3 initialScale = target.localScale;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * zoomSpeed;
            target.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }
    }
    public void StartDragging(int slotIndex, Sprite itemSprite)
    {
        draggedSlotIndex = slotIndex;
        dragIconImage.sprite = itemSprite;
        dragIcon.SetActive(true);
    }

    public void StopDragging()
    {
        dragIcon.SetActive(false);
        draggedSlotIndex = -1;
    }

    private int GetSlotUnderMouse()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, hits);

        foreach (var hit in hits)
        {
            var slotUI = hit.gameObject.GetComponent<InvSlotUI>();
            if (slotUI != null)
                return slotUI.slotIndex;
        }
        return -1;
    }

    public void UpdateDragPosition(Vector2 screenPosition)
    {
        if (dragIcon != null && dragIcon.activeSelf)
        {
            dragIcon.transform.position = screenPosition;
        }
    }

}
