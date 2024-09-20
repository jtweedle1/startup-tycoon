using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LaptopInteraction : MonoBehaviour
{
    // Name of the scene to load when interacting with the laptop
    public string sceneToLoad;

    // Reference to the PlayerControls input action asset
    private PlayerControls controls;
    private bool playerInRange = false;

    private void Awake()
    {
        // Initialize the PlayerControls
        controls = new PlayerControls();

        // Register the interaction event
        controls.Player.Interact.performed += OnInteract;
    }

    private void OnEnable()
    {
        // Enable the input actions
        controls.Enable();
    }

    private void OnDisable()
    {
        // Disable the input actions
        controls.Disable();
    }

    // This method is called when another object enters the trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    // This method is called when another object exits the trigger collider
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        // If the player is in range and the interaction input is triggered
        if (playerInRange && context.performed)
        {
            LoadNewScene();
        }
    }

    private void LoadNewScene()
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
