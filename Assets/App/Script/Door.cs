using UnityEditor;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    private void OpenDoor()
    {
        Debug.Log("Door Opened!");
    }

    
}
