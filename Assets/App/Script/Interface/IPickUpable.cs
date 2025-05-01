using NUnit.Framework.Interfaces;
using UnityEngine;

public interface IPickupable
{
    string ItemName { get; }
    void OnPickup(Transform handParent);
    void OnDrop(Vector3 dropPosition);
}
