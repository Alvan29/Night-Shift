using UnityEngine;

public class MoveController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 7f;
    [SerializeField] private bool isSprinting = false;
    [SerializeField] private Animator animator;
    private float speed;
    private float currentSpeed;
    private float dirX;
    private float dirY;
    private bool isMoving;
    private CharacterController controller;
    private InputController inputController;

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float currentStamina;
    [SerializeField] private float staminaDrain = 8;
    [SerializeField] private float staminaRegen = 5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        inputController = GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnSprint();
        HandleStamina();        
    }

    private void OnMove()
    {
        if (!PlayerManager.instance.canMove) return;

        currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
        Vector2 direction = inputController.moveAction.ReadValue<Vector2>();
        Vector3 move = (transform.forward * direction.y) + (transform.right * direction.x * 0.5f);
        move.Normalize(); // Pastikan gerakan diagonal tidak lebih cepat
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Animation Speed control
        if (!isMoving)
        {
            currentSpeed = 0f;
        }
        speed = Mathf.SmoothStep(speed, currentSpeed, 0.5f);
        animator.SetFloat("Speed", speed);

        dirX = Mathf.SmoothStep(dirX, direction.x, 0.5f);
        dirY = Mathf.SmoothStep(dirY, direction.y, 0.5f);
        animator.SetFloat("InputX", dirX);
        animator.SetFloat("InputY", dirY);
    }
    private void OnSprint()
    {
        isMoving = inputController.moveAction.ReadValue<Vector2>().magnitude > 0.1f;
        if (inputController.sprintAction.WasPressedThisFrame() && isMoving && currentStamina > 0)
        {
            isSprinting = true;
        }
        else if (inputController.sprintAction.WasReleasedThisFrame())
        {
            isSprinting = false;
        }
    }

    private void HandleStamina()
    {
        Mathf.Round(currentStamina);
        bool isMoving = inputController.moveAction.ReadValue<Vector2>().magnitude > 0.1f;
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
