using UnityEngine;

public class ChasingBackend : MonoBehaviour
{
    public AIManager ai;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        setSlotForAI("AI", "A1");
        setSlotForAI("AI2", "A1");
    }

    public void setSlotForAI(string name, string slot)
    {
        Vector3 target = new Vector3(414.9537f, -0.0151712f, 323.8016f);
        ai.SetTargetForAI(name, target);
      
    }

    public static string getWiner(string p1, string p2)
    {
        if (p1 == p2)
        {
            
            return "Equal";
        }

        
        if (p1 == "Fate" || p2 == "Fate")
        {

            return "Fate";
        }

        
        if (p1 == "Rebel" && p2 == "King")
        {

            return "Rebel";
        }
        if (p2 == "Rebel" && p1 == "King")
        {

            return "Rebel";
        }

        if (p1 == "Rebel" && (p2 == "Prince" || p2 == "Stepmother" || p2 == "Witch"))
        {

            return p2;
        }

        if (p2 == "Rebel" && (p1 == "Prince" || p1 == "Stepmother" || p1 == "Witch"))
        {

            return p1;
        }

        if (p1 == "King" && (p2 =="Prince" || p2 == "Stepmother" || p2 == "Witch"))
        {

            return "King";
        }
        if (p2 == "King" && (p1 == "Prince" || p1 == "Stepmother" || p1 == "Witch"))
        {

            return "King";
        }

        
        if ((p1 == "Prince" && p2 == "Witch") ||
            (p1 == "Stepmother" && p2 == "Prince") ||
            (p1 == "Witch" && p2 == "Stepmother"))
        {
          
            return p2;
        }
        if ((p2 == "Prince" && p1 == "Witch") ||
            (p2 == "Stepmother" && p1 == "Prince") ||
            (p2 == "Witch" && p1 == "Stepmother"))
        {
           
            return p1;
        }


        return p1;
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
