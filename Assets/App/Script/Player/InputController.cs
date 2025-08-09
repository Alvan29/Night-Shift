using UnityEngine;
using UnityEngine.InputSystem;
public class InputController : MonoBehaviour
{
    public static InputController instance;
    public PlayerInput playerInput;
    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction sprintAction;
    public InputAction interact;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        lookAction = playerInput.actions.FindAction("Look");
        sprintAction = playerInput.actions.FindAction("Sprint");
        interact = playerInput.actions.FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
