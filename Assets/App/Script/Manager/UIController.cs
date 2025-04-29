using UnityEngine;

public class UIController : MonoBehaviour
{
    public static string actionTask;
    public static string commandTask;
    public static string buttonTask;
    public static bool uiActive;

    [SerializeField] GameObject actionBox;
    [SerializeField] GameObject commandBox;
    [SerializeField] GameObject interactCrossAir;

    // Update is called once per frame
    void Update()
    {
        if (Raycasting.targetObject != null && Raycasting.targetObject.GetComponent<PopupDescription>() != null)
        {
            uiActive = true;
            UiActive();
        }
        else
        {
            uiActive = false;
            HideUI();
        }
    }

    private void UiActive()
    {
        actionBox.SetActive(true);
        commandBox.SetActive(true);
        interactCrossAir.SetActive(true);
        actionBox.GetComponent<TMPro.TMP_Text>().text = "[" + buttonTask + "] " + actionTask;
        commandBox.GetComponent<TMPro.TMP_Text>().text = commandTask;
    }
    private void HideUI()
    {
        actionBox.SetActive(false);
        commandBox.SetActive(false);
        interactCrossAir.SetActive(false);
    }
}
