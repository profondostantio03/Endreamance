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
    public float typingSpeed = 0.02f;
    private Coroutine typingCoroutine;

    public GameObject playerObject; // Assegna il Player da Inspector
    private PlayerMovementAndCamera playerMovement;
    public bool givesQuest = false;
    public string questID;
    private bool questGiven = false;

    public GameObject choicesPanel;

    // Start is called before the first frame update
    void Start()
    {
        dialogueLines.Add("Ciao viaggiatore.");
        dialogueLines.Add("Presta attenzione in questo paese, non e' tutto oro quel che luccica.");
        dialogueLines.Add("*CHOICE*");
        dialogueLines.Add("Non perdere di vista la tua luce.");

        if (playerObject != null)
            playerMovement = playerObject.GetComponent<PlayerMovementAndCamera>();

        canva.SetActive(false); // Nascondi canvas all'inizio
        if (choicesPanel != null)
            choicesPanel.SetActive(false);
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

        if (playerMovement != null)
            playerMovement.enabled = false;

        ShowCurrentLine();
    }

    void NextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Count)
        {
            if (dialogueLines[currentLineIndex] == "*CHOICE*") //per far comparire il panel della scelta durante il dialogo
            {
                TriggerChoice();
            }
            else
            {
                ShowCurrentLine();
            }
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
        TextMeshProUGUI textUI = templateClone.GetComponentInChildren<TextMeshProUGUI>();

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        // Avvia il typewriter effect
        typingCoroutine = StartCoroutine(TypeText(textUI, dialogueLines[currentLineIndex]));
    }

    IEnumerator TypeText(TextMeshProUGUI textComponent, string line)
    {
        textComponent.text = "";
        foreach (char c in line)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        dialogueActive = false;
        canva.SetActive(false);
        if (playerMovement != null)
            playerMovement.enabled = true;

        if (choicesPanel != null)
            choicesPanel.SetActive(false);
    }

    void ShowChoices()
    {
        if (choicesPanel != null)
        {
            canva.SetActive(false);
            choicesPanel.SetActive(true);
        }
    }

    public void TriggerChoice()
    {
        if (dialogueActive && !questGiven && givesQuest)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            ShowChoices();
        }
    }

    // chiamata da un bottone UI
    public void AcceptQuest()
    {
        Debug.Log("Missione accettata: " + questID);
        questGiven = true;
        EndDialogue();
        // segnare nel QuestManager globale
    }

    public void DeclineQuest()
    {
        Debug.Log("Missione rifiutata");
        EndDialogue();
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
