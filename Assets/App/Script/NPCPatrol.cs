using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    private int currentWaypointIndex = 0;

    [Header("Panic Settings")]
    public float panicLevel = 0f;
    public float panicIncreaseAmount = 10f;
    public float panicDecayRate = 2f;
    public float maxPanic = 100f;

    [Header("Sit & Idle Settings")]
    public int sitWaypointIndex = 1; // Waypoint where NPC sits
    public int idleWaypointIndex = 4; // Waypoint where NPC idles
    public float waitTime = 3f;
    private float waitTimer = 0f;
    private bool isWaiting = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        SetWalkingState(true);
    }

    void Update()
    {
        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                isWaiting = false;
                animator.SetBool("isSitting", false);
                animator.SetBool("isIdle", false);
                SetWalkingState(true); // Start walking again after waiting
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
            return;
        }

        Patrol();
        DecayPanic();
    }

    void Patrol()
    {
        if (waypoints.Length == 0 || waypoints[currentWaypointIndex] == null) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;

        transform.position += direction.normalized * speed * Time.deltaTime;

        // Check if NPC has reached the current waypoint
        if (direction.magnitude < 0.2f)
        {
            // Handle sitting at the specified waypoint
            if (currentWaypointIndex == sitWaypointIndex)
            {
                isWaiting = true;
                waitTimer = waitTime;
                SetWalkingState(false);
                animator.SetBool("isSitting", true);
            }
            // Handle idling at the specified waypoint
            else if (currentWaypointIndex == idleWaypointIndex)
            {
                isWaiting = true;
                waitTimer = waitTime;
                SetWalkingState(false);
                animator.SetBool("isIdle", true);
            }
            else
            {
                // Move to the next waypoint
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }

        // Smoothly rotate NPC to face the target waypoint
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void DecayPanic()
    {
        if (panicLevel > 0f)
        {
            panicLevel -= panicDecayRate * Time.deltaTime;
            panicLevel = Mathf.Max(panicLevel, 0f);
        }
    }

    public void HearDistraction(Vector3 soundPosition)
    {
        float distance = Vector3.Distance(transform.position, soundPosition);
        if (distance < 10f)
        {
            panicLevel += panicIncreaseAmount;
            panicLevel = Mathf.Clamp(panicLevel, 0f, maxPanic);
            Debug.Log($"{gameObject.name} mendengar suara! Panic: {panicLevel}");
        }
    }

    void SetWalkingState(bool walking)
    {
        animator.SetBool("isWalking", walking);
        if (walking)
        {
            animator.SetBool("isSitting", false);
            animator.SetBool("isIdle", false);
        }
    }
}
