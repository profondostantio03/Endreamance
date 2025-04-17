using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAndCamera : MonoBehaviour
{
    private Animator characterAnimator;
    private float normalSpeed = 0.75f;
    private float sprintSpeed = 1.25f;
    public float moveSpeed = 5f; // Velocità di movimento
    public float runningSpeed = 6f; // serve per poter aggiornare la velocità del player quando corre
    public float baseSpeed;
    public Camera playerCamera; // Riferimento alla telecamera

    void Start()
    {
        characterAnimator = GetComponent<Animator>();// //sostituito da characterAnimator.SetFloat in Update

    }

    void Update()
    {
        // Ottieni l'input dell'utente
        float horizontal = Input.GetAxis("Horizontal"); // A/D o Frecce sinistra/destra
        float vertical = Input.GetAxis("Vertical"); // W/S o Frecce su/giù

        // per la gestione dell'animator
        characterAnimator.SetFloat("MoveX", horizontal);
        characterAnimator.SetFloat("MoveY", vertical);

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

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && vertical > 0;

        baseSpeed = isSprinting ? runningSpeed : moveSpeed;

        // per aggiornare lo stato delle animazioni

        characterAnimator.SetFloat("MoveX", horizontal);
        characterAnimator.SetFloat("MoveY", vertical);
        characterAnimator.SetBool("isSprinting", isSprinting);

        /*if (Input.GetKey(KeyCode.LeftShift) && vertical > 0) // PER CAMBIARE LA SPEED SE SI CORRE
        {
            baseSpeed = runningSpeed;
        }
        else
        {
            baseSpeed = moveSpeed;
        }*/
        // Muovi l'oggetto
        transform.Translate(movement * baseSpeed * Time.deltaTime, Space.World); // qui moveSpeed si cambia con baseSpeed che avrà due valori differenti a seconda dello sprint


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

        float speed = 0;

        if (Input.GetKey(KeyCode.LeftShift) && vertical > 0)
        {
            speed = sprintSpeed; // Imposta l'animazione per la velocità normale
        }
        else if (vertical > 0)
        {
            speed = normalSpeed; // Imposta l'animazione per la velocità normale
        }
        characterAnimator.SetFloat("Speed", speed);

        /*float moveAmount = movement.magnitude;
        characterAnimator.SetFloat("MoveSpeed", moveAmount);*/
    }
}
