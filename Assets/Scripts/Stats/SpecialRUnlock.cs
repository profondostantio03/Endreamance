using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRUnlock : MonoBehaviour
{
    public PlayerCombat playerCombat;

    public GameObject unlockEffect;
    public AudioClip unlockSound;
    private AudioSource unlockAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (playerCombat == null)
        {
            playerCombat = FindAnyObjectByType<PlayerCombat>();
        }

        if (unlockSound != null)
        {
            unlockAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCombat.UnlockSkillR();
            if(unlockEffect != null) Instantiate(unlockEffect, transform.position, Quaternion.identity);
            if (unlockSound != null && unlockAudioSource != null) unlockAudioSource.PlayOneShot(unlockSound);  
            Destroy(gameObject);
        }
    }
}
