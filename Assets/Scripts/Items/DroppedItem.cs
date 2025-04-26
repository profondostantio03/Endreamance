using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [Header("Dati Item")]
    public Item item;      // Quale oggetto è
    public int quantity = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Oggetto entrato: " + collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {
            Inventory inventory = collision.GetComponent<Inventory>();
            if (inventory != null)
            {
                Debug.Log($"Tentativo di aggiungere {item.itemName} x{quantity} all'inventario.");
                inventory.Add(item, quantity);
                Debug.Log($"Raccolto {item.itemName} x{quantity}");
                Destroy(gameObject);
            }
        }
    }
}
