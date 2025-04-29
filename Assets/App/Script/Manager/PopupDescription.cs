using UnityEngine;

public class PopupDescription : MonoBehaviour
{
    [SerializeField] private string action;
    [SerializeField] private string command;
    [SerializeField] private string button;

    private void Update()
    {
        if (Raycasting.targetObject == gameObject)
        {
            ShowDesc();
        }
    }
    private void ShowDesc()
    {
        UIController.actionTask = action;
        UIController.commandTask = command;
        UIController.buttonTask = button;
        UIController.uiActive = true;
    }
}
