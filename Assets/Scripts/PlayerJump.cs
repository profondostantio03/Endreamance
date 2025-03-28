using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    //public float moveSpeed = 5f; //il moveSpeed lo gestiamo in PlayerMovementAndCamera.cs
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Move(); //il moveSpeed lo gestiamo in PlayerMovementAndCamera.cs
        Jump();
    }

    /*void Move() //il moveSpeed lo gestiamo in PlayerMovementAndCamera.cs
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
    }*/

    void Jump()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}