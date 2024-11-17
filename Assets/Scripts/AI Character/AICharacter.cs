using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class AICharacter : MonoBehaviour
{
    public Transform target;           // Target to follow
    public float walkSpeed = 2f;       // Speed threshold for walking
    public float runSpeed = 4f;        // Speed threshold for running
    private NavMeshAgent agent;        // NavMeshAgent component
    private Animator animator;         // Animator component

    void Start()
    {
        // Get the NavMeshAgent and Animator components
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Set the NavMeshAgent speeds (walk/run)
        agent.speed = walkSpeed;
    }

    void Update()
    {
        // Check if there is a target and move towards it
        if (target != null)
        {
            agent.SetDestination(target.position);
            float speed = agent.velocity.magnitude;

            // Update Animator parameters based on the speed
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

           if (agent.remainingDistance < 2)
            {
                agent.isStopped = true; 
                animator.SetBool("IsMoving", false);
            }
            else
            {
                agent.isStopped = false;
            }
        }
    }

    // Call this method to update the target for the AI character
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
