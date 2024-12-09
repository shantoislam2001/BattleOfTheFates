using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class AICharacter : MonoBehaviour
{
    public Vector3 target;             // Target position to follow
    public float walkSpeed = 2f;       // Speed for walking
    public float runSpeed = 4f;        // Speed for running
    public float roamRadius = 10f;     // Radius for random roaming
    public float roamInterval = 5f;    // Time interval between random movements

    private NavMeshAgent agent;        // NavMeshAgent component
    private Animator animator;         // Animator component
    private float roamTimer;           // Timer for random roaming
    private bool hasTarget = false;    // Flag to determine if a target is set
    private bool isRunning = false;    // Determines if AI should run

    void Start()
    {
        // Get the NavMeshAgent and Animator components
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Set the initial speed
        agent.speed = walkSpeed;

        // Initialize the roam timer
        roamTimer = roamInterval;
    }

    public void restrart()
    {
        Start();
    }

    void Update()
    {
        // Handle movement towards the target or roaming randomly
        if (hasTarget)
        {
            MoveTowardsTarget();
        }
        else
        {
            RoamRandomly();
        }

        // Update animation parameters
        float speed = agent.velocity.magnitude;
        animator.SetBool("IsMoving", speed > 0.1f);

        if (speed >= runSpeed)
        {
            animator.SetFloat("Speed", 1.0f); // Run animation
        }
        else if (speed >= walkSpeed)
        {
            animator.SetFloat("Speed", 0.5f); // Walk animation
        }
        else
        {
            animator.SetFloat("Speed", 0.0f); // Idle animation
        }
    }

    void MoveTowardsTarget()
    {
        agent.SetDestination(target);

        if (agent.remainingDistance < 0.5f)
        {
            agent.isStopped = true;
            animator.SetBool("IsMoving", false);
        }
        else
        {
            agent.isStopped = false;
        }
    }

    void RoamRandomly()
    {
        roamTimer -= Time.deltaTime;

        if (roamTimer <= 0f)
        {
            // Choose a random direction and position within the roam radius
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            randomDirection += transform.position;

            NavMeshHit navHit;
            if (NavMesh.SamplePosition(randomDirection, out navHit, roamRadius, NavMesh.AllAreas))
            {
                // Set the destination for roaming
                agent.SetDestination(navHit.position);

                // Randomize between walking and running
                isRunning = Random.value > 0.5f;
                agent.speed = isRunning ? runSpeed : walkSpeed;
            }

            roamTimer = roamInterval;
        }
    }

    // Prevent collisions with obstacles
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("AI"))
        {
            agent.isStopped = true;
            roamTimer = 0f; // Trigger immediate random roam
        }
    }

    // Public method to set the target as a Vector3 position
    public void SetTarget(Vector3 newTarget)
    {
        target = newTarget;
        hasTarget = true;
        agent.isStopped = false;
    }

    // Public method to clear the target and allow roaming
    public void ClearTarget()
    {
        hasTarget = false;
    }

    // Public method to switch to running
    public void StartRunning()
    {
        isRunning = true;
        agent.speed = runSpeed;
    }

    // Public method to switch to walking
    public void StopRunning()
    {
        isRunning = false;
        agent.speed = walkSpeed;
    }

    // Public method to check the AI GameObject name
    public string GetCharacterName()
    {
        return gameObject.name;
    }

}