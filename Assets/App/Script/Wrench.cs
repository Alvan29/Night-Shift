using UnityEngine;

public class Wrench : MonoBehaviour, IPickupable
{
    [SerializeField] private Transform target;
    public void OnPickup()
    {
        // gameObject.SetActive(false);
        gameObject.transform.SetParent(target, false);
        transform.position = target.transform.position;
    }
}
