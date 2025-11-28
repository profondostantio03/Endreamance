using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityTeleporter : MonoBehaviour
{
    public Transform playerRoot;

    [Header("Interaction Settings")]
    public float interactionDistance = 3.0f;
    public KeyCode activationKey = KeyCode.V;

    [Header("Teleport Area Settings")]
    public Collider destinationArea;
    public LayerMask groundLayer;
    [Tooltip("offset per non far entrare i piedi nel terreno")]
    public float verticalOffset = 1.0f;
    private bool isPlayerNearby = false;

    // Update is called once per frame
    void Update()
    {
        if (playerRoot == null || destinationArea == null) return;

        float distance = Vector3.Distance(transform.position, playerRoot.position);
        // verifica se il player e' abbastanza vicino
        if (distance <= interactionDistance)
        {
            isPlayerNearby = true;
            // mettere una UI per mostrare "Premi V per teletrasportarti"
            if (Input.GetKeyDown(activationKey))
            {
                TeleportPlayer();
            }
        }
        else
        {
            isPlayerNearby = false;
        }
    }

    public void TeleportPlayer()
    {
        if(destinationArea == null)
        {
            Debug.LogError("Hai dimenticato di assegnare la Destination Area nell'Inspector!");
            return;
        }

        Vector3 targetPosition = GetRandomPointOnGround();

        CharacterController cc = playerRoot.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false; // disattiva momentaneamente la fisica
            playerRoot.position = targetPosition;
            cc.enabled = true;  
        }
        else
        {
            playerRoot.position = targetPosition;
        }

        Debug.Log($"Teletrasporto eseguito a: {targetPosition}");
    }

    private Vector3 GetRandomPointOnGround()
    {
        Bounds bounds = destinationArea.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        // raycast dall'alto verso il basso per trovare il terreno
        float startHeight = bounds.max.y + 50f;
        Vector3 rayStart = new Vector3(randomX, startHeight, randomZ);
        RaycastHit hit;

        float rayLenght = 50f + bounds.size.y + 10f;

        if (Physics.Raycast(rayStart, Vector3.down, out hit, rayLenght, groundLayer))
        {
            return hit.point + Vector3.up * verticalOffset;
        }

        Debug.LogWarning("Terreno non trovato, uso il centro dell'area.");
        return bounds.center + Vector3.up * verticalOffset;
    }
}
