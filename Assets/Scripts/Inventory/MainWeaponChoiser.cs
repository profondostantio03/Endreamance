using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeaponChoiser : MonoBehaviour
{
    [Header("Oggetto dove ci sono tutte le armi")]
    public Transform weaponHolder; // parte dell'oggetto player con l'arma
    private GameObject armaCorrente;
    public GameObject weaponSelectionArea;
    public GameObject pickupParticles;
    public float destroyDelay = 0.3f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeaponPickup"))
        {
            WeaponPickup pickup = other.GetComponent<WeaponPickup>();
            if (pickup != null)
            {
                AttivaArma(pickup.weaponName);
                StartCoroutine(DissolviAltreArmi());
                Destroy(other.gameObject); // rimuove l'arma selezionata a terra
                if (pickupParticles != null)
                {
                    Instantiate(pickupParticles, armaCorrente.transform.position + Vector3.up * 3f, Quaternion.identity);
                }
            }
        }
    }

    void AttivaArma(string nomeArma)
    {
        // disattiva tutte le armi in caso non lo fossero
        foreach (Transform arma in weaponHolder)
        {
            arma.gameObject.SetActive(false);
        }

        Transform nuovaArma = weaponHolder.Find(nomeArma);
        if (nuovaArma != null)
        {
            nuovaArma.gameObject.SetActive(true);
            armaCorrente = nuovaArma.gameObject;
            Debug.Log("Arma attivata: " + nomeArma);

            if (weaponSelectionArea != null)
                weaponSelectionArea.SetActive(false);
            Debug.Log("Disattivata la weapon selection area");
        }
        else
        {
            Debug.LogWarning("Nessuna arma trovata con nome: " + nomeArma);
        }
    }

    IEnumerator DissolviAltreArmi()
    {
        GameObject[] tutteLeArmi = GameObject.FindGameObjectsWithTag("WeaponPickup");

        foreach (GameObject arma in tutteLeArmi)
        {
            if (arma != null && arma != armaCorrente)
            {
                Debug.Log("Sto facendo fade su: " + arma.name);
                Instantiate(pickupParticles, arma.transform.position + Vector3.up * 2f, Quaternion.identity);

                FadeAndDestroy fade = arma.GetComponent<FadeAndDestroy>(); //va a richiamare lo script FadeAndDestroy, che e' uno script a parte nelle Utilities
                if (fade == null) fade = arma.AddComponent<FadeAndDestroy>();
               
                fade.fadeDuration = 1f;
                fade.StartFade();

                yield return new WaitForSeconds(destroyDelay); // delay prima di distruggere l'arma successiva
            }
        }
    }

    public GameObject GetArmaCorrente()
    {
        return armaCorrente;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
