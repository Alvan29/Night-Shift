using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IPickupable item;
    private IInteractable interactable;

    [SerializeField] private GameObject player;
    [SerializeField] private bool onHold = true;
    [SerializeField] private Transform handParent;

    [SerializeField] private PlayerInput playerInput;
    private InputAction interact;
    private InputAction pick;
    private Outline currentOutline;
    private InventoryManager inventory;


    private void Start()
    {
        playerInput = player.GetComponent<PlayerInput>();
        interact = playerInput.actions.FindAction("Interact");
        pick = playerInput.actions.FindAction("Pick");
        inventory = player.GetComponent<InventoryManager>();
    }

    private void Update()
    {
        if (pick.WasPressedThisFrame())
        {
            Debug.Log(item);
            if (item != null && !onHold)
            {
                inventory.GetActiveItemName();
                item.OnPickup(handParent);
                onHold = true;
            }
            else if (onHold && item != null)
            {
                item.OnDrop(transform.position + transform.forward * 1.5f);
                onHold = false;
                item = null;
            }
        }

        if (interact.WasPressedThisFrame() && onHold && interactable != null)
        {
            if (inventory != null && interactable.RequiredToolName == inventory.GetActiveItemName())
            {
                Debug.Log("Interact");
                interactable.Interact();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        item = other.GetComponent<IPickupable>();
        interactable = other.GetComponent<IInteractable>();

        if (item != null && other.TryGetComponent(out Outline outline))
        {
            outline.OutlineMode = Outline.Mode.OutlineVisible;
            currentOutline = outline;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (currentOutline != null)
        {
            currentOutline.OutlineMode = Outline.Mode.OutlineHidden;
            currentOutline = null;
        }

        if (!onHold && other.GetComponent<IPickupable>() == item)
        {
            item = null;
        }

        if (other.GetComponent<IInteractable>() == interactable)
        {
            interactable = null;
        }
    }
}

