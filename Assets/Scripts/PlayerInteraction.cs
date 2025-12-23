using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    void Interact(GameObject interactor);
}

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public Transform interactorSource;   
    public float interactRange = 3f;
    public LayerMask interactableLayer;  

    private PlayerInputActions input;

    void Awake()
    {
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
                UIItemIndicator.Instance.ShowInteractPrompt();
                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    GameObject player = transform.parent.gameObject;
                    interactable.Interact(player);
                    Debug.Log($"Interacted with {hitInfo.collider.name}");
                }
            }
            else
            {
                UIItemIndicator.Instance.HideInteractPrompt();
                //Debug.Log($"Hit {hitInfo.collider.name}, but it is not interactable.");
            }
        }
        else
        {
            UIItemIndicator.Instance.HideInteractPrompt();
            //Debug.Log("Nothing to interact with.");
        }
    }
}
