using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private Camera[] cameras;
    [SerializeField] private string cameraOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
        if (other.gameObject.tag == "Player")
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
    }
}
