using UnityEngine;
using System.Collections.Generic;

public class Slot : MonoBehaviour
{
    public static PriorityQueue<string> emptySlots = new PriorityQueue<string>();
    public static Dictionary<string, bool> slotStatus = new Dictionary<string, bool>();
    public static Dictionary<string, Vector3> slotPosition = new Dictionary<string, Vector3>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slotPosition.Add("A1", new Vector3(413.2227f, -0.02000013f, 322.3752f));
        slotPosition.Add("A2", new Vector3(413.5374f, -0.02000013f, 324.3748f));
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

        slotStatus.Add("A1", true);
        slotStatus.Add("A2", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
