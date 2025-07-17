using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSphere : MonoBehaviour
{
    public float boostForce = 10f;
    public ParticleSystem boostParticles;

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
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        if (rb != null)
        {
        // annulla la velocità Y per evitare lo spam di salti
            Vector3 velocity = rb.velocity;
            velocity.y = 0f;
            rb.velocity = velocity;
            // forza verso l'alto
            rb.AddForce(Vector3.up * boostForce, ForceMode.Impulse);

            if (boostParticles != null)
            {
                ContactPoint contact = collision.contacts[0];
                ParticleSystem particles = Instantiate(boostParticles, contact.point, Quaternion.identity);
                particles.Play();
                Destroy(particles.gameObject, particles.main.duration + particles.main.startLifetime.constantMax);
            }
        }
    }
}
