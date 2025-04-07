using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        OpenDoor();
    }

    private void OpenDoor()
    {
        Debug.Log("Door Opened!");
    }
}
