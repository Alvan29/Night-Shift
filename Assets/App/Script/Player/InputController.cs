using UnityEngine;
using UnityEngine.InputSystem;
public class InputController : MonoBehaviour
{
    public PlayerInput playerInput;
    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction sprintAction;
    public InputAction interact;

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
