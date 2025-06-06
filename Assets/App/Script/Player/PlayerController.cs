using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("First Person")]
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject body;
    [SerializeField] private float maxY = 70f;
    [SerializeField] private float minY = -70f;
    private float pitch = 0f;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 7f;
    [SerializeField] private bool isSprinting = false;
    [SerializeField] private Animator animator;
    private float currentSpeed;

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float currentStamina;
    [SerializeField] private float staminaDrain = 8;
    [SerializeField] private float staminaRegen = 5f;
    
    private CharacterController controller;
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction lookAction;
    InputAction sprintAction;
    public static InputAction interact;
    public static InputAction pick;
    public static bool canMove = true;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        lookAction = playerInput.actions.FindAction("Look");
        sprintAction = playerInput.actions.FindAction("Sprint");
        interact = playerInput.actions.FindAction("Interact");
        pick = playerInput.actions.FindAction("Pick");
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (canMove)
        {
            HandleStamina();
            onSprint();
            onMove();
            onLook();
        }
    }

    private void onMove()
    {
        currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
        Vector2 direction = moveAction.ReadValue<Vector2>();
        Vector3 move = (transform.forward * direction.y) + (transform.right * direction.x);
        move.Normalize(); // Pastikan gerakan diagonal tidak lebih cepat
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Animation Speed control
        //animator.SetFloat("Speed", direction.magnitude);
        //animator.SetFloat("Direction", direction.y);
        direction.Normalize();
        //animator.SetFloat("InputX", direction.x);
        //animator.SetFloat("InputY", direction.y);
    }

    private void onLook() { 
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        lookInput.Normalize();
        body.transform.Rotate(Vector3.up, lookInput.x * Time.deltaTime * lookSensitivity); // Player body
       
        // Rotasi vertikal (Camera)
        pitch -= lookInput.y * Time.deltaTime * lookSensitivity;
        pitch = Mathf.Clamp(pitch, minY, maxY);
        cam.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    private void onSprint()
    {
        bool isMoving = moveAction.ReadValue<Vector2>().magnitude > 0.1f;
        if (sprintAction.WasPressedThisFrame() && isMoving && currentStamina > 0)
        {
            isSprinting = true;
        }
        else if (sprintAction.WasReleasedThisFrame())
        {
            isSprinting = false;
        }
    }

    private void HandleStamina()
    {
        Mathf.Round(currentStamina);
        bool isMoving = moveAction.ReadValue<Vector2>().magnitude > 0.1f;
        if (isSprinting && isMoving && currentStamina > 0)
        {
            currentStamina -= staminaDrain * Time.deltaTime;
            if (currentStamina <= 0)
            {
                currentStamina = 0;
                isSprinting = false;
            }
        }
        else if (!isSprinting || !isMoving)
        {
            currentStamina += staminaRegen * Time.deltaTime;
            if (currentStamina >= maxStamina)
            {
                currentStamina = maxStamina;
            }
        }
    }
}
