using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Heal, MaxHealth, MaxHealthTemporary, XPBoost }
    public PowerUpType type;
    public int amount = 20;
    public float duration = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            CharacterStats stats = other.GetComponent<CharacterStats>();
            if (stats != null)
            {
                switch (type)
                {
                    case PowerUpType.Heal:
                        stats.Heal(amount);
                        break;

                    case PowerUpType.MaxHealth:
                        stats.maxHealth += amount;
                        stats.currentHealth += amount;
                        break;

                    case PowerUpType.MaxHealthTemporary:
                        stats.StartCoroutine(ApplyMaxHealthTemporary(stats));
                        break;

                    case PowerUpType.XPBoost:
                        stats.GainXP(amount);
                        break;
                }
                Destroy(gameObject);
            }
        }
    }
    // PER RENDERE IL POWER UP TEMPORANEO
    private IEnumerator ApplyMaxHealthTemporary(CharacterStats stats)
    {
        int originalMaxHealth = stats.maxHealth;
        stats.maxHealth += amount;
        stats.currentHealth += amount;

        yield return new WaitForSeconds(duration);

        stats.maxHealth = originalMaxHealth;
        stats.currentHealth = Mathf.Min(stats.currentHealth, stats.maxHealth);
    }

}
