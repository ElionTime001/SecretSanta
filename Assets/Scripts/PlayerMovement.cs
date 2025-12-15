using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;

    [Header("Movement")]
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float crouchSpeed = 3f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    [Header("Look")]
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    [Header("Crouch")]
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private float rotationX;

    private PlayerInputActions input;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isSprinting;
    private bool isCrouching;
    private bool jumpPressed;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = PlayerInputManager.InputActions;
    }

    void OnEnable()
    {
        input.Player.Enable();

        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += _ => moveInput = Vector2.zero;

        input.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        input.Player.Look.canceled += _ => lookInput = Vector2.zero;

        input.Player.Jump.performed += _ => jumpPressed = true;
        input.Player.Sprint.performed += _ => isSprinting = true;
        input.Player.Sprint.canceled += _ => isSprinting = false;

        input.Player.Crouch.performed += _ => isCrouching = true;
        input.Player.Crouch.canceled += _ => isCrouching = false;
    }

    void OnDisable()
    {
        input.Player.Disable();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
    }

    void HandleMovement()
    {
        float speed =
            isCrouching ? crouchSpeed :
            isSprinting ? runSpeed :
            walkSpeed;

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        float yVelocity = moveDirection.y;

        moveDirection =
            (forward * moveInput.y + right * moveInput.x) * speed;

        if (controller.isGrounded)
        {
            if (jumpPressed)
            {
                yVelocity = jumpPower;
            }
        }

        yVelocity -= gravity * Time.deltaTime;
        moveDirection.y = yVelocity;

        controller.height = isCrouching ? crouchHeight : defaultHeight;

        controller.Move(moveDirection * Time.deltaTime);
        jumpPressed = false;
    }

    void HandleLook()
    {
        rotationX -= lookInput.y * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * lookInput.x * lookSpeed);
    }
}
