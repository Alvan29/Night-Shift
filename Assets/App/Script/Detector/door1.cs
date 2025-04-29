using UnityEngine;

public class door1 : MonoBehaviour
{
    public Transform obj; // assign di inspector
    public Vector3 openRotation = new Vector3(0, 90, 0); // rotasi saat terbuka
    public float openSpeed = 2f;

    private bool isPlayerNear = false;
    private Quaternion closedRotation;
    private Quaternion targetRotation;

    void Start()
    {
        closedRotation = obj.rotation;
        targetRotation = closedRotation;
    }

    void Update()
    {
        // Smoothly rotate the door to target rotation
        obj.rotation = Quaternion.Lerp(obj.rotation, targetRotation, Time.deltaTime * openSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            targetRotation = Quaternion.Euler(openRotation + closedRotation.eulerAngles);
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        isPlayerNear = false;
    //        targetRotation = closedRotation;
    //    }
    //}
}