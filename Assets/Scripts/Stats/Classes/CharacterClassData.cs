using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuovaClasse", menuName = "Combat/Classe Personaggio")]
public class CharacterClassData : ScriptableObject
{
    [Header("Info classe")]
    public string className;

    [Header("VFX")]
    public ClassVisualType visualType;

    [Header("Skill")]
    public SkillData specificSkill;
}
