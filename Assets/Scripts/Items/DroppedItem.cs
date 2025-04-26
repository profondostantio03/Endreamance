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

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Oggetto entrato: " + collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collisione avvenuta");
            Inventory playerInventory = FindObjectOfType<Inventory>();
            if (playerInventory != null)
            {
                Debug.Log($"Tentativo di aggiungere {item.itemName} x{quantity} all'inventario.");
                playerInventory.Add(item, quantity);
                Debug.Log($"Raccolto {item.itemName} x{quantity}");
                Destroy(gameObject);
            }
        }
    }
}
