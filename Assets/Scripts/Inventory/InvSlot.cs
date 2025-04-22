using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InvSlot
{
    public Item item;
    public int quantity;
    
    public void AddItem(Item newItem, int amount)
    {
        if (item == null)
        {
            item = newItem;
            quantity = amount;
        }
        else if (item == newItem && item.isStackable)
        {
            quantity += amount;
        }
    }
    public void ClearSlot()
    {
        item = null;
        quantity = 0;
    }
}


