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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectWeapon(int index)
    {
        for (int i = 0; i < weaponHolder.childCount; i++)
            weaponHolder.GetChild(i).gameObject.SetActive(i == index);
    }
}
