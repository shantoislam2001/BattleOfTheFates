using UnityEngine;
using System.Collections.Generic;
using TMPro;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;

public class ChasingBackend : MonoBehaviour
{
    public static ChasingBackend Self { get; private set; }
    public CountDownTimer timer;
    public static PriorityQueue<string> lostPlayer = new PriorityQueue<string>();
    public static Queue<string> winPlayer = new Queue<string>();
    public AIManager ai;
    public TextMeshProUGUI chasingEndText;
    public bool chasingEndTimerIsOn = false;
    public TextMeshProUGUI findingEndText;
    public bool findingEndTimerIsOn = false;

    [Header("Game time settings")]
    public float findingTime = 0;
    public float chasingTime = 0;
    public static bool playerPertisipeted = false;

    private GameObject ai1;
    private GameObject ai2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        //ai.SetTargetForAIWithCallback("AI", new Vector3(414.9537f, -0.0151712f, 323.8016f), () =>
        //{
        //    Debug.Log("target rechad ai");
        //});

        //ai.SetTargetForAIWithCallback("AI2", new Vector3(414.9537f, -0.0151712f, 323.8016f), () =>
        //{
        //    Debug.Log("target rechad ai2");
        //    ai.ClearTargetForAI("AI2");
        //});





        lostPlayer.Enqueue("AI",1);
        lostPlayer.Enqueue("AI2",1);
        stratGame();


    }


    private void Awake()
    {
        // Ensure there's only one instance
        if (Self == null)
        {
            Self = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void stratGame()
    {

        UIController.Self.feTimerActive();
        timer.StartTimer("t1", findingTime, () => {
            findingEndTimerIsOn = false;
            UIController.Self.feTimerInactive();

            for (int i = 0; i < lostPlayer.Count; i++)
            {
                string n = lostPlayer.Dequeue();
                setSlotForAI(n);
                lostPlayer.Enqueue(n, 1);
            }
            

            UIController.Self.ceTimerActive();
            timer.StartTimer("t2", chasingTime, () =>
            {
                if (!playerPertisipeted)
                {
                    UIController.Self.lostPopupActive("");
                    Invoke("backToMainMenu", 5f);

                }
                chasingEndTimerIsOn = false;
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
            chasingEndTimerIsOn = true;

        });
        findingEndTimerIsOn = true;
    }

    public void setSlotForAI(string name)
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
        if (winPlayer.Count == 0)
        {
            return false;

        }
        else
        {

            for (int i = 0; i < winPlayer.Count; i++)
            {
                string n = winPlayer.Dequeue();
                setSlotForAI(n);
                ai.ClearTargetForAI(n);
                lostPlayer.Enqueue(n, 1);
            }
            return true;

        }
    }

    void nextRoundPanelOff()
    {
        UIController.Self.nextRoundPanelInactive();
    }

    // Update is called once per frame
    void Update()
    {
        if(chasingEndTimerIsOn)
        {
            chasingEndText.text = timer.GetFormattedTime("t2");
        }

        if (findingEndTimerIsOn)
        {
            findingEndText.text = timer.GetFormattedTime("t1");
        }
    }

   

    public void backToMainMenu()
    {
        LoadingScreen.ShowLoadingScreen();
        SceneManager.LoadScene("Menu");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadingScreen.HideLoadingScreen();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
