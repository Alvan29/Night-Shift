using UnityEngine;

public class FixTheAC : MonoBehaviour
{
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private string taskName;
    [SerializeField] private float currectProgress = 0f;
    [SerializeField] private float increaseProgress;
    [SerializeField] private float progressComplete = 100f;
    [SerializeField] private Cloth cloth;
    [SerializeField] private Vector3 wind;

    private void OnDisable()
    {
        cloth.externalAcceleration = wind;
        currectProgress = 0f;
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
                cloth.externalAcceleration = wind;
                taskManager.CompleteTask(taskName);
            }
        }
        if (taskManager.IsTaskActive(taskName))
        {
            cloth.externalAcceleration = Vector3.zero;
        }
    }
}
