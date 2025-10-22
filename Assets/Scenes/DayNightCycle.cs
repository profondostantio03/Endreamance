using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering; 

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] public float dayLength = 60f;
    [SerializeField] private Light directionalLight;
    [SerializeField] private Material skyboxMaterialTemplate; 
    [SerializeField] private float skyboxRotationSpeed = 1f;

    private float skyboxRotation = 0f;

    private Material skyboxInstance;  
    private Material originalSkybox;  

    // colori per alba/tramonto e notte 
    private readonly Color defaultSkyColor = new Color(0.509f, 0.462f, 0.541f); 
    private readonly Color nightSkyColor = new Color(0.1f, 0.1f, 0.3f);     // notte

    private void Start()
    {
        // forza modalità skybox e resetta l'ambiente per pulire lo stato
        RenderSettings.ambientMode = AmbientMode.Skybox;
        RenderSettings.ambientLight = Color.black;
        DynamicGI.UpdateEnvironment(); 

        
        if (!directionalLight)
        {
            directionalLight = GetComponent<Light>();
        }
        RenderSettings.sun = directionalLight;

        // --- CREAZIONE ISTANZA ---
        // salva lo skybox originale per poterlo ripristinare alla fine
        originalSkybox = RenderSettings.skybox;

        if (skyboxMaterialTemplate != null)
        {
            // crea una *copia* (istanza) del materiale
            skyboxInstance = new Material(skyboxMaterialTemplate);
            // assegna la *copia* ai RenderSettings
            RenderSettings.skybox = skyboxInstance;
        }
        else
        {
            Debug.LogWarning("DayNightCycle: Nessun materiale skybox assegnato! Uso quello di default.");
            skyboxInstance = originalSkybox; 
        }
    }

    private void Update()
    {
        if (directionalLight == null || skyboxInstance == null) return;

        // calcolo del tempo e rotazione del sole
        float time = Time.time / dayLength;
        float rotationDegrees = time * 360f;
        transform.rotation = Quaternion.Euler(rotationDegrees, 170f, 0);

        // --- MODIFICA SOLO L'ISTANZA (LA COPIA) ---

        // Rotazione Skybox
        skyboxRotation += Time.deltaTime * skyboxRotationSpeed;
        skyboxInstance.SetFloat("_Rotation", skyboxRotation);

        // intensità Luce
        float intensityMultiplier = Mathf.Clamp01((Mathf.Sin(time * 2f * Mathf.PI - Mathf.PI / 2f) + 1f) / 2f);
        directionalLight.intensity = Mathf.Lerp(0f, 1f, intensityMultiplier);

        // colore Skybox
        Color skyColor = Color.Lerp(nightSkyColor, defaultSkyColor, intensityMultiplier);
        skyboxInstance.SetColor("_Tint", skyColor);
    }

    private void OnDestroy()
    {
        // --- PULIZIA ---

        // resetta i valori globali
        RenderSettings.sun = null;
        RenderSettings.skybox = originalSkybox; // Ripristina lo skybox originale

        // distrugge la copia del materiale che abbiamo creato
        if (skyboxInstance != null)
        {

#if UNITY_EDITOR
            DestroyImmediate(skyboxInstance, true);
#else
                // Nella build finale serve Destroy
                Destroy(skyboxInstance);
#endif
        }
    }
}