using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuroScorrevole : MonoBehaviour
{
    [Header("Scorrimento Texture")]
    public float scrollSpeed = 0.1f;

    [Header("Pulsazione Trasparenza")]
    [Tooltip("Quanto velocemente cambia l'opacità.")]
    public float pulseSpeed = 0.1f;

    [Range(0f, 1f)] public float minAlpha = 0.25f; 
    [Range(0f, 1f)] public float maxAlpha = 0.5f;

    private Renderer rend;
    private Color baseColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        baseColor = rend.material.color;
    }

    void Update()
    {
        // gestione scorrimento, moltiplichiamo per Time.time per muoverlo costantemente
        float offset = Time.time * scrollSpeed;
        rend.material.mainTextureOffset = new Vector2(offset, 0);

        // gestine pulsazione, logica: (Tempo * Velocità) oscillante tra 0 e (Max - Min) + Base
        float alphaDifference = maxAlpha - minAlpha;
        float pulsazione = Mathf.PingPong(Time.time * pulseSpeed, alphaDifference) + minAlpha;

        // applichiamo il nuovo alpha mantenendo il colore originale
        rend.material.color = new Color(baseColor.r, baseColor.g, baseColor.b, pulsazione);
    }
}