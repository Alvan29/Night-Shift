using UnityEngine;
using UnityEngine.WSA;

public class Raycasting : MonoBehaviour
{
    private PopupDescription popup;
    public static GameObject targetObject;
    public static float distanceFromTarget;
    [SerializeField] float maxDistance;
    [SerializeField] float toTarget;
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.green);
            toTarget = hit.distance;
            distanceFromTarget = toTarget;
            targetObject = hit.collider.gameObject; // Simpan objek yang terkena ray
        }
        else
        {
            toTarget = Mathf.Infinity;
            distanceFromTarget = Mathf.Infinity;
            targetObject = null;
        }
    }
}
