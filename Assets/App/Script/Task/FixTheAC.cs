using UnityEngine;

public class FixTheAC : MonoBehaviour
{
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private string taskName;
    [SerializeField] private float currectProgress = 0f;
    [SerializeField] private float increaseProgress;
    [SerializeField] private float progressComplete = 100f;
    
    void OnEnable()
    {
        currectProgress = 0f;
        PlayerController.canMove = false;
    }

    void Update()
    {
        if (PlayerController.interact.WasPressedThisFrame())
        {
            increaseProgress = Random.Range(8, 10); 
            currectProgress += increaseProgress * Time.deltaTime;
            if (currectProgress >= progressComplete)
            {
                taskManager.CompleteTask(taskName);
            }
        }
    }
}
