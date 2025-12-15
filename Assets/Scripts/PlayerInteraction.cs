using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    void Interact();
}

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public Transform interactorSource;   // Usually the player camera
    public float interactRange = 3f;
    public LayerMask interactableLayer;  // Optional: only hit specific layers

    private PlayerInputActions input;

    void Awake()
    {
        // Get reference to input actions
        input = PlayerInputManager.InputActions;
        Debug.Log(input == null ? "InputActions is null!" : "InputActions loaded correctly");
    }

    void OnEnable()
    {
        input.Player.Enable();
        input.Player.Interact.performed += OnInteractPerformed;
    }

    void OnDisable()
    {
        input.Player.Disable();
    }

    void Update()
{
    
    TryInteract();
    
}


    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        TryInteract();
    }

    private void TryInteract()
    {
        Ray ray = new Ray(interactorSource.position, interactorSource.forward);
        Debug.DrawRay(interactorSource.position, interactorSource.forward * interactRange, Color.red, 1f);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactRange, interactableLayer))
        {
            if (hitInfo.collider.TryGetComponent(out IInteractable interactable))
            {
                //Debug.Log($"Collided with an interractable object!");
                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    interactable.Interact();
                    Debug.Log($"Interacted with {hitInfo.collider.name}");
                }
            }
            else
            {
                //Debug.Log($"Hit {hitInfo.collider.name}, but it is not interactable.");
            }
        }
        else
        {
            //Debug.Log("Nothing to interact with.");
        }
    }
}
