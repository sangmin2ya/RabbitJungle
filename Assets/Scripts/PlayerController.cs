using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Import the Input System namespace

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the capsule
    private Vector2 moveInput; // Store movement input
    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        // Update is not used in this example
    }

    void FixedUpdate()
    {
        // Apply movement in FixedUpdate for physics calculations
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    // This method will be called by the Input System
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // Read the movement vector
    }
}
