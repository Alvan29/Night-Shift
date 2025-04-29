using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [System.Serializable]
    public class Task
    {
        public string taskName;
        public GameObject taskObject;
        public bool isActive = false;
        public float panicIncrease = 0.2f;
    }

    public List<Task> tasks;
    public float minDelay = 5f;
    public float maxDelay = 10f;

    [Header("Panic System")]
    public float panicLevel = 0f;
    public float maxPanic = 1f;
    public float panicDecayRate = 0.05f; // per second

    private void Start()
    {
        StartCoroutine(RandomTaskActivator());
    }

    private void Update()
    {
        if (panicLevel > 0f)
        {
            panicLevel -= panicDecayRate * Time.deltaTime;
            panicLevel = Mathf.Clamp01(panicLevel);
        }
    }

    private System.Collections.IEnumerator RandomTaskActivator()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            Task task = GetRandomInactiveTask();
            if (task != null)
            {
                ActivateTask(task);
            }
        }
    }

    private Task GetRandomInactiveTask()
    {
        List<Task> inactiveTasks = tasks.FindAll(t => !t.isActive);
        if (inactiveTasks.Count == 0) return null;
        return inactiveTasks[Random.Range(0, inactiveTasks.Count)];
    }

    private void ActivateTask(Task task)
    {
        task.isActive = true;
        if (task.taskObject != null)
            task.taskObject.SetActive(true);

        panicLevel += task.panicIncrease;
        panicLevel = Mathf.Clamp01(panicLevel);

        Debug.Log($"Task Aktif: {task.taskName}, Panic: {panicLevel}");
    }

    public void CompleteTask(string taskName)
    {
        Task task = tasks.Find(t => t.taskName == taskName);
        if (task != null && task.isActive)
        {
            task.isActive = false;
            if (task.taskObject != null)
                task.taskObject.SetActive(false);

            Debug.Log($"Task Selesai: {task.taskName}");
        }
    }

    public bool IsTaskActive(string taskName)
    {
        Task task = tasks.Find(t => t.taskName == taskName);
        return task != null && task.isActive;
    }
}
