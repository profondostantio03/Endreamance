using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager Instance;

    public TMP_Text coinText;
    private int coinCount = 0;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // serve per farlo rimanere tra le varie scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin()
    {
        AddCoins(1);
    }

    public void AddCoins(int amount)
    {
        coinCount += amount;
        UpdateCoinUI();
        Debug.Log($"Aggiunte {amount} monete. Totale: {coinCount}");
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
        else
        {
            Debug.LogWarning("coinText non assegnato nel CoinsManager");
        }
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
    /*void Start() //vogliamo assegnarlo manualmente coinText
    {
        coinText = GameObject.Find("CoinCounter").GetComponent<TMP_Text>();
    }*/

}
