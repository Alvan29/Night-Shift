using UnityEngine;

public class Door : MonoBehaviour, IPickupable
{
    [SerializeField] private Transform target;
    private void OpenDoor()
    {
        Debug.Log("Door Opened!");
    }

    public void OnPickup()
    {
        // gameObject.SetActive(false);
        gameObject.transform.SetParent(target, false);
        transform.position = target.transform.position;
    }
}
