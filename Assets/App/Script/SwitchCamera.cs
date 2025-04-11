using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private Camera[] cameras;
    [SerializeField] private string cameraOn;
    private Outline outline;

    private void Start()
    {
        outline = GetComponent<Outline>();
    }

    public void enableCamera(string cameraOn)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].name == cameraOn)
            {
                cameras[i].enabled = true;
            }
            else
            {
                cameras[i].enabled = false;
            }
        }
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


}
