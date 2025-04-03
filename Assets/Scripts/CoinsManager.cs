using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public TMP_Text coinText;
    private int coinCount = 0;

    public void AddCoin()
    {
        coinCount++;
        coinText.text = "Coins: " + coinCount;
    }
    /*void Start() //vogliamo assegnarlo manualmente coinText
    {
        coinText = GameObject.Find("CoinCounter").GetComponent<TMP_Text>();
    }*/

}
