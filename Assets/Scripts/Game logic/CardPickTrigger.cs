using UnityEngine;
using UnityEngine.TextCore.Text;
using System.Collections;

public class CardPickTrigger : MonoBehaviour
{
   
    [Header("Gizmo Settings")]
    public Color gizmoColor = Color.green; // Color of the gizmo
    public Vector3 triggerSize = new Vector3(1, 1, 1); // Size of the trigger area

    [Header("Trigger Settings")]
    public bool isTrigger = true; // Should it act as a trigger or a collider

    [Header("Debug Messages")]
    public bool enableDebugMessages = true; // Toggle debug messages

    public void Start()
    {
        
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



    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position, triggerSize); // Draw the outer gizmo
        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 0.2f);
        Gizmos.DrawCube(transform.position, triggerSize); // Draw the semi-transparent filled gizmo
    }








}
