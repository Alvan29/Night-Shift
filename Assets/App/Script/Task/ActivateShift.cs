using UnityEngine;

public class ActivateShift : MonoBehaviour
{
    [SerializeField] private NPCPatrol[] NPC;
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private MonoBehaviour[] scripts;

    void Update()
    {
        if (Raycasting.targetObject == gameObject && PlayerController.pick.WasPressedThisFrame())
        {
            foreach (NPCPatrol npc in NPC)
            {
                npc.enabled = true;
            }
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }
            taskManager.StartTask();
            Debug.Log("Pressed");
        }
    }
}
