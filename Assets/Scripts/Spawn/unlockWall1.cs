using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockWall1 : MonoBehaviour
{
    public GameObject wall;
    public float fadeDuration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(FadeAndDestroy(wall));
    }

    IEnumerator FadeAndDestroy(GameObject target)
    {
        Renderer renderer = target.GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("Renderer not found on the target");
            yield break; // Esci dalla coroutine se non c'è un renderer
        }
        Material mat = new Material(renderer.material); // Crea una copia del materiale
        renderer.material = mat;
        Color color = mat.color;
        float startAlpha = color.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeDuration);
            mat.color = new Color(color.r, color.g, color.b, newAlpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Debug.LogError("5");
        mat.color = new Color(color.r, color.g, color.b, 0f);
        Debug.Log("Wall faded out. Destroying the wall.");
        Destroy(target);
        Destroy(gameObject);
    }

}
