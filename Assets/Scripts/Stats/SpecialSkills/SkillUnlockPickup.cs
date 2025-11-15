using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRUnlock : MonoBehaviour
{
    public SkillData skillToUnlock;

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
        if (other.CompareTag("Player"))
        {
            PlayerCombat pc = other.GetComponent<PlayerCombat>();
            if (pc != null)
            {
                pc.EquipSkill(skillToUnlock);
            }

            Destroy(gameObject);
        }
    }
}
