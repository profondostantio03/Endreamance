using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassSelectorUI : MonoBehaviour
{
    [Header("Riferimenti generali")]
    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private PlayerClassManager playerManager;
    [Header("Classi Disponibili")]
    [SerializeField] private CharacterClassData[] classOptions;

    private bool isMenuOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        CloseMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenMenu()
    {
        selectionPanel.SetActive(true);
        isMenuOpen = true;
        Time.timeScale = 0f;
        /*Cursor.lockState = CursorLockMode.None; //sblocca il cursore
        Cursor.visible = true;*/
    }

    public void CloseMenu()
    {
        selectionPanel.SetActive(false);
        isMenuOpen = false;
        Time.timeScale = 1f;
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/
    }

    public void SelectClassByIndex(int index)
    {
        if(index<0  || index >= classOptions.Length)
        {
            Debug.LogError("la classe chiesa non esiste nell'array");
            return;
        }
        CharacterClassData selectedData = classOptions[index];
        playerManager.SelectClass(selectedData);
        CloseMenu();
    }
}
