using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IPickupable item;

    [SerializeField] private GameObject player;
    PlayerInput playerInput;
    InputAction interact;

    private void Start()
    {
        playerInput = player.GetComponent<PlayerInput>();
        interact = playerInput.actions.FindAction("Interact");
    }

    private void Update()
    {
        if (interact.WasPressedThisFrame() && item != null)
        {
            item.OnPickup();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Outline outline = other.GetComponent<Outline>();
        item = other.GetComponent<IPickupable>();
        if (item != null)
        {
            outline.OutlineMode = Outline.Mode.OutlineVisible;
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (item != null)
        {
            other.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
            item = null;
        }
    }
}
