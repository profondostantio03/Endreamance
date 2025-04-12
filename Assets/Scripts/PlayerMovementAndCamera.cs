using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAndCamera : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed = 5f; // Velocità di movimento
    public Camera playerCamera; // Riferimento alla telecamera

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Ottieni l'input dell'utente
        float horizontal = Input.GetAxis("Horizontal"); // A/D o Frecce sinistra/destra
        float vertical = Input.GetAxis("Vertical"); // W/S o Frecce su/giù

        // Crea un vettore di movimento basato sulla direzione della telecamera
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;

        // Annulla la componente Y della direzione della telecamera
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalizza le direzioni
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calcola la direzione del movimento
        Vector3 movement = (cameraForward * vertical + cameraRight * horizontal).normalized;

        // Muovi l'oggetto
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);


        // GESTIONE ANIMATOR
        /*if (movement.magnitude < 0.1f)
        {
            animator.Play("Idle_Normal_SwordAndShield");
        }
        else
        {
            if (vertical > 0.5f)
            {
                animator.Play("MoveFWD_Normal_RM_SwordAndShield");
            }
        }*/
        float moveAmount = movement.magnitude;
        animator.SetFloat("MoveSpeed", moveAmount);
    }
}
