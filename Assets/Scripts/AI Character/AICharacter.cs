using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacter : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private static Dictionary<string, AICharacter> characters = new Dictionary<string, AICharacter>();

    public string characterName;
    public Transform target;
    private Action onTargetReachedCallback;

    private void Awake()
    {
        if (string.IsNullOrEmpty(characterName))
        {
            Debug.LogError($"Character {gameObject.name} needs a unique name.");
            enabled = false;
            return;
        }

        if (characters.ContainsKey(characterName))
        {
            Debug.LogError($"Character name {characterName} already exists.");
            enabled = false;
            return;
        }

        characters[characterName] = this;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget > agent.stoppingDistance)
            {
                agent.isStopped = false;
                agent.destination = target.position;
                animator.SetFloat("Speed", agent.velocity.magnitude);
            }
            else
            {
                if (!agent.isStopped)
                {
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero;
                    animator.SetFloat("Speed", 0f);

                    // Trigger callback if assigned
                    if (onTargetReachedCallback != null)
                    {
                        var callback = onTargetReachedCallback;
                        onTargetReachedCallback = null; // Clear callback only after invocation
                        callback.Invoke();
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (characters.ContainsKey(characterName))
        {
            characters.Remove(characterName);
        }
    }

    // Static Methods

    public static void SetTarget(string name, Transform newTarget, Action callback = null)
    {
        if (characters.TryGetValue(name, out AICharacter character))
        {
            character.target = newTarget;
            character.onTargetReachedCallback = callback;
        }
        else
        {
            Debug.LogWarning($"Character with name {name} not found.");
        }
    }

    public static void StartRunning(string name)
    {
        if (characters.TryGetValue(name, out AICharacter character))
        {
            character.agent.isStopped = false;
            character.animator.SetBool("IsRunning", true);
            character.agent.speed = 6f; // Set running speed
        }
        else
        {
            Debug.LogWarning($"Character with name {name} not found.");
        }
    }

    public static void StopMoving(string name)
    {
        if (characters.TryGetValue(name, out AICharacter character))
        {
            character.agent.isStopped = true;
            character.agent.velocity = Vector3.zero;
            character.animator.SetFloat("Speed", 0f);
        }
        else
        {
            Debug.LogWarning($"Character with name {name} not found.");
        }
    }

    public static void StartMoving(string name)
    {
        if (characters.TryGetValue(name, out AICharacter character))
        {
            character.agent.isStopped = false;
            character.animator.SetFloat("Speed", character.agent.velocity.magnitude);
        }
        else
        {
            Debug.LogWarning($"Character with name {name} not found.");
        }
    }

    public static void SetSpeed(string name, float speed)
    {
        if (characters.TryGetValue(name, out AICharacter character))
        {
            character.agent.speed = speed;
        }
        else
        {
            Debug.LogWarning($"Character with name {name} not found.");
        }
    }
}
