using UnityEngine;
using UnityEngine.TextCore.Text;
using System.Collections;

public class AreaTrigger : MonoBehaviour
{
    public string slot;
    [Header("Gizmo Settings")]
    public Color gizmoColor = Color.green; // Color of the gizmo
    public Vector3 triggerSize = new Vector3(1, 1, 1); // Size of the trigger area

    [Header("Trigger Settings")]
    public bool isTrigger = true; // Should it act as a trigger or a collider

    [Header("Debug Messages")]
    public bool enableDebugMessages = true; // Toggle debug messages

    [Header("Events")]
    public UnityEngine.Events.UnityEvent onTriggerEnter; // Event when an object enters
    public UnityEngine.Events.UnityEvent onTriggerStay;  // Event when an object stays
    public UnityEngine.Events.UnityEvent onTriggerExit;  // Event when an object exits
    public string tag;
    

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

        if(other.CompareTag(tag))
        {
            onTriggerEnter.Invoke();
            GameObject.FindGameObjectWithTag(tag).GetComponent<Cards>().currentSlot = 
                gameObject.GetComponent<AreaTrigger>().slot;
        }
        else if (other.CompareTag("AI"))
        {
            
            string aiName = other.gameObject.name;
            GameObject character = GameObject.Find(aiName);
            string currentSlot = gameObject.GetComponent<AreaTrigger>().slot;
            Debug.Log("slot naem " + currentSlot);
            
            StartCoroutine(RotateAfterDelay(character, Slot.slotRotation[currentSlot].y, 2f));
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


        if (other.CompareTag(tag))
        {
            onTriggerExit.Invoke();
        }
        else if (other.CompareTag("AI"))
        {
            Debug.Log("called after delay1");
            string aiName = other.gameObject.name;
            GameObject character = GameObject.Find(aiName);
            string currentSlot = gameObject.GetComponent<AreaTrigger>().slot;
            Debug.Log("slot naem " + currentSlot);
            Debug.Log("called after delay2");
            StartCoroutine(RotateAfterDelay(character, Slot.slotRotation[currentSlot].y ,2f));
        }


    }

    IEnumerator RotateAfterDelay(GameObject character, float v,float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Execute the rotation
        character.transform.eulerAngles = new Vector3(0f, v, 0f);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position, triggerSize); // Draw the outer gizmo
        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 0.2f);
        Gizmos.DrawCube(transform.position, triggerSize); // Draw the semi-transparent filled gizmo
    }

  


  

}
