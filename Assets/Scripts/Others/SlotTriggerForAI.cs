using UnityEngine;
using System;

using System.Collections;

public class SlotTriggerForAI : MonoBehaviour
{
    
    [Header("Gizmo Settings")]
    public Color gizmoColor = Color.green; // Color of the gizmo
    public Vector3 triggerSize = new Vector3(1, 1, 1); // Size of the trigger area

    [Header("Trigger Settings")]
    public bool isTrigger = true; // Should it act as a trigger or a collider

    [Header("Debug Messages")]
    public bool enableDebugMessages = true; // Toggle debug messages
    public AIManager ai;

    [Header("Events")]
    public UnityEngine.Events.UnityEvent onTriggerEnter; // Event when an object enters
    public UnityEngine.Events.UnityEvent onTriggerStay;  // Event when an object stays
    public UnityEngine.Events.UnityEvent onTriggerExit;  // Event when an object exits
    public SlotA slotA;

    public GameObject a1;
    public GameObject a2;
    public GameObject b1;
    public GameObject b2;
    public GameObject c1;
    public GameObject c2;
    public GameObject d1;
    public GameObject d2;

    private void Reset()
    {
        // Automatically adjust collider settings on adding the script
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider>();
        }
        boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTrigger) return;

        if (enableDebugMessages)
            Debug.Log($"{other.name} entered the trigger zone.");

        if (other.CompareTag("AI"))
        {
            string n = other.gameObject.name;
            GameObject character = GameObject.Find(n);
            if ((Slot.emptySlots.Count > 0))
            {
                string slot = Slot.emptySlots.Dequeue();
                ai.SetTargetForAI(n, Slot.slotPosition[slot]);
                updateSlotPayority(slot);
                playerSetInSlot(slot, n);
                Debug.Log("selecterd slot "+ slot);
                slotControlForPlayer(slot);



            }
            else
            {
                ai.SetTargetForAI(n, new Vector3(UnityEngine.Random.Range(420f,425f), -0.02000013f, UnityEngine.Random.Range(320f, 325f)));
                Slot.waitingAI.Enqueue(n);

            }
           
        }


    }
    // Slot setup for player
    public void enterForPlayer()
    {
        string currentSlot = GameObject.FindGameObjectWithTag("Player").GetComponent<Cards>().currentSlot;
        string playerName = GameObject.FindGameObjectWithTag("Player").gameObject.name;
        updateSlotPayority(currentSlot);
        playerSetInSlot(currentSlot, playerName);
        Slot.emptySlots.Delete(currentSlot);
        UIController.Self.enterButtonInactive();
        UIController.Self.cardThowPanelOpen();
        slotControlForPlayer(currentSlot);

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


        if (other.CompareTag(tag))
        {
            onTriggerExit.Invoke();
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position, triggerSize); // Draw the outer gizmo
        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 0.2f);
        Gizmos.DrawCube(transform.position, triggerSize); // Draw the semi-transparent filled gizmo
    }


    public void updateSlotPayority(string slot)
    {
        if(slot == "A1")
        {
            Slot.emptySlots.UpdatePriority("A2", 0);
            
        }
        else if (slot == "B1")
        {
            Slot.emptySlots.UpdatePriority("B2", 0);
        }
        else if (slot == "C1")
        {
            Slot.emptySlots.UpdatePriority("C2", 0);
        }
        else if (slot == "D1")
        {
            Slot.emptySlots.UpdatePriority("D2", 0);
        }
    }

    public void playerSetInSlot(string slot, string player)
    {
        if (slot == "A1")
        {
            SlotA.p1Name = player;
        }
        else if (slot == "A2")
        {
            SlotA.p2Name = player;
            slotA.startGame();
        }
    }


    void slotControlForPlayer(string slot)
    {
        if (slot == "A1")
        {
            StartCoroutine(ExecuteAfterDelay(() => a1.SetActive(false), 5f));
            a2.SetActive(true);
        }
        else if (slot == "A2")
        {
            a2.SetActive(false);
        }
        else if (slot == "B1")
        {
            StartCoroutine(ExecuteAfterDelay(() => b1.SetActive(false), 5f));
            b2.SetActive(true);
        }
        else if (slot == "B2")
        {

            b2.SetActive(false);
        }
        else if (slot == "C1")
        {
            StartCoroutine(ExecuteAfterDelay(() => c1.SetActive(false), 5f));
            c2.SetActive(true);
        }
        else if (slot == "C2")
        {

            c2.SetActive(false);
        }
        else if (slot == "D1")
        {
            StartCoroutine(ExecuteAfterDelay(() => d1.SetActive(false), 5f));
            d2.SetActive(true);
        }
        else if (slot == "D2")
        {

            d2.SetActive(false);
        }
    }

    IEnumerator ExecuteAfterDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

}
