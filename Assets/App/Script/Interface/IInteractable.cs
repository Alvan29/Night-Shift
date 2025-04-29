using UnityEngine;

public interface IInteractable
{
    string RequiredToolName { get;}

    void Interact();
}
