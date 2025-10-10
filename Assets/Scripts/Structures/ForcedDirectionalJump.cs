using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedDirectionalJump : MonoBehaviour
{
    public enum JumpDirection
    {
        Forward,
        Backward,
        Left,
        Right
    }

    public JumpDirection direction = JumpDirection.Forward;
    public float jumpForce = 9f;
    public float horizontalForce = 7f;
    public bool localDirection = false;

    //controlli
    public bool requireFromAbove = true;      // attiva solo se il player arriva sopra
    public float requiredDownwardVelocity = -1f; // soglia verticale per considerare l'arrivo dall'alto
    public float minimalIncomingSpeed = 0.1f;   // soglia per usare la velocity, altrimenti si usa la posizione
    public float launchCooldown = 0.25f;

    private Dictionary<Rigidbody, float> lastLaunchTime = new Dictionary<Rigidbody, float>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody rb = other.attachedRigidbody ?? other.GetComponent<Rigidbody>();
        if (rb == null) return;

        float now = Time.time;
        if (lastLaunchTime.TryGetValue(rb, out float last) && now - last < launchCooldown) return;

        // per attivare solo dall'alto
        if (requireFromAbove)
        {
            if (other.transform.position.y <= transform.position.y + 0.1f) return; // controllo posizione (player sopra l'oggetto)
            
            if (rb.velocity.y > requiredDownwardVelocity) return; // controllo velocità verso il basso (caduta)
        }

        // calcolo vettore di ingresso (solo XZ)
        Vector3 incoming = rb.velocity;
        Vector3 incomingXZ = new Vector3(incoming.x, 0f, incoming.z);

        if (incomingXZ.sqrMagnitude < minimalIncomingSpeed * minimalIncomingSpeed)
        {
            // usa la direzione dall'oggetto verso il player
            incomingXZ = new Vector3(other.transform.position.x - transform.position.x, 0f, other.transform.position.z - transform.position.z);
            if (incomingXZ.sqrMagnitude < 0.001f)
            {
                incomingXZ = Vector3.forward; // ultimo fallback: usa forward del mondo
            }
        }
        incomingXZ.Normalize();

        // costruisco la direzione di uscita basata sulla scelta
        Vector3 outgoingXZ;
        switch (direction)
        {
            case JumpDirection.Forward:
                outgoingXZ = incomingXZ; 
                break;

            case JumpDirection.Backward:
                outgoingXZ = -incomingXZ; 
                break;

            case JumpDirection.Left:
                outgoingXZ = Quaternion.Euler(0f, -90f, 0f) * incomingXZ; 
                break;

            case JumpDirection.Right:
                outgoingXZ = Quaternion.Euler(0f, 90f, 0f) * incomingXZ; 
                break;

            default:
                outgoingXZ = incomingXZ; 
                break;
        }
        outgoingXZ.Normalize();

        // azzera la velocità attuale per applicare un lancio clean
        rb.velocity = Vector3.zero;

        // applica il "colpo" istantaneo (horizontal + vertical)
        Vector3 finalChange = outgoingXZ * horizontalForce + Vector3.up * jumpForce;
        rb.AddForce(finalChange, ForceMode.VelocityChange);

        // segna il lancio per cooldown
        lastLaunchTime[rb] = now;
    }

    // gizmo in scena (mostra forward del pad)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 p = transform.position + Vector3.up * 0.2f;
        Gizmos.DrawRay(p, transform.forward * 1.5f);
        Gizmos.DrawRay(p, -transform.forward * 1.5f);
        Gizmos.DrawRay(p, transform.right * 1.5f);
    }

}
