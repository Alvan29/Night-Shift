using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Outline outline;
    private Rigidbody rb;
    private Collider collider;

    private void Start()
    {
        outline = GetComponent<Outline>();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }
    public void OnPickup()
    {
        outline.enabled = false;
        rb.isKinematic = true;
        rb.useGravity = false;
        // gameObject.SetActive(false);
        transform.SetParent(target, false);
        transform.position = target.transform.position;
    }
    public void OnDrop()
    {
        outline.enabled = true;
        rb.isKinematic = false;
        rb.useGravity = true;

        transform.SetParent(null);

        // Tempatkan sedikit di depan player
        Vector3 dropOffset = new Vector3(0f, 0, 1.5f);
        transform.position = target.TransformPoint(dropOffset);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Detector"))
        {
            outline.OutlineMode = Outline.Mode.OutlineVisible;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Detector"))
        {
            outline.OutlineMode = Outline.Mode.OutlineHidden;
        }
    }

    public ItemData GetItemData()
    {
        throw new System.NotImplementedException();
    }

    public void OnPickup(Transform handParent)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrop(Vector3 dropPosition)
    {
        throw new System.NotImplementedException();
    }
}
