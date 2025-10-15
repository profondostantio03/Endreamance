using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public int damageBonus = 0;
    public float attackRateBonus = 0f;   // valori positivi = attacchi più veloci
    public float attackRangeBonus = 0f;

    private PlayerCombat playerCombat;
    private bool equipped = false;

    private void OnEnable()
    {
        if (playerCombat == null) 
        { 
            playerCombat = FindObjectOfType<PlayerCombat>();
        }
        Equip();
    }

    private void OnDisable()
    {
        Unequip();
    }
    public void Equip()
    {
        if (playerCombat == null || equipped) return;

        playerCombat.attackDamage += damageBonus;
        playerCombat.attackRate = Mathf.Max(0.05f, playerCombat.attackRate - attackRateBonus); // previene rate negativi
        playerCombat.attackRange += attackRangeBonus;

        equipped = true;
        FindObjectOfType<PlayerStatsUI>()?.UpdateUI();
    }

    public void Unequip()
    {
        if (playerCombat == null || !equipped) return;

        playerCombat.attackDamage -= damageBonus;
        playerCombat.attackRate += attackRateBonus;
        playerCombat.attackRange -= attackRangeBonus;

        equipped = false;
        FindObjectOfType<PlayerStatsUI>()?.UpdateUI();
    }
}
