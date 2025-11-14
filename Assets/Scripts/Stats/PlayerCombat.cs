using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SpecialSkill
{
    [Header("Skill settings")]
    public string skillName;
    public KeyCode activationKey;
    public float specialCooldown = 2f;

    public int damageBonus = 2;
    public float knockbackMultiplier = 2f;
    public GameObject specialHitEffect;

    [HideInInspector] public float nextUseTime = 0f;
    [HideInInspector] public bool unlocked = false;
}
public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public GameObject hitEffect;
    public GameObject damagePopup;
    public DamagePopupSpawn popupSpawner;

    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public LayerMask enemyLayers;
    public float attackRate = 0.75f; //tempo di attesa in secondi da dover aspettare per l'attacco successivo
    private float nextAttackTime = 0f;
    public float knockbackForce = 15f;

    [Header("Special Skills")]
    public SpecialSkill skillR = new SpecialSkill { skillName = "Skill R" , activationKey = KeyCode.R};
    public SpecialSkill skillT = new SpecialSkill { skillName = "Skill T", activationKey = KeyCode.T };
    public SpecialSkill skillE = new SpecialSkill { skillName = "Skill E", activationKey = KeyCode.E };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1")) // Click sinistro del mouse
            {
                Attack();
                nextAttackTime = Time.time + attackRate;
            }

            CheckAndUseSkill(skillR);
            CheckAndUseSkill(skillT);
            CheckAndUseSkill(skillE);
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        PerformAttack(attackDamage, attackRange, hitEffect, knockbackForce);

    }
    void PerformAttack(int damage, float range, GameObject effect, float knockback)
    {
        if (PauseMenu.instance.isPaused) // serve per non far prendere i click dell'attacco mentre è aperto il menu di pausa, va a richiamare l'instance di PauseMenu.cs
        {
            return;
        }
        animator.SetTrigger("Attack");

        // Collision detection
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            EnyDamageable damageable = enemy.GetComponent<EnyDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
                if (effect != null)
                {
                    Instantiate(effect, enemy.transform.position, Quaternion.identity);
                }
                if (popupSpawner != null)
                {
                    popupSpawner.SpawnPopup(attackDamage, enemy.transform.position);
                }

                KnockbackReceiver receiver = enemy.GetComponent<KnockbackReceiver>();
                if (receiver != null)
                {
                    Vector3 direction = (enemy.transform.position - transform.position);

                    direction.y = 0;

                    receiver.ApplyKnockback(direction, knockbackForce);
                }
            }
        }
    }

    void CheckAndUseSkill(SpecialSkill skill)
    {
        if (!skill.unlocked) return;
        if (Time.time < skill.nextUseTime) return;
        if (Input.GetKeyDown(skill.activationKey))
        {
            UseSpecialSkill(skill);
            skill.nextUseTime = Time.time + skill.specialCooldown;
        }
    }
    void UseSpecialSkill(SpecialSkill skill)
    {
        animator.SetTrigger("Attack"); //potrei cambiarlo in "SpecialAttack" e creare una animazione diversa nell'animator
        int totalDamage = attackDamage * skill.damageBonus;
        float totalKnockback = knockbackForce * skill.knockbackMultiplier;
        PerformAttack(totalDamage, attackRange, skill.specialHitEffect, totalKnockback);
    }
    public void UnlockSkillR() => skillR.unlocked = true;
    public void UnlockSkillT() => skillT.unlocked = true;
    public void UnlockSkillE() => skillE.unlocked = true;
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
