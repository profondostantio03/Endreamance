using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCSystem : MonoBehaviour
{
    bool player_detect = false;
    bool dialogueActive = false;
    int currentLineIndex = 0;

    public GameObject canva;
    public GameObject conversationTemplate;
    public List<string> dialogueLines = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        dialogueLines.Add("Ciao viaggiatore.");
        dialogueLines.Add("Presta attenzione in questo paese, non e' tutto oro quel che luccica.");
        dialogueLines.Add("Non perdere di vista la tua luce.");

        canva.SetActive(false); // Nascondi canvas all'inizio
    }

    // Update is called once per frame
    void Update()
    {
        if(player_detect && Input.GetKeyDown(KeyCode.F))
        {
            if (!dialogueActive)
            {
                StartDialogue();
            }
            else
            {
                NextLine();
            }
        }
    }

    void StartDialogue()
    {
        dialogueActive = true;
        currentLineIndex = 0;
        canva.SetActive(true);
        ShowCurrentLine();
    }

    void NextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Count)
        {
            ShowCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    void ShowCurrentLine()
    {
        // Pulisce tutti i vecchi elementi nel canvas
        foreach (Transform child in canva.transform)
        {
            Destroy(child.gameObject);
        }

        // Crea nuova battuta e mostra testo corrente
        GameObject templateClone = Instantiate(conversationTemplate, canva.transform);
        templateClone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = dialogueLines[currentLineIndex];
    }

    void EndDialogue()
    {
        dialogueActive = false;
        canva.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerBody" || other.CompareTag("Player"))
        {
            player_detect = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "PlayerBody" || other.CompareTag("Player"))
        {
            player_detect = false;
            if (dialogueActive)
                EndDialogue(); // Chiudi il dialogo se esce dal trigger
        }
    }

}
