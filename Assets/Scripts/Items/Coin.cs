using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int xpValue = 10;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Moneta raccolta!"); // ?? Debug per verificare
            CoinsManager coinManager = FindObjectOfType<CoinsManager>();

            if (coinManager != null)
            {
                coinManager.AddCoin();
            }
            else
            {
                Debug.LogError("CoinsManager non trovato nella scena!");
            }
            CharacterStats stats = other.GetComponent<CharacterStats>();
            if (stats != null)
            {
                stats.GainXP(xpValue);
            }
            else
            {
                Debug.LogError("CharacterStats non trovato sul Player!");
            }
            Destroy(gameObject); // spostato qui da sotto il coinManager.AddCoin() perchè ho aggiunto la componente stats per dare xp una volta raccolta la moneta
        }
    }
}
