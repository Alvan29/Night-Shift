using UnityEngine;

public class FixTheAC : MonoBehaviour, ITask
{
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private string taskName;
    [SerializeField] private float currectProgress = 0f;
    [SerializeField] private float increaseProgress;
    [SerializeField] private float progressComplete = 100f;
    [SerializeField] private Cloth cloth;
    [SerializeField] private Vector3 wind;

    public void OnTaskActivated(string name)
    {
        if (name == taskName)
        {
            currectProgress = 0f;
            cloth.externalAcceleration = Vector3.zero;
        }
    }

    public void OnTaskCompleted(string name)
    {
        if (name == taskName)
        {
            cloth.externalAcceleration = wind;
        }
    }

    void Update()
    {
        if (PlayerController.interact.WasPressedThisFrame())
        {
            // PlayerController.canMove = false;
            increaseProgress = Random.Range(8, 10);
            Mathf.Round(increaseProgress);
            currectProgress += increaseProgress;
            if (currectProgress >= progressComplete)
            {
                PlayerController.canMove = true;
                //cloth.externalAcceleration = wind;
                taskManager.CompleteTask(taskName);
            }
        }
    }
}
