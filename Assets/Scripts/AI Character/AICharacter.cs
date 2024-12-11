using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class AICharacter : MonoBehaviour
{
    public Vector3 target;             // Target position to follow
    public float walkSpeed = 2f;       // Speed for walking
    public float runSpeed = 4f;        // Speed for running
    public float roamInterval = 5f;    // Time interval between random movements
    public float stoppingDistance = 1f; // Distance to stop near the destination

    private NavMeshAgent agent;        // NavMeshAgent component
    private Animator animator;         // Animator component
    private float roamTimer;           // Timer for random roaming
    public bool hasTarget = false;    // Flag to determine if a target is set
    private bool isRunning = false;    // Determines if AI should run
    public event Action OnReachedDestination;

    void Start()
    {
        // Get the NavMeshAgent and Animator components
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Set the initial speed
        agent.speed = walkSpeed;

        // Initialize the roam timer
        roamTimer = roamInterval;

        // Set stopping distance to ensure smooth stopping
        agent.stoppingDistance = stoppingDistance;
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

        // Update animation parameters based on agent velocity
        float speed = agent.velocity.magnitude;
        animator.SetBool("IsMoving", speed > 0.1f);

        if (speed > walkSpeed * 0.5f)
        {
            animator.SetFloat("Speed", isRunning ? 1.0f : 0.5f); // Run or Walk animation
        }
        else
        {
            animator.SetFloat("Speed", 0.0f); // Idle animation
        }
    }

    void MoveTowardsTarget()
    {
        agent.SetDestination(target);

        // Stop moving when close to the target
        if (agent.remainingDistance <= stoppingDistance && !agent.pathPending)
        {
            agent.isStopped = true;
            animator.SetBool("IsMoving", false);

            // Trigger the callback
            OnReachedDestination?.Invoke();
            OnReachedDestination = null;
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
            Vector3 randomPosition = GetValidRandomPosition();
            if (randomPosition != Vector3.zero)
            {
                // Set the destination for roaming
                agent.SetDestination(randomPosition);

                // Randomize between walking and running
                isRunning = UnityEngine.Random.value > 0.5f; // 50% chance to run or walk
                agent.speed = isRunning ? runSpeed : walkSpeed;

                agent.isStopped = false; // Ensure agent starts moving
            }

            roamTimer = roamInterval;
        }

        // Check if the character has reached its destination
        if (agent.remainingDistance <= stoppingDistance && !agent.pathPending)
        {
            agent.isStopped = true;
            animator.SetBool("IsMoving", false);
        }
    }

    Vector3 GetValidRandomPosition()
    {
        // Generate a random position within the NavMesh
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * 50f; // Larger roaming area
        randomDirection += transform.position;

        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randomDirection, out navHit, 50f, NavMesh.AllAreas))
        {
            // Check if the terrain below is walkable
            RaycastHit hit;
            if (Physics.Raycast(navHit.position + Vector3.up * 2, Vector3.down, out hit, 4f))
            {
                if (hit.collider.CompareTag("Terrain"))
                {
                    float slope = Vector3.Angle(hit.normal, Vector3.up);
                    if (slope <= 30f) // Ensure it's not steep
                    {
                        return navHit.position;
                    }
                }
            }
        }

        return Vector3.zero;
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
