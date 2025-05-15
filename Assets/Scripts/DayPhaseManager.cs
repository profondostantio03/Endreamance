using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayPhaseManager : MonoBehaviour
{
    [SerializeField] private float dayDuration = 48f; // durata giorno
    [SerializeField] private Light sunLight;
    [SerializeField] private float startTime = 8f;

    [Header("Sky Settings")]
    [SerializeField] private Material skyboxMaterial;
    [SerializeField] private Color dayColor = new Color(0.533f, 0.4f, 0.576f);
    [SerializeField] private Color nightColor = new Color(0.1f, 0.1f, 0.2f);
    [SerializeField] private Color dawnDuskColor = new Color(1f, 0.6f, 0.4f);

    private float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!sunLight)
        {
            sunLight = GetComponent<Light>();
        }
        currentTime = startTime / 24f * dayDuration;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= dayDuration)
        {
            currentTime = 0;
        }

        float currentHour = (currentTime / dayDuration) * 24f;

        float lightIntensity = CalculateLightIntensity(currentHour);

        float rotationAngle = (currentTime / dayDuration) * 360f;
        transform.rotation = Quaternion.Euler(rotationAngle, -30f, 0);

        sunLight.intensity = lightIntensity;
        UpdateAmbientLight(lightIntensity);
        UpdateSkyColor(currentHour, lightIntensity);

        /*float intensity = Mathf.Clamp01(Mathf.Sin(rotationAngle * Mathf.Deg2Rad));
        sunLight.intensity = Mathf.Lerp(0.1f, 1f, intensity);*/
    }
    public float GetCurrentHour()
    {
        return (currentTime / dayDuration) * 24f;
    }

    private float CalculateLightIntensity(float hour)
    {
        // più basso 2:00 (2.0f), più alto alle 13:00 (13.0f)
        float intensity;

        if (hour < 2.0f) // prima delle due
        {
            intensity = Mathf.Lerp(0.2f, 0.1f, hour / 2.0f);
        }
        else if (hour < 13.0f) // dalle due alle 13
        {
            intensity = Mathf.Lerp(0.1f, 1.0f, (hour - 2.0f) / 11.0f);
        }
        else // After 1 PM
        {
            intensity = Mathf.Lerp(1.0f, 0.2f, (hour - 13.0f) / 11.0f);
        }

        return Mathf.Clamp01(intensity);
    }

    private void UpdateAmbientLight(float intensity)
    {
        Color ambientColor = Color.Lerp(
            new Color(0.1f, 0.1f, 0.15f), // notte
            new Color(0.6f, 0.6f, 0.6f),  // giorno
            intensity
        );

        RenderSettings.ambientLight = ambientColor;
    }

    private void UpdateSkyColor(float hour, float intensity)
    {
        Color baseColor;

        if (hour < 2.0f) // prima delle 2
        {
            baseColor = Color.Lerp(nightColor, nightColor * 0.5f, hour / 2.0f);
        }
        else if (hour < 13.0f) 
        {
            float t = (hour - 2.0f) / 11.0f;
            baseColor = Color.Lerp(nightColor, dayColor, t);
        }
        else
        {
            float t = (hour - 13.0f) / 11.0f;
            baseColor = Color.Lerp(dayColor, nightColor, t);
        }

        skyboxMaterial.SetColor("_Tint", baseColor * intensity);
    }

}
