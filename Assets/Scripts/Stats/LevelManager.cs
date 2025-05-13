using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public CharacterStats stats;
    public Image Level;
    public Text MyLevel;
    public Text XPAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stats == null || Level == null)
            return;

        float fillAmount = (float)stats.currentXP / stats.xpToNextLevel;
        Level.fillAmount = fillAmount;

        MyLevel.text = "Livello: " + stats.level;
        XPAmount.text = "XP: " + stats.currentXP + "/" + stats.xpToNextLevel;
    }
}
