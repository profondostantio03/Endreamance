using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLight : MonoBehaviour
{
    public enum LightColor
    {
        Yellow,
        Red,
        Blue,
        Green,
        Purple,
        White
    }

    public LightColor selectedColor = LightColor.Yellow;

    private Light glowLight;
    private float baseIntensity = 3f; // Intensità media della luce
    private float pulseSpeed = 2f;    // Velocità del battito
    private float pulseAmount = 1f;   // Quanto si "espande" la luce

    void Start()
    {
        // Crea una luce se non c'è
        glowLight = gameObject.AddComponent<Light>();
        glowLight.type = LightType.Point;
        glowLight.range = 5f;
        glowLight.intensity = baseIntensity;
        glowLight.color = Color.white;
        glowLight.color = GetColorFromSelection();
    }

    void Update()
    {
        if (glowLight != null)
        {
            // Applica un effetto di pulsazione usando il seno
            glowLight.intensity = baseIntensity + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        }
    }
    Color GetColorFromSelection()
    {
        switch (selectedColor)
        {
            case LightColor.Yellow: return Color.yellow;
            case LightColor.Red: return Color.red;
            case LightColor.Blue: return Color.blue;
            case LightColor.Green: return Color.green;
            case LightColor.Purple: return new Color(0.5f, 0f, 0.5f);
            case LightColor.White: return Color.white;
            default: return Color.white;
        }
    }
}

