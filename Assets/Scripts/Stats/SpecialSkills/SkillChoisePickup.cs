using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillChoisePickup : MonoBehaviour
{
    public SkillData[] possibleSkills;
    public SkillChoiseUI ui;
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
        PlayerCombat player = other.GetComponent<PlayerCombat>();
        if (!player) return;

        ui.Open(possibleSkills, chosenSkill =>
        {
            player.AssignSkill(chosenSkill.slot, chosenSkill);
        });
        gameObject.SetActive(false);
    }
}
