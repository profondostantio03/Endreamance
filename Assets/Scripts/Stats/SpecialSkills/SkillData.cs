using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillSlot { R, E, T}

[CreateAssetMenu(fileName = "NewSpecialSkill", menuName = "Combat/SpecialSkill")]
public class SkillData : ScriptableObject
{
    public string skillName;

    public SkillSlot slot; 
    public KeyCode activationKey; // da andare a modificare quando avro' stabilito quali saranno i possibili tasti di attivazione di una skill

    public GameObject hitEffect;

    [Header("Stats")]
    public float damageMultiplier = 2;
    public float cooldown = 2f;
    public float knockbackMultiplier = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
