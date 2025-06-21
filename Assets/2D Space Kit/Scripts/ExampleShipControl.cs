using UnityEngine;
using System.Collections;

public class ExampleShipControl : MonoBehaviour
{
    public float moveSpeed = 8f; // Increased speed
    public float rotationSpeed = 180f; // Degrees per second
    public GameObject turret;
    public float turret_rotation_speed = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Screen.lockCursor = !Screen.lockCursor;

        float moveInput = 0f;
        float rotationInput = 0f;

        // Forward/Backward movement (W/S or Up/Down)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            moveInput = 1f;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            moveInput = -1f;

        // Rotation (A/D or Left/Right)
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            rotationInput = 1f;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            rotationInput = -1f;

        // Apply movement
        rb.linearVelocity = transform.up * moveInput * moveSpeed;

        // Apply rotation
        rb.angularVelocity = rotationInput * rotationSpeed;

        // Stop movement and rotation with C key
        if (Input.GetKey(KeyCode.C))
        {
            rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0, 0.5f);
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, 0.5f);
        }

        // Reset position to origin with H key
        if (Input.GetKey(KeyCode.H))
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
