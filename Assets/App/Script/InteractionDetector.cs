using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable detectedObject;

    void OnTriggerEnter(Collider other)
    {
        Outline outline = other.GetComponent<Outline>();
        IInteractable interactable = other.GetComponent<IInteractable>();
        IPickupable pickup = other.GetComponent<IPickupable>();
        if (interactable != null)
        {
            outline.OutlineMode = Outline.Mode.OutlineVisible;
            detectedObject = interactable;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Outline outline = other.GetComponent<Outline>();
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null && detectedObject == interactable)
        {
            outline.OutlineMode = Outline.Mode.OutlineHidden;
            detectedObject = null;
        }
    }

    public void TryInteract()
    {
        if (detectedObject != null)
        {
            detectedObject.Interact();
        }
    }
}
