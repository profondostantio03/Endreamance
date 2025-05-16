using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private float dayLength = 60f; // Durata di un giorno in secondi
    [SerializeField] private Light directionalLight;
    [SerializeField] private Material skyboxMaterial;

    private void Update()
    {
        float time = Time.time / dayLength;
        float rotationDegrees = time * 360f;

        transform.rotation = Quaternion.Euler(rotationDegrees, 170f, 0);

        float intensityMultiplier = Mathf.Clamp01((Mathf.Sin(time * 2f * Mathf.PI - Mathf.PI / 2f) + 1f) / 2f);
        directionalLight.intensity = Mathf.Lerp(0f, 1f, intensityMultiplier);

        Color skyColor = Color.Lerp(new Color(0.1f, 0.1f, 0.3f), new Color(0.5f, 0.5f, 1f), intensityMultiplier);
        RenderSettings.ambientLight = skyColor;
        if (skyboxMaterial != null)
            skyboxMaterial.SetColor("_Tint", skyColor);
    }
}