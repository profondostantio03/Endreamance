using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooterTest : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 100f;

    void FixedUpdate()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);
    }
}
