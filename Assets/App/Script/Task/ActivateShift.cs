using UnityEngine;

public class ActivateShift : MonoBehaviour
{
    [SerializeField] private NPCPatrol[] NPC;

    void Update()
    {
        if (Raycasting.targetObject == gameObject && PlayerController.pick.WasPressedThisFrame())
        {
            foreach (NPCPatrol npc in NPC)
            {
                npc.enabled = true;
            }
            Debug.Log("Pressed");
        }

    }
}
