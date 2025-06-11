using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("XP / Level")]
    public int level = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;
    public int xpIncrement = 50; // quanto aumenta la soglia ogni livello

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void GainXP(int amount)
    {
        currentXP += amount;
        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        xpToNextLevel += xpIncrement;

        maxHealth += 20;
        currentHealth = maxHealth;

        int rewardCoins = CalculateLevelUpReward(level);
        CoinsManager.Instance.AddCoins(rewardCoins);
    }

    int CalculateLevelUpReward(int level) // ricompensa per ogni level up
    {
        return 0 + (level - 1) * 5; // ex. 0 base + 5 per livello
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " è morto!");
        // Qui puoi aggiungere animazione morte, respawn, ecc.
    }
}
