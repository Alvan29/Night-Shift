// File: AI/NPCPatrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[System.Serializable]
public class Waypoint
{
    public Transform point;
    public float waitTime = 2f;
    public bool sitHere;
    public bool idleHere;
}

public class NPCPatrol : MonoBehaviour
{
    [SerializeField] private Waypoint[] waypoints;

    [Header("Panic Settings")]
    [SerializeField] private float panicLevel = 0f;
    [SerializeField] private float panicIncreaseAmount = 10f;
    [SerializeField] private float panicDecayRate = 2f;
    [SerializeField] private float maxPanic = 100f;

    private NavMeshAgent agent;
    private Animator animator;
    private int currentIndex = 0;
    private bool isWaiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        animator.SetBool("isIdle", false);

        if (waypoints.Length > 0 && waypoints[0].point != null)
            agent.SetDestination(waypoints[0].point.position);
    }

    void Update()
    {
        if (isWaiting || waypoints.Length == 0) return;

        animator.SetFloat("Speed", agent.velocity.magnitude);
        DecayPanic();

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            StartCoroutine(WaitAndMove());
        }
    }

    private IEnumerator WaitAndMove()
    {
        isWaiting = true;
        agent.isStopped = true;
        animator.SetFloat("Speed", 0f);

        Waypoint current = waypoints[currentIndex];

        if (current.sitHere)
        {
            animator.SetBool("isSitting", true);
            transform.localEulerAngles = new Vector3(0f, -170f, 0f);
        }
        else if (current.idleHere)
            animator.SetBool("isIdle", true);

        yield return new WaitForSeconds(current.waitTime);

        animator.SetBool("isSitting", false);
        animator.SetBool("isIdle", false);

        currentIndex = (currentIndex + 1) % waypoints.Length;
        if (waypoints[currentIndex].point != null)
            agent.SetDestination(waypoints[currentIndex].point.position);

        agent.isStopped = false;
        isWaiting = false;
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
}
