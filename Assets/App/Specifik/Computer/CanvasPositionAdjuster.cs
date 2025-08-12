using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasPositionAdjuster : MonoBehaviour
{
    [SerializeField] private float zOffset = -0.1f;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform screenTransform;

    void Update()
    {
        // Sesuaikan posisi canvas dengan layar komputer
        Vector3 screenForward = screenTransform.forward;
        transform.position = screenTransform.position + screenForward * zOffset;
        transform.rotation = Quaternion.LookRotation(screenForward, screenTransform.up);

        // Sesuaikan scale jika diperlukan
        transform.localScale = screenTransform.lossyScale;
    }
}