// File: Inventory/PickupObject.cs
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PickupObject : MonoBehaviour, IPickupable
{
    [SerializeField] private string itemName = "Tool";
    public string ItemName => itemName;

    private Rigidbody rb;
    private Collider collider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    public void OnPickup(Transform handParent)
    {
        rb.isKinematic = true;
        rb.useGravity = false;
        collider.enabled = false;

        transform.SetParent(handParent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void OnDrop(Vector3 dropPosition)
    {
        transform.SetParent(null);
        transform.position = dropPosition;

        rb.isKinematic = false;
        rb.useGravity = true;
        collider.enabled = true;
    }
}
