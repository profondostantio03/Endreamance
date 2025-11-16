using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillChoiseUI : MonoBehaviour
{
    [Serializable]
    public class SkillOptionUI
    {
        public Image icon;
        public TMP_Text descriptionText;
        public Button chooseButton;
        public TMP_Text buttonText;
    }

    public SkillOptionUI[] options;
    private SkillData[] currentSkills;   // Skills proposte
    private Action<SkillData> onSkillChosen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(SkillData[] skills, Action<SkillData> callback)
    {
        gameObject.SetActive(true);

        Time.timeScale = 0.15f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        currentSkills = skills;
        onSkillChosen = callback;

        for (int i = 0; i < options.Length; i++)
        {
            SkillData skill = skills[i];
            SkillOptionUI ui = options[i];
            ui.buttonText.text = skill.name;
            ui.descriptionText.text =
                $"Dmg x{skill.damageMultiplier}\n" +
                $"CD: {skill.cooldown}s\n" +
                $"Knock: x{skill.knockbackMultiplier}";
            ui.icon.sprite = skill.icon;
            ui.chooseButton.onClick.RemoveAllListeners();
            ui.chooseButton.onClick.AddListener(() => SelectSkill(skill));
        }
    }

    private void SelectSkill(SkillData skill)
    {
        onSkillChosen?.Invoke(skill);
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
