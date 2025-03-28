using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocita di movimento

    void Update()
    {
        // Ottieni l'input dell'utente
        float horizontal = Input.GetAxis("Horizontal"); // A/D o Frecce sinistra/destra
        float vertical = Input.GetAxis("Vertical"); // W/S o Frecce su giu

        // Crea un vettore di movimento
        Vector3 movement = new Vector3(horizontal, 0, vertical);

        // Normalizza il vettore di movimento per evitare movimenti più veloci in diagonale
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        // Muovi l'oggetto
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}
