using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreak : MonoBehaviour
{
    public GameObject breakEffect; // Un prefab per l'effetto di rottura
    public int hitsToBreak = 3; // Numero di colpi necessari per rompere il blocco

    private int currentHits = 0; // Contatore dei colpi attuali


    void Update()
    {
        // Controlla se il tasto sinistro del mouse è stato premuto
        if (Input.GetMouseButtonDown(0)) // 0 è il tasto sinistro del mouse
        {
            // Controlla se il mouse è sopra il blocco
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform) // Controlla se il blocco è stato colpito
                {
                    currentHits++; // Incrementa il contatore dei colpi
                    Debug.Log("Colpi attuali: " + currentHits); // Stampa il numero di colpi attuali

                    // Controlla se il numero di colpi ha raggiunto il limite
                    if (currentHits >= hitsToBreak)
                    {
                        BreakBlock();
                    }
                }
            }
        }

        void BreakBlock()
        {
            // Instanzia l'effetto di rottura se presente
            if (breakEffect != null)
            {
                Instantiate(breakEffect, transform.position, Quaternion.identity);
            }

            // Distruggi il blocco
            Destroy(gameObject);
        }
    }
}