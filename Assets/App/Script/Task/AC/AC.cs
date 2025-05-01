using UnityEngine;

public class AC : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject qte;
    [SerializeField] private string requiredToolName;

    public string RequiredToolName => requiredToolName;

    public void Interact()
    {
        if (qte != null)
        {
            qte.SetActive(true);
            Debug.Log("QTE diaktifkan oleh interaksi player dengan alat yang sesuai.");
        }
        else
        {
            Debug.LogWarning("QTE object belum di-assign pada " + name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && qte != null)
        {
            qte.SetActive(false);
        }
    }
}
