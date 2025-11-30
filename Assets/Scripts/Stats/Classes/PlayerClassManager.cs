using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClassManager : MonoBehaviour
{
    [Header("player combat")]
    [SerializeField] private PlayerCombat playerCombat;

    [Header("dettagli estetici")]
    [SerializeField] private GameObject assassinMaskObject;
    [SerializeField] private GameObject angelWingsObject;
    [SerializeField] private GameObject warriorShieldObject;

    // metodo chiamato quando clicchi una classe
    public void SelectClass(CharacterClassData newClass)
    {
        // attiva/disattiva oggetti
        UpdateVisuals(newClass.visualType);

        // assegna la skill
        if (newClass.specificSkill != null)
        {
            // usiamo il metodo AssignSkill esistente
            playerCombat.AssignSkill(newClass.specificSkill.slot, newClass.specificSkill);

            Debug.Log($"Classe {newClass.className}: Skill {newClass.specificSkill.skillName} equipaggiata!");
        }

        // cambiare parametri base se vuoi
        // playerCombat.attackDamage = 30; // ex per il guerriero che è più forte
    }

    private void UpdateVisuals(ClassVisualType type)
    {
        // disattiva tutto
        if (assassinMaskObject) assassinMaskObject.SetActive(false);
        if (angelWingsObject) angelWingsObject.SetActive(false);
        if (warriorShieldObject) warriorShieldObject.SetActive(false);

        // attiva quello giusto
        switch (type)
        {
            case ClassVisualType.AssassinMask:
                if (assassinMaskObject) assassinMaskObject.SetActive(true);
                break;
            case ClassVisualType.AngelWings:
                if (angelWingsObject) angelWingsObject.SetActive(true);
                break;
            case ClassVisualType.WarriorShield:
                if (warriorShieldObject) warriorShieldObject.SetActive(true);
                break;
        }
    }
}
