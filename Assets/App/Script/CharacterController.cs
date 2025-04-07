using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 7f;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject body;
    private Vector2 lookInput;
    private float yaw;
    private float pitch;
    private float currentSpeed;
    [SerializeField] private bool isSprinting = false;

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float currentStamina;
    [SerializeField] private float staminaDrain = 8;
    [SerializeField] private float staminaRegen = 5f;

    public InteractionDetector detector;
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction lookAction;
    InputAction interactAction;
    InputAction sprintAction;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        lookAction = playerInput.actions.FindAction("Look");
        interactAction = playerInput.actions.FindAction("Interact");
        sprintAction = playerInput.actions.FindAction("Sprint");
    }

    void Update()
    {
        HandleStamina();
        onSprint();
        onMove();
        onLook();
    }

    private void onMove()
    {
        currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
        Vector2 direction = moveAction.ReadValue<Vector2>();
        Vector3 move = (transform.forward * direction.y) + (transform.right * direction.x);
        move.Normalize(); // Pastikan gerakan diagonal tidak lebih cepat
        transform.position += move * currentSpeed * Time.deltaTime;
    }

    private void onLook() { 
        lookInput = lookAction.ReadValue<Vector2>();
        yaw += lookInput.x * lookSensitivity * Time.deltaTime;
        pitch -= lookInput.y * lookSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.rotation = Quaternion.Euler(0f, yaw, 0f); // Player body
        cam.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f); // Camera up-down
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
