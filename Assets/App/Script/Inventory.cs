using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxSlots = 5;
    private List<GameObject> items = new List<GameObject>();
    public bool AddItem(GameObject item)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("Inventory full!");
            return false;
        }

        items.Add(item);
        Debug.Log($"Item picked up: {item.name}");
        return true;
    }
}
