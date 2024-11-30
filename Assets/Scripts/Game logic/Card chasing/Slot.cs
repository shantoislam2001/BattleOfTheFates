using UnityEngine;
using System.Collections.Generic;

public class Slot : MonoBehaviour
{
    public static PriorityQueue<string> emptySlots = new PriorityQueue<string>();
    public static Dictionary<string, string> slotCurrentPlayer = new Dictionary<string, string>();
    public static Dictionary<string, Vector3> slotPosition = new Dictionary<string, Vector3>();
    public static Dictionary<string, Quaternion> slotRotation = new Dictionary<string, Quaternion>();
    public static Queue<string> waitingAI = new Queue<string>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slotPosition.Add("A1", new Vector3(414.4883f, -0.0151712f, 319.9008f));
        slotRotation.Add("A1", new Quaternion(0f, -90f, 0f,0f));
        slotPosition.Add("A2", new Vector3(412.5337f, -0.0151712f, 319.8827f));
        slotRotation.Add("A2", new Quaternion(0f, 90f, 0f, 0f));
        slotPosition.Add("B1", new Vector3(412.4951f, -0.0151712f, 321.3918f));
        slotRotation.Add("B1", new Quaternion(0f, 90f, 0f, 0f));
        slotPosition.Add("B2", new Vector3(414.4955f, -0.0151712f, 321.236f));
        slotRotation.Add("B2", new Quaternion(0f, -90f, 0f, 0f));
        slotPosition.Add("C1", new Vector3(414.5686f, -0.0151712f, 325.5324f));
        slotRotation.Add("C1", new Quaternion(0f, -90f, 0f, 0f));
        slotPosition.Add("C2", new Vector3(412.5133f, -0.0151712f, 325.4667f));
        slotRotation.Add("C2", new Quaternion(0f, 90f, 0f, 0f));
        slotPosition.Add("D1", new Vector3(412.5121f, -0.0151712f, 326.8651f));
        slotRotation.Add("D1", new Quaternion(0f, 90f, 0f, 0f));
        slotPosition.Add("D2", new Vector3(414.5806f, -0.0151712f, 326.8845f));
        slotRotation.Add("D2", new Quaternion(0f, -90f, 0f, 0f));
        updateAllValue();
        
    }

    static void clearQ()
    {
        if(emptySlots.Count > 0) {

            for(int i = 0; i < emptySlots.Count; i++)
            {
               string s = emptySlots.Dequeue();
            }

        }
    

    }

    public static void updateAllValue()
    {
        clearQ();
        emptySlots.Enqueue("A1", 1);
        emptySlots.Enqueue("A2", 1);
        emptySlots.Enqueue("B1", 1);
        emptySlots.Enqueue("B2", 1);
        emptySlots.Enqueue("C1", 1);
        emptySlots.Enqueue("C2", 1);
        emptySlots.Enqueue("D1", 1);
        emptySlots.Enqueue("D2", 1);

        slotCurrentPlayer.Add("A1", "Empty");
        slotCurrentPlayer.Add("A2", "Empty");
        slotCurrentPlayer.Add("B1", "Empty");
        slotCurrentPlayer.Add("B2", "Empty");
        slotCurrentPlayer.Add("C1", "Empty");
        slotCurrentPlayer.Add("C2", "Empty");
        slotCurrentPlayer.Add("D1", "Empty");
        slotCurrentPlayer.Add("D2", "Empty");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
