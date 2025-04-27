using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [Header("Dati Item")]
    public Item item;      // Quale oggetto �
    public int quantity = 1;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Applica una piccola "spinta" verso l'alto e in direzione casuale
            Vector3 randomForce = new Vector3(Random.Range(-1f, 1f), 1.5f, Random.Range(-1f, 1f));
            rb.AddForce(randomForce * 4f, ForceMode.Impulse);
        }
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
