using UnityEngine;
using System.Collections.Generic;
using TMPro;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;
using ReadyPlayerMe.Core;
using System.Collections;
using System;
using UnityEngine.TextCore.Text;

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
    private bool aiRoam = true;
    public CardSpawner spawner;

    [Header("Game time settings")]
    public float findingTime = 0;
    public float chasingTime = 0;
    public static bool playerPertisipeted = false;
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
    public Transform gate;

    public Transform waitingP1;
    public Transform waitingP2;
    public Transform waitingP3;
    public Transform waitingP4;
    public Transform waitingP5;
    public Transform waitingP6;
    public Transform waitingP7;
    public Transform waitingP8;
    private int waitingpoint = 0;

    private GameObject ai1;
    private GameObject ai2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
       
        lostPlayer.Enqueue("AI2", 1);
        lostPlayer.Enqueue("AI1", 1);
        lostPlayer.Enqueue("AI3", 1);
        lostPlayer.Enqueue("AI4", 1);
        lostPlayer.Enqueue("AI5", 1);
        lostPlayer.Enqueue("AI6", 1);
        lostPlayer.Enqueue("AI7", 1);
        stratGame();
        startRoam();

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
        Slot.updateAllValue();
        UIController.Self.feTimerActive();
        timer.StartTimer("t1", findingTime, () =>
        {
            findingEndTimerIsOn = false;
            UIController.Self.feTimerInactive();
            Debug.Log("Lost player " + lostPlayer.Count);
            Debug.Log("win player " + winPlayer.Count);
            for (int i = 0; i < lostPlayer.Count; i++)
            {
                string n = lostPlayer.Dequeue();
                setSlotForAI(n);
                lostPlayer.Enqueue(n, 1);
            }


            UIController.Self.ceTimerActive();
            timer.StartTimer("t2", chasingTime, () =>
            {
                chasingEndTimerIsOn = false;
                if (!playerPertisipeted)
                {
                    UIController.Self.lostPopupActive("");
                    Invoke("backToMainMenu", 5f);
                    return;
                }

                UIController.Self.ceTimerInactive();
                deleteLostPlayer();

                if (nextRoundStartable())
                {
                    UIController.Self.nextRoundPanelActive();
                    Invoke("nextRoundPanelOff", 5f);
                    playerPertisipeted = false;
                    waitingpoint = 0;
                    stratGame();
                    spawner.SpawnObjects();
                    startRoam();

                }
                else
                {
                    UIController.Self.finallyWinActive();
                    Invoke("backToMainMenu", 5f);
                }
            });
            chasingEndTimerIsOn = true;

        });
        findingEndTimerIsOn = true;
    }

    public void setSlotForAI(string name)
    {
        AICharacter.SetTarget(name, gate);
        AICharacter.SetSpeed(name, 2f);
        GameObject.Find(name).GetComponent<Cards>().played = false;
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

        if (p1 == "King" && (p2 == "Prince" || p2 == "Stepmother" || p2 == "Witch"))
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
        
    }

    public static void deleteLostPlayer()
    {
        if (lostPlayer.Count > 0)
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

            for (int i = winPlayer.Count; i > 0; i--)
            {
                lostPlayer.Enqueue(winPlayer.Dequeue(), 1);
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
        if (chasingEndTimerIsOn)
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
        findingEndTimerIsOn = false;
        chasingEndTimerIsOn = false;
        LoadingScreen.ShowLoadingScreen();
        SceneManager.LoadScene("Menu");
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("player perticipated " + playerPertisipeted);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadingScreen.HideLoadingScreen();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

  

   

  

  

   


    public void goWaiting(string n)
    {
        waitingpoint++;
        if (waitingpoint == 1)
        {
            AICharacter.SetTarget(n, waitingP1);
        }
        else if (waitingpoint == 2)
        {
            AICharacter.SetTarget(n, waitingP2);
        }
        else if (waitingpoint == 3)
        {
            AICharacter.SetTarget(n, waitingP3);
        }
        else if (waitingpoint == 4)
        {
            AICharacter.SetTarget(n, waitingP4);
        }
        else if (waitingpoint == 5)
        {
            AICharacter.SetTarget(n, waitingP5);
        }
        else if (waitingpoint == 6)
        {
            AICharacter.SetTarget(n, waitingP6);
        }
        else if (waitingpoint == 7)
        {
            AICharacter.SetTarget(n, waitingP7);
        }
        else if (waitingpoint == 8)
        {
            AICharacter.SetTarget(n, waitingP8);
        }




    }

    void roamAI(string n)
    {
        if(CardSpawner.cardName.Count > 0 && aiRoam)
        {
            Cards cards = GameObject.Find(n).GetComponent<Cards>();
            string cardName = CardSpawner.cardName.Dequeue();

            AICharacter.SetTarget(n, GameObject.Find(cardName).transform, () =>
            {
                GameObject ob = GameObject.Find(cardName);
                if (ob != null)
                {
                    keepCard(GameObject.Find(n).GetComponent<Cards>(),cardName);
                    spawner.DestroyObjectByName(cardName);
                }
                roamAI(n);
            });
            AICharacter.SetSpeed(n, 2f);
        }
    }

    Transform getRoamTarget()
    {
        return GameObject.Find(CardSpawner.cardName.Dequeue()).transform;
    }

    void startRoam()
    {
        for (int i = 0; i < lostPlayer.Count; i++)
        {
            string n = lostPlayer.Dequeue();
            roamAI(n);
            lostPlayer.Enqueue(n, 1);
        }
    }

    void keepCard(Cards card, string c)
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

 

}