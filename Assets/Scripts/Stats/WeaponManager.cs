using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public PlayerCombat playerCombat;
    public Transform weaponHolder; // il transform "weapon_r"

    private WeaponStats currentWeaponStats;
    // Start is called before the first frame update
    void Start()
    {
        UpdateEquippedWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateEquippedWeapon()
    {
        // rimuove eventuali bonus della vecchia arma
        if (currentWeaponStats != null)
        {
            currentWeaponStats.Unequip(playerCombat);
            currentWeaponStats = null;
        }

        // trova l'arma attiva nel weaponHolder
        if (weaponHolder.childCount > 0)
        {
            Transform activeWeapon = weaponHolder.GetChild(0);
            currentWeaponStats = activeWeapon.GetComponent<WeaponStats>();

            if (currentWeaponStats != null)
            {
                currentWeaponStats.Equip(playerCombat);
                Debug.Log($"Arma equipaggiata: {activeWeapon.name}");
            }
            else
            {
                Debug.LogWarning($"L'arma {activeWeapon.name} non ha uno script WeaponStats!");
            }
        }
    }
}
