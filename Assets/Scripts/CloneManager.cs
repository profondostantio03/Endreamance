using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneManager : MonoBehaviour
{
    public GameObject objectToClone; // Oggetto da clonare
    public int numOfClone; // Numero di cloni da creare
    public GameObject[] clones; // Array per memorizzare i cloni

    private bool isCloningEnabled = false; // Flag per abilitare la clonazione

    // Update viene chiamato ad ogni frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMinus) && !isCloningEnabled)
        {
            StartCloning();
        }
        if (isCloningEnabled)
        {
            foreach (GameObject clone in clones)
            {
                if (clone != null)
                {
                    // Accedi al materiale e aggiorna il valore alpha
                    Color color = clone.GetComponent<Renderer>().material.color;
                    color.a = Random.value; // Imposta un valore alpha casuale
                    clone.GetComponent<Renderer>().material.color = color;
                }
            }
        }
    }
    // Metodo per abilitare la clonazione
    public void StartCloning()
    {
        if (objectToClone != null && numOfClone > 0)
        {
            clones = new GameObject[numOfClone];
            for (int i = 0; i < numOfClone; i++)
            {
                // Genera una posizione casuale
                Vector3 randomPosition = new Vector3(
                    Random.Range(-5f, 5f),
                    Random.Range(0f, 5f),
                    Random.Range(-5f, 5f)
                );

                // Istanziamo il clone
                clones[i] = Instantiate(objectToClone, randomPosition, Quaternion.identity);
            }
            isCloningEnabled = true; // Abilita la clonazione
        }
        else
        {
            Debug.LogError("Seleziona un oggetto da clonare e un numero valido di cloni.");
        }
    }
}
