using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Moneta raccolta!"); // ?? Debug per verificare
            CoinsManager coinManager = FindObjectOfType<CoinsManager>();

            if (coinManager != null)
            {
                coinManager.AddCoin();
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("CoinsManager non trovato nella scena!");
            }
        }
    }
}
