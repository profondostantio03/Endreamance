using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallDamage : MonoBehaviour
{
    public CharacterStats stats;  // riferimento allo script delle statistiche
    public float minFallHeight = 3f; // altezza sotto la quale non prende danno
    public float damageMultiplier = 2f; // quanto danno fare per ogni unità sopra la soglia

    private Rigidbody rb;
    private float fallStartY;
    private bool isFalling = false;
    private bool wasGrounded = true;
    private float highestYBeforeFall;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    /*void Update()
    {
        if (!isFalling && !IsGrounded() && rb.velocity.y < 0)
        {
            // Inizio della caduta
            fallStartY = transform.position.y;
            isFalling = true;
        }
    }*/

    void FixedUpdate()
    {
        bool grounded = IsGrounded();

        if (!grounded && wasGrounded && rb.velocity.y < 0)
        {
            // Inizia la caduta
            highestYBeforeFall = transform.position.y;
            isFalling = true;
        }

        wasGrounded = grounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se ha colpito il terreno
        if (collision.gameObject.CompareTag("Terrain"))
        {
            /*float fallDistance = Mathf.Abs(rb.velocity.y);*/
            float fallDistance = highestYBeforeFall - transform.position.y;
            if (fallDistance > minFallHeight)
            {
                int damage = Mathf.RoundToInt((fallDistance - minFallHeight) * damageMultiplier);
                stats.TakeDamage(damage);
                Debug.Log($"Danno da caduta: {damage}");

            }
            isFalling = false;
        }
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.down * 0.1f, Vector3.down, 0.2f);
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
