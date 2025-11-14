using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class KnockbackReceiver : MonoBehaviour
{
    private Rigidbody rb;
    private NavMeshAgent agent;
    private bool isBeingKnockedBack = false;
    public float knockbackDuration = 0.2f;
    public float knockbackResistance = 1f; // resistenza di default

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyKnockback(Vector3 direction, float force)
    {
        if (isBeingKnockedBack) return;
        float effectiveSpeed = force / knockbackResistance;
        if (effectiveSpeed < 0.1f) return;
        StartCoroutine(KnockbackRoutine(direction.normalized, effectiveSpeed, knockbackDuration));
    }

    private IEnumerator KnockbackRoutine(Vector3 direction, float speed, float duration)
    {
        isBeingKnockedBack = true;
        if (agent != null && agent.enabled)
        {
            agent.enabled = false;
        }
        float timer = 0;
        while (timer < duration)
        {
            float currentSpeed = Mathf.Lerp(speed, 0, timer / duration);

            // muove il transform manualmente
            transform.Translate(direction * currentSpeed * Time.deltaTime, Space.World);

            timer += Time.deltaTime;
            yield return null;
        }
        isBeingKnockedBack = false;
        if (agent != null)
        {
            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5.0f, NavMesh.AllAreas))
            {
                agent.Warp(hit.position); // teletrasporta l'agente alla posizione fisica
                agent.enabled = true;
            }
            else
            {
                Debug.LogWarning("Knockback ha spinto il nemico fuori dal NavMesh!");
            }
        }
    }
}
