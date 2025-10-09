using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DirectionalJump : MonoBehaviour
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

    public bool localDirection = false; //impostato a true, si regola in base alla direzione dell'oggetto con lo script

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
        if (other.CompareTag("Player")){
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                Vector3 jumpDir = Vector3.zero;

                switch (direction)
                {

                case JumpDirection.Forward:
                        jumpDir = localDirection ? transform.forward : Vector3.forward;
                    break;

                case JumpDirection.Backward:
                        jumpDir = localDirection ? -transform.forward : Vector3.back;
                    break;

                case JumpDirection.Left:
                        jumpDir = localDirection ? -transform.right : Vector3.left;
                    break;

                case JumpDirection.Right:
                        jumpDir = localDirection ? transform.right : Vector3.right;
                    break;
                }
                Vector3 finalForce = (jumpDir.normalized * horizontalForce) + (Vector3.up * jumpForce);
                rb.AddForce(finalForce, ForceMode.VelocityChange);
            }
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 dir = Vector3.zero;
        switch (direction)
        {
            case JumpDirection.Forward: dir = localDirection ? transform.forward : Vector3.forward; break;
            case JumpDirection.Backward: dir = localDirection ? -transform.forward : Vector3.back; break;
            case JumpDirection.Left: dir = localDirection ? -transform.right : Vector3.left; break;
            case JumpDirection.Right: dir = localDirection ? transform.right : Vector3.right; break;
        }
        Gizmos.DrawRay(transform.position + Vector3.up * 0.2f, dir.normalized * 2f);
    }
}
