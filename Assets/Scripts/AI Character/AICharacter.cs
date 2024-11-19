using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class AICharacter : MonoBehaviour
{
    public Transform target;           // Target to follow
    public float walkSpeed = 2f;       // Speed threshold for walking
    public float runSpeed = 4f;        // Speed threshold for running
    public float roamRadius = 10f;     // Radius for random roaming
    public float roamInterval = 5f;    // Time interval between random movements
    private NavMeshAgent agent;        // NavMeshAgent component
    private Animator animator;         // Animator component
    private float roamTimer;           // Timer for random roaming

    void Start()
    {
        // Get the NavMeshAgent and Animator components
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Set the NavMeshAgent speeds (walk/run)
        agent.speed = walkSpeed;

        // Initialize the roam timer
        roamTimer = roamInterval;
    }

    void Update()
    {
        // Handle movement towards the target or roaming randomly
        if (target != null)
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
        agent.SetDestination(target.position);

        if (agent.remainingDistance < 2f)
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
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            randomDirection += transform.position;

            NavMeshHit navHit;
            if (NavMesh.SamplePosition(randomDirection, out navHit, roamRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(navHit.position);
            }

            roamTimer = roamInterval;
        }
    }

    // Prevent collisions with obstacles
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Character"))
        {
            // Stop the character and choose a new direction to avoid collision
            agent.isStopped = true;
            roamTimer = 0f; // Trigger immediate random roam
        }
    }

    // Call this method to update the target for the AI character
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;

        // Resume moving if a new target is assigned
        if (target != null)
        {
            agent.isStopped = false;
        }
    }
}
