using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractObj : MonoBehaviour
{
    [SerializeField] private string taskName;
    [SerializeField] private string needItem;
    [SerializeField] private MonoBehaviour taskScriptToEnable;
    [SerializeField] private TaskManager taskManager;

    private void Update()
    {
        bool taskActive = taskManager.IsTaskActive(taskName);
        if (taskActive && Raycasting.targetObject == gameObject && PlayerController.interact.WasPerformedThisFrame())
        {
            string getItemName = PickupObj.currentlyHeldItem?.ItemName;

            if (getItemName == needItem)
            {
                Debug.Log("Terinteraksi dengan item yang sesuai");
                taskScriptToEnable.enabled = true;
            }
            else
            {
                Debug.Log("Membutukan " + needItem + " untuk Interaksi");
            }
        }
    }
}
