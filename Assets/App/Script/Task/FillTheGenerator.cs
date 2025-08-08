using UnityEngine;

public class FillTheGenerator : MonoBehaviour, ITask
{
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private string taskName = "Fill The Generator";
    [SerializeField] private float currectFuel = 0f;
    [SerializeField] private float increaseFuel = 20f;
    [SerializeField] private float maxFuel = 100f;
    [SerializeField] private Light[] lights;
    [SerializeField] private GameObject[] objects;
    [SerializeField] private Cloth cloth;

    public void OnTaskActivated(string name)
    {
        if (name == taskName)
        {
            currectFuel = 0f;
            foreach (var light in lights)
            {
                light.enabled = false;
                cloth.externalAcceleration = Vector3.zero;
            }
            foreach (GameObject obj  in objects)
            {
                obj.SetActive(false);
            }
        }
    }

    public void OnTaskCompleted(string taskName)
    {
        foreach (var light in lights)
        {
            light.enabled = true;
            cloth.externalAcceleration = new Vector3(0f, 6f, 4f);
        }
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
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
