using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("First Person")]
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject body;
    [SerializeField] private float maxY = 70f;
    [SerializeField] private float minY = -70f;
    private float pitch = 0f;
    private InputController inputController;

    void Start()
    {
        inputController = GetComponent<InputController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        OnLook();
    }

    private void OnLook()
    {
        if (!PlayerManager.instance.canLook) return;

        Vector2 lookInput = inputController.lookAction.ReadValue<Vector2>();
        lookInput.Normalize();
        body.transform.Rotate(Vector3.up, lookInput.x * Time.deltaTime * lookSensitivity); // Player body

        // Rotasi vertikal (Camera)
        pitch -= lookInput.y * Time.deltaTime * lookSensitivity;
        pitch = Mathf.Clamp(pitch, minY, maxY);
        cam.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
