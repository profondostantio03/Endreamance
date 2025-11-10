using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class PlayerStatsUI : MonoBehaviour
{
    [Header("References")]
    public PlayerCombat playerCombat; // assegna il tuo player
    public WeaponManager weaponManager; // opzionale, se vuoi mostrare anche il nome dell’arma

    [Header("UI Elements")]
    public TMP_Text damageText;
    public TMP_Text attackRateText;
    public TMP_Text attackRangeText;
    public TMP_Text weaponNameText; // facoltativo

    [Header("UI Update")]
    public float refreshRate = 0.25f; // aggiorna ogni 0.25s
    private float nextUpdate = 0f;

    private void Update()
    {
        if (Time.time >= nextUpdate)
        {
            UpdateUI();
            nextUpdate = Time.time + refreshRate;
        }
    }

    public void UpdateUI()
    {
        if (playerCombat == null) return;

        // Mostra valori attuali
        if (damageText != null)
            damageText.text = $"Danno: {playerCombat.attackDamage}";

        if (attackRateText != null)
            attackRateText.text = $"Velocita': {playerCombat.attackRate:0.00}s";

        if (attackRangeText != null)
            attackRangeText.text = $"Raggio: {playerCombat.attackRange:0.00}";

        // per mostrare l’arma attualmente equipaggiata
        if (weaponManager != null && weaponNameText != null)
        {
            string armaAttiva = TrovaArmaAttiva();

            if (!string.IsNullOrEmpty(armaAttiva))
                weaponNameText.text = $"Arma: {armaAttiva}";
            else
                weaponNameText.text = "Arma: Nessuna";
        }
    }

    private string TrovaArmaAttiva()
    {
        if(weaponManager.weaponHolder == null)
            return null;

        foreach (Transform arma in weaponManager.weaponHolder)
        {
            if (arma.gameObject.activeSelf) 
                return arma.name;
        }
        return null;
    }
}
