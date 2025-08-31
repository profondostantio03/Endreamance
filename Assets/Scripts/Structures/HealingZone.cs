using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    public float interactionDistance = 3f;
    public string interactionKey = "p";
    public GameObject player;
    public GameObject uiTextObject;

    private CharacterStats playerStats;
    private bool isInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        if (player != null)
        {
            playerStats = player.GetComponent<CharacterStats>();
        }

        if (uiTextObject != null)
            uiTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || playerStats == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= interactionDistance)
        {
            if (!isInRange)
            {
                isInRange = true;
                if (uiTextObject != null)
                    uiTextObject.SetActive(true);
            }
            if (Input.GetKeyDown(interactionKey))
            {
                playerStats.currentHealth = playerStats.maxHealth; 
                Debug.Log("Vita refillata");
            }
        }
        else
        {
            if (isInRange)
            {
                isInRange = false;
                if (uiTextObject != null)
                    uiTextObject.SetActive(false);
            }
        }
    }
}
