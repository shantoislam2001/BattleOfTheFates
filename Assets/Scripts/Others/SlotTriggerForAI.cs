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
    public SlotB slotB;
    public SlotC slotC;
    public SlotD slotD;

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

        if (other.CompareTag("AI") && !other.GetComponent<Cards>().played )
        {
            other.GetComponent<Cards>().played = true;
            string n = other.gameObject.name;
            GameObject character = GameObject.Find(n);
            if ((Slot.emptySlots.Count > 0))
            {
                string slot = Slot.emptySlots.Dequeue();
                Debug.Log("slot selected "+slot);
                AICharacter.SetTarget(n, slotPoint(slot), () => {

                    StartCoroutine(RotateAfterDelay(n, Slot.slotRotation[slot].y, slot, 1f));
                });
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
            Debug.Log("game start a");
        }
        else if (slot == "B1")
        {
            SlotB.p1Name = player;
        }
        else if (slot == "B2")
        {
            SlotB.p2Name = player;
            slotB.startGame();
            Debug.Log("game start b");
        }
        else if (slot == "C1")
        {
            SlotC.p1Name = player;
        }
        else if (slot == "C2")
        {
            SlotC.p2Name = player;
            slotC.startGame();
        }
        else if (slot == "D1")
        {
            SlotD.p1Name = player;
        }
        else if (slot == "D2")
        {
            SlotD.p2Name = player;
            slotD.startGame();
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

    Transform slotPoint(string s)
    {
        if (s == "A1")
        {
            return a1.transform;
        }
        else
        if (s == "A2")
        {
            return a2.transform;
        }
        else
        if (s == "B1")
        {
            return b1.transform;
        }
        else
        if (s == "B2")
        {
            return b2.transform;
        }
        else
        if (s == "C1")
        {
            return c1.transform;
        }
        else
        if (s == "C2")
        {
            return c2.transform;
        }
        else
        if (s == "D1")
        {
            return d1.transform;
        }
        else
        if (s == "D2")
        {
            return d2.transform;
        }
        return null;
    }

    IEnumerator RotateAfterDelay(string n, float v, string slot, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Execute the rotation
        GameObject.Find(n).transform.eulerAngles = new Vector3(0f, v, 0f);



    }

}
