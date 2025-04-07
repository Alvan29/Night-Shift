using UnityEngine;

public class ItemPickup : MonoBehaviour, IPickupable
{
    [SerializeField] private Inventory playerInventory;
    public void OnPickup()
    {
        bool added = playerInventory.AddItem(gameObject);
        if (added)
        {
            gameObject.SetActive(false);
        }
    }
}
