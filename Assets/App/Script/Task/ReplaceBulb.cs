using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class ReplaceBulb : MonoBehaviour, ITask
{
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private string taskName;
    [SerializeField] private float currentProg;
    [SerializeField] private float increaseProg;
    [SerializeField] private float maxProg;
    [SerializeField] private GameObject bulb;
    [SerializeField] private Light lamp;
    [SerializeField] private float flickerDuration;
    [SerializeField] private GameObject child;

    public void OnTaskActivated(string name)
    {
        if (name == taskName)
        {
            currentProg = 0;
            StartCoroutine(FlickerThenBreak());
        }
    }

    public void OnTaskCompleted(string name)
    {
        if (name == taskName)
        {
            foreach (Transform child in child.transform)
            {
                Destroy(child.gameObject);
            }
            PickupObj.currentlyHeldItem = null;
            lamp.enabled = true;
            bulb.SetActive(true);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.interact.IsPressed())
        {
            currentProg += increaseProg * Time.deltaTime;
            if (currentProg >= maxProg)
            {
                taskManager.CompleteTask(taskName);
            }
        }
    }
    private IEnumerator FlickerThenBreak()
    {
        float elapsed = 0f;
        while (elapsed < flickerDuration)
        {
            lamp.enabled = !lamp.enabled;
            float delay = Random.Range(0.1f, 0.3f);
            yield return new WaitForSeconds(delay);
            elapsed += delay;
        }

        lamp.enabled = false;
        bulb.SetActive(false);
    }
}
