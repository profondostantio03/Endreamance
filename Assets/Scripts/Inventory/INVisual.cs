using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class INVisual : MonoBehaviour
{
    public Inventory inventory;
    [System.Serializable]
    public class SlotUI
    {
        public Image icon;
        public TMP_Text itemNameText; // Nome + quantità
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public SlotUI[] slotsUI;

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
}
