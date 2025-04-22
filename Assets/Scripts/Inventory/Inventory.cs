using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InvSlot[] slots = new InvSlot[15];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Add(Item item, int amount)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item && item.isStackable)
            {
                slots[i].AddItem(item, amount);
                return;
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(item, amount);
                return;
            }
        }
        Debug.Log("Inventario pieno!");
    }
}
