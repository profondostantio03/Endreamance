using UnityEngine;

public class PlayerJumpFixed : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;
    public float fallMultiplier = 2.5f;
    public float tiltAmount = 5f; // inclinazione del personaggio

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Jump();

        // Permette un salto più corto se rilasci lo spazio prima di raggiungere l'apice
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.5f, rb.velocity.z);
        }
        // Inclinazione del personaggio in base alla velocità verticale
        float tilt = rb.velocity.y * tiltAmount;
        transform.rotation = Quaternion.Euler(tilt, transform.rotation.eulerAngles.y, 0);
    }

    void FixedUpdate()
    {
        // Aggiunge gravità extra quando il personaggio sta cadendo
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isGrounded = false; // Evita doppi salti accidentali
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
