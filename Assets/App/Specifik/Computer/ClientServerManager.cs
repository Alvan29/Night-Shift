using UnityEngine;

public class ClientServerManager : MonoBehaviour
{
    public static ClientServerManager instance;

    [SerializeField] private ProblemServerData data;
    public float textSize = 1.5f;
    private int taskComplete = 0;
    private int serverHealth = 10;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ProcessTask(bool correct)
    {
        if (correct)
        {
            taskComplete++;
            if (taskComplete == 5)
            {
                Debug.Log("Lanjut Story");
            }
        }
        else
        {
            int decrease = Random.Range(1, 3);
            serverHealth -= decrease;
            if (serverHealth <= 0)
            {
                Debug.Log("Game Over");
            }
        }
    }

    private void GetRandomTask()
    {

    }
}
