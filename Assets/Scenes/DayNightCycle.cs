using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] public float dayLength = 60f;
    [SerializeField] private Light directionalLight;
    [SerializeField] private Material skyboxMaterial;
    [SerializeField] private float skyboxRotationSpeed = 1f;
    private float skyboxRotation = 0f;

    private readonly Color defaultSkyColor = new Color(0.509f, 0.462f, 0.541f); // #82768A
    private readonly float defaultLightIntensity = 1f;
    private readonly Quaternion defaultRotation = Quaternion.Euler(50f, 170f, 0);

    private void Start()
    {
        if (!directionalLight)
        {
            directionalLight = GetComponent<Light>();
        }
        UnityEngine.RenderSettings.sun = directionalLight;
        UnityEngine.RenderSettings.skybox = skyboxMaterial;
    }

    private void Update()
    {
        float time = Time.time / dayLength;
        float rotationDegrees = time * 360f;

        transform.rotation = Quaternion.Euler(rotationDegrees, 170f, 0);

        // rotazione skybox
        skyboxRotation += Time.deltaTime * skyboxRotationSpeed;
        UnityEngine.RenderSettings.skybox.SetFloat("_Rotation", skyboxRotation);

        float intensityMultiplier = Mathf.Clamp01((Mathf.Sin(time * 2f * Mathf.PI - Mathf.PI / 2f) + 1f) / 2f);
        directionalLight.intensity = Mathf.Lerp(0f, 1f, intensityMultiplier);

        Color skyColor = Color.Lerp(new Color(0.1f, 0.1f, 0.3f), defaultSkyColor, intensityMultiplier);
        if (skyboxMaterial != null)
        {
            skyboxMaterial.SetColor("_Tint", skyColor);
            UnityEngine.RenderSettings.skybox.SetColor("_Tint", skyColor);
        }
    }


    private void OnDisable()
    {
        ResetLighting();
    }
    private void ResetLighting()
    {
        transform.rotation = Quaternion.Euler(50f, 170f, 0);
        directionalLight.intensity = 1f;
        if (skyboxMaterial != null)
        {
            skyboxMaterial.SetColor("_Tint", defaultSkyColor);
            UnityEngine.RenderSettings.skybox.SetColor("_Tint", defaultSkyColor);
        }
    }
}