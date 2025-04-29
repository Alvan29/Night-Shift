using UnityEngine;
using UnityEngine.InputSystem;

public class InteractObj : MonoBehaviour
{
    [SerializeField] private string needItem;

    // Update is called once per frame
    void Update()
    {
        if (Raycasting.targetObject == gameObject && PlayerController.interact.WasPerformedThisFrame())
        {
            string getItemName = PickupObj.currentlyHeldItem?.ItemName;
            if (getItemName == needItem)
            {
                Debug.Log("Terinteraksi");

            }
            else
            {
                Debug.Log("Gunakan " + needItem);
            }
        }
    }
}
