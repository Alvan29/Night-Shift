using UnityEngine;
using UnityEngine.InputSystem;

public class PickupObj : MonoBehaviour, IPickupable
{
    [SerializeField] Transform handParent;
    [SerializeField] private PlayerInput playerInput;
    private InputAction pick;
    private Rigidbody rb;
    private bool canPick = true;

    [SerializeField] string itemName;
    string IPickupable.ItemName => itemName;
    public static IPickupable currentlyHeldItem;

    void Start()
    {
        pick = playerInput.actions.FindAction("Pick");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Raycasting.targetObject == gameObject && canPick && pick.WasPerformedThisFrame())
        {
            OnPickup(handParent);
            
        }
        else if (pick.WasPressedThisFrame() && canPick == false)
        {
            OnDrop(transform.position + transform.TransformDirection(Vector3.forward) * 1.5f);
        }
    }

    public void OnPickup(Transform handParent)
    {
        currentlyHeldItem = this;
        canPick = false;
        rb.isKinematic = true;
        rb.useGravity = false;
        // collider.enabled = false;

        transform.SetParent(handParent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void OnDrop(Vector3 dropPosition)
    {
        currentlyHeldItem = null;
        canPick = true;
        transform.SetParent(null);
        transform.position = dropPosition;

        rb.isKinematic = false;
        rb.useGravity = true;
        // collider.enabled = true;
    }
}
