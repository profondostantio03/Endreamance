using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public Item keyItem;
    public bool keyItemDroppable = false;
    private Inventory playerInventory; 
    private int currentHealth;
    private bool isDying = false;
    private Renderer rend;
    public GameObject dropPrefab; // prefab "DroppedItem"
    public Item itemToDrop;      // item da droppare
    public int dropAmount = 1;
    public bool itemToDropDroppable = true;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rend = GetComponentInChildren<Renderer>();
        playerInventory = FindObjectOfType<Inventory>(); // Trova l'inventario nella scena
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        if (isDying) return;
        currentHealth -= damage;
        Debug.Log("Enemy took damage: " + damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy died!");
        // Si possono mettere le varie animazioni suoni etc
        StartCoroutine(FadeAndDie());
        /*if (keyItem != null && playerInventory != null)
        {
            playerInventory.Add(keyItem, 1);
            Debug.Log("Aggiunta chiave all'inventario!");
        }

        if (dropPrefab != null)
        {
            GameObject droppedObj = Instantiate(dropPrefab, transform.position, Quaternion.identity);

            DroppedItem dropScript = droppedObj.GetComponent<DroppedItem>();
            if (dropScript != null)
            {
                dropScript.item = itemToDrop;
                dropScript.quantity = dropAmount;
            }
        }*/
        if (keyItem != null)
        {
            if (keyItemDroppable && dropPrefab != null)
            {
                GameObject droppedObj = Instantiate(dropPrefab, transform.position, Quaternion.identity);
                DroppedItem dropScript = droppedObj.GetComponent<DroppedItem>();
                if (dropScript != null)
                {
                    dropScript.item = keyItem;
                    dropScript.quantity = 1;
                }
            }
            else if (playerInventory != null)
            {
                playerInventory.Add(keyItem, 1);
                Debug.Log("Aggiunta direttamente la Key all'inventario!");
            }
        }

        // GESTIONE eventuale altro oggetto (tipo oro, materiali, ecc.)
        if (itemToDrop != null)
        {
            if (itemToDropDroppable && dropPrefab != null)
            {
                GameObject droppedObj = Instantiate(dropPrefab, transform.position, Quaternion.identity);
                DroppedItem dropScript = droppedObj.GetComponent<DroppedItem>();
                if (dropScript != null)
                {
                    dropScript.item = itemToDrop;
                    dropScript.quantity = dropAmount;
                }
            }
            else if (playerInventory != null)
            {
                playerInventory.Add(itemToDrop, dropAmount);
                Debug.Log("Aggiunto direttamente item secondario all'inventario!");
            }
        }
        //Destroy(gameObject);
    }
    IEnumerator FadeAndDie()
    {
        isDying = true;

        Material mat = rend.material;
        Color originalColor = mat.color;

        float duration = 1.5f;
        float elapsed = 0f;

        // Assicura che il materiale supporti la trasparenza
        mat.SetFloat("_Mode", 2); // Rendering Mode = Fade
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            mat.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject); // Lo elimina dopo che si è abbassato il canale alfa
    }
    public bool IsDying()
    {
        return isDying;
    }
}
