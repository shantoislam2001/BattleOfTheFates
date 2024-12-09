using UnityEngine;
using UnityEngine.TextCore.Text;
using System.Collections;

public class CardPickTrigger : MonoBehaviour
{
    public CardSpawner spawner;
    public AIManager ai;

    [Header("Gizmo Settings")]
    public Color gizmoColor = Color.green; // Color of the gizmo
    public Vector3 triggerSize = new Vector3(1, 1, 1); // Size of the trigger area

    [Header("Trigger Settings")]
    public bool isTrigger = true; // Should it act as a trigger or a collider

    [Header("Debug Messages")]
    public bool enableDebugMessages = true; // Toggle debug messages

    public void Start()
    {
        ai = GameObject.Find("Scripts").GetComponent<AIManager>();
        spawner = GameObject.Find("Scripts").transform.Find("Card spawner").GetComponent<CardSpawner>();
    }



  

    private void OnTriggerEnter(Collider other)
    {
        if (!isTrigger) return;

        if (enableDebugMessages)
            Debug.Log($"{other.name} entered the trigger zone.");

        if (other.CompareTag("Player"))
        {
            UIController.Self.pickButtonActive();
        }

        if (other.CompareTag("AI"))
        {
            Cards cards = other.GetComponent<Cards>();
            keepCard(cards, cards.triggeredCard);
            spawner.DestroyObjectByName(cards.triggeredCard);
            ai.SetTargetForAI(other.gameObject.name, new Vector3(414.9537f, -0.0151712f, 323.8016f));
            ai.ClearTargetForAI(other.gameObject.name);
            Debug.Log("triger ai " + other.gameObject.name);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (!isTrigger) return;

        if (enableDebugMessages)
            Debug.Log($"{other.name} is staying in the trigger zone.");

        // onTriggerStay.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isTrigger) return;

        if (enableDebugMessages)
            Debug.Log($"{other.name} exited the trigger zone.");


        if (other.CompareTag("Player"))
        {
            UIController.Self.pickButtonInactive();
        }


      

    }

    void keepCard(Cards card,string c)
    {
        if (c.Contains("Prince"))
        {
            card.prince += 1;
        }
        else if (c.Contains("Stepmother"))
        {
            card.stepmother += 1;
        }
        else if (c.Contains("Witch"))
        {
            card.witch += 1;
        }
        else if (c.Contains("Fate"))
        {
            card.fate += 1;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position, triggerSize); // Draw the outer gizmo
        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 0.2f);
        Gizmos.DrawCube(transform.position, triggerSize); // Draw the semi-transparent filled gizmo
    }








}
