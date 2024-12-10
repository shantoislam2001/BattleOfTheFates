using UnityEngine;
using System.Collections.Generic;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class ChasingBackend : MonoBehaviour
{
    public CountDownTimer timer;
    public static PriorityQueue<string> lostPlayer = new PriorityQueue<string>();
    public static Queue<string> winPlayer = new Queue<string>();
    public AIManager ai;
    public TextMeshProUGUI chasingEndText;
    public GameObject chasingEndTimer;
    public TextMeshProUGUI findingEndText;
    public GameObject findingEndTimer;

    [Header("Game time settings")]
    public float findingTime = 0;
    public float chasingTime = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        stratGame();


       
        lostPlayer.Enqueue("AI",1);
        lostPlayer.Enqueue("AI2",1);
        
       
        
    }

    void stratGame()
    {

        UIController.Self.feTimerActive();
        timer.StartTimer("t1", findingTime, () => {

            UIController.Self.feTimerInactive();

            setSlotForAI("AI", "A1");
            setSlotForAI("AI2", "A1");

            UIController.Self.ceTimerActive();
            timer.StartTimer("t2", chasingTime, () =>
            {
                UIController.Self.ceTimerInactive();
                deleteLostPlayer();
                
                if (nextRoundStartable())
                {
                    UIController.Self.nextRoundPanelActive();
                    Invoke("nextRoundPanelOff", 5f);
                    Debug.Log("Next round started");
                    stratGame();
                }
                else
                {
                    Debug.Log("you win fainly");
                }
            });

        });
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
   
    // control winer 
    public static void addWiner(string n)
    {
        winPlayer.Enqueue(n);
        lostPlayer.Delete(n);
        Debug.Log(lostPlayer.Count);
    }

    public static void deleteLostPlayer()
    {
        if(lostPlayer.Count > 0)
        {
            for (int i = 0; lostPlayer.Count > 0; i++)
            {
                Destroy(GameObject.Find(lostPlayer.Dequeue()));
            }
            
        }
       
      
    }


    public bool nextRoundStartable()
    {
        if(winPlayer.Count == 0)
        {
            return false;
        }

        for (int i =0; i<winPlayer.Count; i++)
        {
            string n = winPlayer.Dequeue();
            setSlotForAI(n, "A1");
            ai.ClearTargetForAI(n);
            lostPlayer.Enqueue(n, 1);
        }
        return true;    
    }

    void nextRoundPanelOff()
    {
        UIController.Self.nextRoundPanelInactive();
    }

    // Update is called once per frame
    void Update()
    {
        if(chasingEndTimer.activeSelf)
        {
            chasingEndText.text = timer.GetFormattedTime("t2");
        }

        if (findingEndTimer.activeSelf)
        {
            findingEndText.text = timer.GetFormattedTime("t1");
        }


    }
}
