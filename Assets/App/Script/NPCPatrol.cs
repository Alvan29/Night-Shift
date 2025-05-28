using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private int sitWaypointIndex = 1;
    [SerializeField] private int idleWaypointIndex = 1;

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

        if (waypoints.Length > 0)
            agent.SetDestination(waypoints[currentIndex].position);
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

        if (currentIndex == sitWaypointIndex)
        {
            animator.SetBool("isSitting", true);
        }
        else if (currentIndex == idleWaypointIndex)
        {
            animator.SetBool("isIdle", true);
        }

        yield return new WaitForSeconds(waitTime);

        animator.SetBool("isSitting", false);
        animator.SetBool("isIdle", false);
        currentIndex = (currentIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentIndex].position);
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
