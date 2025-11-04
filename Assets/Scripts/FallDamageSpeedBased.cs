using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallDamageSpeedBased : MonoBehaviour
{
    public CharacterStats stats; 
    public float minFallVelocity = 10f; 
    public float damageMultiplier = 2f; 
    public string groundTag = "Terrain"; 

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            // 'relativeVelocity' è la velocità combinata dei due oggetti in collisione e si usa per ottenere la velocità di impatto verticale
            float fallSpeed = collision.relativeVelocity.y;

            if (fallSpeed > minFallVelocity)
            {
                int damage = Mathf.RoundToInt((fallSpeed - minFallVelocity) * damageMultiplier);

                if (stats != null)
                {
                    stats.TakeDamage(damage);
                }
                Debug.Log($"Danno da caduta: {damage} (Velocità impatto: {fallSpeed:F1})");
            }
        }
    }
}