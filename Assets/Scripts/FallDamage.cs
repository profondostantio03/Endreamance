using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallDamage : MonoBehaviour
{
    public CharacterStats stats;  // riferimento allo script delle statistiche
    public float minFallVelocity = 10f; // velocità sotto la quale non prende danno
    public float damageMultiplier = 2f; // quanto danno fare per ogni unità sopra la soglia

    private Rigidbody rb;
    private float fallStartY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se ha colpito il terreno
        if (collision.gameObject.CompareTag("Terrain"))
        {
            float fallSpeed = Mathf.Abs(rb.velocity.y);

            if (fallSpeed > minFallVelocity)
            {
                int damage = Mathf.RoundToInt((fallSpeed - minFallVelocity) * damageMultiplier);
                stats.TakeDamage(damage);
                Debug.Log($"Danno da caduta: {damage}");

            }
        }
    }
}

/*using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallDamage : MonoBehaviour
{
    public CharacterStats stats;  // riferimento allo script delle statistiche
    public float minFallVelocity = 10f; // velocità sotto la quale non prende danno
    public float damageMultiplier = 2f; // quanto danno fare per ogni unità sopra la soglia

    private Rigidbody rb;
    private float fallStartY;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se ha colpito il terreno
        if (collision.gameObject.CompareTag("Ground")) // Assicurati che il terreno abbia il tag "Ground"
        {
            float fallSpeed = Mathf.Abs(rb.velocity.y);

            if (fallSpeed > minFallVelocity)
            {
                int damage = Mathf.RoundToInt((fallSpeed - minFallVelocity) * damageMultiplier);
                stats.TakeDamage(damage);
                Debug.Log($"Danno da caduta: {damage}");
            }
        }
    }
}
*/
