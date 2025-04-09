using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{
    public CharacterStats stats;
    public Image Level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fill = (float)stats.currentXP / stats.xpToNextLevel;
        Level.fillAmount = fill;
    }
}
