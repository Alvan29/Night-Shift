using UnityEngine;

public class FillTheGenerator : MonoBehaviour
{
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private string taskName = "Fill The Generator";
    [SerializeField] private float currectFuel = 0f;
    [SerializeField] private float increaseFuel = 0.2f;
    [SerializeField] private float maxFuel = 100f;
    private bool taskIsActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        currectFuel = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (taskIsActive == true)
        {
            if (PlayerController.interact.IsPressed())
            {
                currectFuel += increaseFuel * Time.deltaTime;
            }

            if (currectFuel >= maxFuel)
            {
                taskManager.CompleteTask(taskName);
            }
        }
    }
}
