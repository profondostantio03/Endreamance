using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public CharacterStats characterStats;
    public Image HealthBarFill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fillAmount = (float)characterStats.currentHealth / characterStats.maxHealth;
        HealthBarFill.fillAmount = fillAmount;
    }
}
