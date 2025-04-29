using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public string currentItemName;

    public string GetActiveItemName()
    {
        return currentItemName;
    }

    public bool HasItem()
    {
        return currentItemName != null;
    }
}
