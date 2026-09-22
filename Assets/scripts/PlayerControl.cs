using UnityEngine;
using UnityEngine.InputSystem; // Required to use the Keyboard class

public class PlayerMovement : MonoBehaviour
{
    // Public variable visible and editable in the Inspector
    public float speed = 5f; 
    
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Reset movement vector every frame
        movement = Vector2.zero;

        // Safety check: skip if no keyboard is plugged in
        if (Keyboard.current == null) return;

        // Check specific keys using the new Input System
        if (Keyboard.current.wKey.isPressed) movement.y += 1;
        if (Keyboard.current.sKey.isPressed) movement.y -= 1;
        if (Keyboard.current.dKey.isPressed) movement.x += 1;
        if (Keyboard.current.aKey.isPressed) movement.x -= 1;

        // Normalize so diagonal movement isn't faster
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // Apply the velocity to the Rigidbody
        rb.linearVelocity = movement * speed;
    }
}