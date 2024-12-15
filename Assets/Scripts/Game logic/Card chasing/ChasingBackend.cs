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
      

       // lostPlayer.Enqueue("AI", 1);
        lostPlayer.Enqueue("AI2", 1);
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

        UIController.Self.feTimerActive();
        timer.StartTimer("t1", findingTime, () =>
        {
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
                chasingEndTimerIsOn = false;
                if (!playerPertisipeted)
                {
                    UIController.Self.lostPopupActive("");
                    Invoke("backToMainMenu", 5f);

                }
               
                UIController.Self.ceTimerInactive();
                deleteLostPlayer();

                if (nextRoundStartable())
                {
                    UIController.Self.nextRoundPanelActive();
                    Invoke("nextRoundPanelOff", 5f);
                    Debug.Log("Next round started");
                    waitingpoint = 0;
                    stratGame();
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
        Vector3 target = new Vector3(414.9537f, -0.0151712f, 323.8016f);
        if (Slot.emptySlots.Count > 0)
        {
            string slot = Slot.emptySlots.Dequeue();
            AICharacter.SetTarget(name, gate, () =>
            {
                Debug.Log(name);
                AICharacter.SetTarget(name, slotPoint(slot), () =>
                {
                    StartCoroutine(RotateAfterDelay(name, Slot.slotRotation[slot].y, slot, 2f));
                });
                updateSlotPayority(slot);
                playerSetInSlot(slot, name);
                slotControlForPlayer(slot);

            });
            AICharacter.SetSpeed(name, 2f);
        }
        else
        {
            // AICharacter.SetTarget(name, new Vector3(UnityEngine.Random.Range(420f, 425f), -0.02000013f, UnityEngine.Random.Range(320f, 325f)));
            Slot.waitingAI.Enqueue(name);
        }




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
        Debug.Log(lostPlayer.Count);
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
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadingScreen.HideLoadingScreen();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void updateSlotPayority(string slot)
    {
        if (slot == "A1")
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
        else if (slot == "B1")
        {
            SlotB.p1Name = player;
        }
        else if (slot == "B2")
        {
            SlotB.p2Name = player;
            slotB.startGame();
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
        if(s == "A1")
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