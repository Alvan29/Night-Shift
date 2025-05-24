using UnityEngine;

public class FillTheGenerator : MonoBehaviour
{
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private string taskName = "Fill The Generator";
    [SerializeField] private float currectFuel = 0f;
    [SerializeField] private float increaseFuel = 20f;
    [SerializeField] private float maxFuel = 100f;

    void OnEnable()
    {
        currectFuel = 0f;
        PlayerController.canMove = false;
    }

    void Update()
    {
        if (PlayerController.interact.IsPressed())
        {
            currectFuel += increaseFuel * Time.deltaTime;
            if (currectFuel >= maxFuel)
            {
                taskManager.CompleteTask(taskName);
            }
        }
    }
}
