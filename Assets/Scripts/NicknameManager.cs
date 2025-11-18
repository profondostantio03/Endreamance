using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NicknameManager : MonoBehaviour
{
    public GameObject nicknamePanel;
    public TMP_InputField nicknameInputField;
    public TMP_Text nicknameDisplayText;

    private const string NicknameKey = "PlayerNickname";
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        GameInputBlocker.Blocked = true;
        if (PlayerPrefs.HasKey(NicknameKey))
        {
            string savedName = PlayerPrefs.GetString(NicknameKey);
            nicknameDisplayText.text = savedName;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfirmNickname()
    {
        string nickname = nicknameInputField.text;
        if (string.IsNullOrEmpty(nickname))
            return;

        PlayerPrefs.SetString(NicknameKey, nickname); // per salvare il nickname

        nicknameDisplayText.text = nickname;

        PlayerPrefs.Save(); // flush per sicurezza 

        nicknamePanel.SetActive(false);
        GameInputBlocker.Blocked = false;
        Time.timeScale = 1;
    }
}
