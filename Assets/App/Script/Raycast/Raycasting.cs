using UnityEngine;

public class Raycasting : MonoBehaviour
{
    private PopupDescription popup;
    public static GameObject targetObject;
    public static float distanceFromTarget;
    [SerializeField] float maxDistance;
    [SerializeField] float toTarget;
    void Update()
    {
        HandleRaycast();
        if (targetObject != null && InputController.instance.interact.WasPressedThisFrame())
        {
            HandleAllInteractions();
        }
    }
    private void HandleAllInteractions()
    {
        // Prioritaskan urutan pengecekan interface
        if (TryInteract<IInteractable>(x => x.Interact())) return;

        Debug.Log("No compatible interface found");
    }
    private bool TryInteract<T>(System.Action<T> action) where T : class
    {
        T component = targetObject.GetComponent<T>();
        if (component != null)
        {
            action(component);
            return true;
        }
        return false;
    }

    private void HandleRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.green);
            toTarget = hit.distance;
            distanceFromTarget = toTarget;
            targetObject = hit.collider.gameObject; // Simpan objek yang terkena ray
            //Debug.Log(targetObject);
        }
        else
        {
            toTarget = Mathf.Infinity;
            distanceFromTarget = Mathf.Infinity;
            targetObject = null;
        }
    }
}
