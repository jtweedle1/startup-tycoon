using System.Collections;
using System.Collections.Generic;
using UnityEngine; // Imports Unity Engine library, includes basic classes and functions
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Character movement speed, initialized to 5 units per second
    public Rigidbody2D rb; // Component that handles physics movements, can be assigned in Unity Inspector
    public Animator animator; // Component for handling/controlling animations, can be assigned in Unity Inspector

    private Vector2 movement; // Stores movement input; Vector 2 represents a point in 2D space w/ x and y coordinates
    private Vector2 lastMovement; // Stores last direction moved
    private PlayerControls controls; // Input action object

    // Called before the first frame update, i.e. usually when the first scene starts
    // Initializes controls and vind movement actions
    void Awake()
    {
        // Contains definitions for all input actions (Move, etc.) defined in Input Actions asset
        controls = new PlayerControls();

        // These lines listen to movement and stopping of movement 
        // `Move` handles 2D movement and returns a Vector2 value
        // `ctx` is `InputAction.CallbackContext` which provides what value was received and which control triggered it
        // `performed` event fires when input actions has been successfully recognized/when player presses a key
        // `movement = ctx.ReadValue<Vector2>();` reads value of Move action (a Vector2) and assigns to movement variable
        controls.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        // `canceled` event fires when Move action is canceled/released
        // `movement = Vector2.zero;` sets movement variable to vector (0,0) meaning no movement
        controls.Player.Move.canceled += ctx => movement = Vector2.zero;
    }

    // Called when the object becomes enabled and active
    private void OnEnable()
    {
        controls.Enable();
    }

    // Called when the object becomes disabled
    private void OnDisable()
    {
        controls.Disable();
    }

    // Called once per frame; tasks that need to be checked/updated frequently
    // Handles user input and updates animation parameters based on that input
    void Update()
    {
        // Print the input values to the Console
        Debug.Log("Horizontal: " + movement.x);
        Debug.Log("Vertical: " + movement.y);

        // Update animator with movement values
        animator.SetFloat("Horizontal", movement.x); // Set Horizontal parameter in Unity Animator to current x value
        animator.SetFloat("Vertical", movement.y); // Set Vertical parameter in Unity Animator to current y value
        animator.SetFloat("Speed", movement.sqrMagnitude); // Set Speed parameter in Unity Animator to square magnitude of movement vector (overall movement speed) -> when movement.sqrMagnitude is greater than 0.1 it triggers transition from idle to walking

        // If there's movement, update the last movement direction
        if (movement != Vector2.zero)
        {
            lastMovement = movement;
        }

        // Set the last direction values in the animator (only when movement changes)
        animator.SetFloat("LastHorizontal", lastMovement.x);
        animator.SetFloat("LastVertical", lastMovement.y);
    }

    // Called at fixed intervals, physics-related updates (moving the character)
    // Applies calculated position changes to the characters Rigidbody2D
    void FixedUpdate()
    {
        // Overall: moves Rigidbody2D to new position based on player input
        // rb.position gets current position of Rigidbody2D
        // movement * moveSpeed scales movement vector by moveSpeed -> how far the character should move per second
        // movement * moveSpeed * Time.fixedDeltaTime ensures movement is frame-rate independent
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
