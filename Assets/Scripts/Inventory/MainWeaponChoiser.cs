using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeaponChoiser : MonoBehaviour
{
    [Header("Oggetto dove ci sono tutte le armi")]
    public Transform weaponHolder; // parte dell'oggetto player con l'arma
    private GameObject armaCorrente;
    public GameObject weaponSelectionArea;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeaponPickup"))
        {
            WeaponPickup pickup = other.GetComponent<WeaponPickup>();
            if (pickup != null)
            {
                AttivaArma(pickup.weaponName);
                Destroy(other.gameObject); // rimuove l'arma selezionata a terra
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
