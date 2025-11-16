using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Equipped Special Skills")]
    public SkillData skillR;
    public SkillData skillT;
    public SkillData skillE;

    private Dictionary<SkillData, float> cooldownTracker = new Dictionary<SkillData, float>();
    public Dictionary<SkillSlot, SkillData> equippedSkills = new Dictionary<SkillSlot, SkillData>(); // PER SALVARE LA SKILL SCELTA

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

            CheckSkill(skillR);
            CheckSkill(skillT);
            CheckSkill(skillE);
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
                damageable.TakeDamage(damage);
                if (effect != null)
                {
                    Instantiate(effect, enemy.transform.position, Quaternion.identity);
                }
                if (popupSpawner != null)
                {
                    popupSpawner.SpawnPopup(damage, enemy.transform.position);
                }

                KnockbackReceiver receiver = enemy.GetComponent<KnockbackReceiver>();
                if (receiver != null)
                {
                    Vector3 direction = (enemy.transform.position - transform.position);

                    direction.y = 0;

                    receiver.ApplyKnockback(direction, knockback);
                }
            }
        }
    }

    void CheckSkill(SkillData skill)
    {
        if (skill == null) return;
        if (!cooldownTracker.ContainsKey(skill))
            cooldownTracker[skill] = 0;

        if (Time.time < cooldownTracker[skill]) return;

        if (Input.GetKeyDown(skill.activationKey))
        {
            UseSkill(skill);
            cooldownTracker[skill] = Time.time + skill.cooldown;
        }
    }
    void UseSkill(SkillData skill)
    {
        animator.SetTrigger("Attack"); //potrei cambiarlo in "SpecialAttack" e creare una animazione diversa nell'animator
        int totalDamage = Mathf.RoundToInt(attackDamage * skill.damageMultiplier);
        float totalKnockback = knockbackForce * skill.knockbackMultiplier;
        PerformAttack(totalDamage, attackRange, skill.hitEffect, totalKnockback);
    }

    public void EquipSkill(SkillData newSkill)
    {
        switch (newSkill.slot)
        {
            case SkillSlot.R: skillR = newSkill; break;
            case SkillSlot.E: skillE = newSkill; break;
            case SkillSlot.T: skillT = newSkill; break;
        }
    }

    public void AssignSkill(SkillSlot slot, SkillData data) // per equipaggiare la skill scelta 
    {
        equippedSkills[slot] = data;
        EquipSkill(data);
        Debug.Log($"Skill {data.skillName} assegnata allo slot {slot}");
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
