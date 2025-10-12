using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAndDestroy : MonoBehaviour
{
    [Header("Durata dissolvenza")]
    public float fadeDuration = 1f;

    public bool autoDestroy = true;
    private bool isFading = false;
    private Material matInstance;
    private Renderer[] renderers; // supporta più materiali
    private Material[] materials;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();

        // crea istanza unica dei materiali per non modificare il prefab
        materials = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            materials[i] = new Material(renderers[i].material);
            renderers[i].material = materials[i];

            // forza modalità Fade/Transparent se possibile
            if (materials[i].HasProperty("_Mode"))
            {
                materials[i].SetFloat("_Mode", 2f); // 2 = Fade
                materials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                materials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                materials[i].SetInt("_ZWrite", 0);
                materials[i].DisableKeyword("_ALPHATEST_ON");
                materials[i].EnableKeyword("_ALPHABLEND_ON");
                materials[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
                materials[i].renderQueue = 3000;
            }
        }
    }

    public void StartFade()
    {
        if (!isFading)
            StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        isFading = true;

        float elapsed = 0f;

        Color[] startColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            startColors[i] = materials[i].color;
        }

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / fadeDuration);

            for (int i = 0; i < materials.Length; i++)
            {
                Color c = startColors[i];
                c.a = Mathf.Lerp(1f, 0f, progress);
                materials[i].color = c;
            }

            yield return null;
        }

        // alpha a 0 finale
        for (int i = 0; i < materials.Length; i++)
        {
            Color c = startColors[i];
            c.a = 0f;
            materials[i].color = c;
        }

        if (autoDestroy)
            Destroy(gameObject);
    }
}
