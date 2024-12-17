using TMPro;
using UnityEngine;

public class SlotB : MonoBehaviour
{
    public AIManager ai;
    public static string p1Name;
    public static string p2Name;
    public GameObject A1cards;
    public GameObject A2cards;
    private string p1card;
    private string p2card;
    private bool p1isAI;
    private bool p2isAI;
    private string winerCard;
    public GameObject p1Trigger;
    public CountDownTimer timer;
    public TextMeshProUGUI timerText;
    public bool timerUIisOn = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public void startGame()
    {
        GameObject.Find(p1Name).GetComponent<Cards>().throwedCard = "Empty";
        GameObject.Find(p2Name).GetComponent<Cards>().throwedCard = "Empty";
        p1isAI = GameObject.Find(p1Name).CompareTag("AI");
        p2isAI = GameObject.Find(p2Name).CompareTag("AI");

        if (p1isAI)
        {
            GameObject.Find(p1Name).GetComponent<Cards>().aiCardThrowing();
            p1card = GameObject.Find(p1Name).GetComponent<Cards>().throwedCard;
            if (p1card != "Empty")
            {
                Invoke("A1cardHideInTable", 9f);
            }


        }
        else
        {
            UIController.Self.cdTimerActive();
        }


        if (p2isAI)
        {
            GameObject.Find(p2Name).GetComponent<Cards>().aiCardThrowing();
            p2card = GameObject.Find(p2Name).GetComponent<Cards>().throwedCard;
            if (p1card != "Empty")
            {
                Invoke("A2cardHideInTable", 9f);
            }

        }
        else
        {
            UIController.Self.cdTimerActive();
        }


        timer.StartTimer("Chasing timerB", 15f, () =>
        {
            timerUIisOn = false;
            winer();
        });
        timerUIisOn = true;


    }

    void winer()
    {

        Invoke("uiInactive", 5f);
        if (p1isAI == false)
        {

            p1card = GameObject.Find(p1Name).GetComponent<Cards>().throwedCard;
            UIController.Self.cdTimerInactive();
        }

        if (p2isAI == false)
        {
            p2card = GameObject.Find(p2Name).GetComponent<Cards>().throwedCard;
            UIController.Self.cdTimerInactive();
        }

        if ((p1card != "Empty"))
        {
            A1cards.transform.Find("Hide").gameObject.SetActive(false);
            A1cards.transform.Find(p1card + " card").gameObject.SetActive(true);
        }
        else
        {
            if (p1isAI)
            {
                ChasingBackend.Self.goWaiting(p1Name);


                if (p2card != "Empty")
                {
                    if (p2isAI)
                    {
                        ChasingBackend.addWiner(p2Name);
                        ChasingBackend.Self.goWaiting(p2Name);

                    }
                    else
                    {
                        UIController.Self.winPopupActive(p1card);
                        p1Trigger.SetActive(true);
                        Invoke("cardInactive", 5f);
                        ChasingBackend.playerPertisipeted = true;

                    }
                }
                else
                {
                    if (p2isAI)
                    {
                        ChasingBackend.Self.goWaiting(p2Name);
                        p1Trigger.SetActive(true);

                        return;
                    }
                    else
                    {
                        UIController.Self.lostPopupActive(p1card);
                        p1Trigger.SetActive(true);
                        Invoke("mainMenu", 5f);
                        return;
                    }
                }

            }
            else
            {
                UIController.Self.lostPopupActive(p2card);
                Invoke("mainMenu", 5f);
            }
        }


        if ((p2card != "Empty"))
        {
            A2cards.transform.Find("Hide").gameObject.SetActive(false);
            A2cards.transform.Find(p2card + " card").gameObject.SetActive(true);
        }
        else
        {

            if (p2isAI)
            {
                ChasingBackend.Self.goWaiting(p2Name);



            }
            else
            {
                UIController.Self.lostPopupActive(p1card);
                Invoke("mainMenu", 5f);
            }

            if (p1isAI)
            {
                ChasingBackend.addWiner(p1Name);
                ChasingBackend.Self.goWaiting(p1Name);
            }
            else
            {
                UIController.Self.winPopupActive(p2card);
                ChasingBackend.playerPertisipeted = true;
            }
            p1Trigger.SetActive(true);
            Invoke("cardInactive", 5f);
        }
        Debug.Log(p1card + " and " + p2card);

        if (p1card != "Empty" && p2card != "Empty")
        {
            winerCard = ChasingBackend.getWiner(p1card, p2card);
        }

        Debug.Log("result " + winerCard);

        if (winerCard == p1card)
        {
            if (p1isAI)
            {
                ChasingBackend.Self.goWaiting(p1Name);
                Invoke("cardInactive", 6f);
                p1Trigger.SetActive(true);
                ChasingBackend.addWiner(p1Name);

                if (p2isAI)
                {
                    ChasingBackend.Self.goWaiting(p2Name);
                }
                else
                {
                    UIController.Self.lostPopupActive(p1card);
                    Invoke("mainMenu", 5f);
                }

            }
            else
            {
                Invoke("cardInactive", 6f);
                p1Trigger.SetActive(true);
                Debug.Log("you are win");
                UIController.Self.winPopupActive(p2card);
                ChasingBackend.playerPertisipeted = true;

                if (p2isAI)
                {
                    ChasingBackend.Self.goWaiting(p2Name);
                }

            }

        }
        Debug.Log("winer card " + winerCard + " p2 card " + p2card);
        if (winerCard == p2card)
        {

            if (p2isAI)
            {
                ChasingBackend.Self.goWaiting(p2Name);
                Invoke("cardInactive", 6f);
                p1Trigger.SetActive(true);
                ChasingBackend.addWiner(p2Name);

                if (p1isAI)
                {
                    ChasingBackend.Self.goWaiting(p1Name);
                }
                else
                {
                    UIController.Self.lostPopupActive(p2card);
                    Invoke("mainMenu", 5f);
                }

            }
            else
            {
                Invoke("cardInactive", 6f);
                p1Trigger.SetActive(true);
                Debug.Log("you are win");
                UIController.Self.winPopupActive(p1card);

                if (p1isAI)
                {
                    ChasingBackend.Self.goWaiting(p1Name);
                }

            }

        }

        if (winerCard == "Equal")
        {
            if (!p1isAI)
            {
                UIController.Self.tiePopupActive("same card");
                UIController.Self.cardThowPanelOpen();
            }

            if (!p2isAI)
            {
                UIController.Self.tiePopupActive("same card");
                UIController.Self.cardThowPanelOpen();
            }

            cardInactive();

            startGame();

        }

    }

    void A1cardHideInTable()
    {
        A1cards.transform.Find("Hide").gameObject.SetActive(true);
    }

    void A2cardHideInTable()
    {
        A2cards.transform.Find("Hide").gameObject.SetActive(true);
    }





    void cardInactive()
    {
        if (p1card != "Empty")
        {
            A1cards.transform.Find(p1card + " card").gameObject.SetActive(false);
        }
        if (p2card != "Empty")
        {
            A2cards.transform.Find(p2card + " card").gameObject.SetActive(false);
        }
        Slot.emptySlots.Enqueue("A1", 1);
        Slot.emptySlots.Enqueue("A2", 1);
    }

    void uiInactive()
    {
        UIController.Self.winPopupInactive();
        UIController.Self.lostPopupInactive();
        UIController.Self.tiePopupInactive();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerUIisOn)
        {
            timerText.text = timer.GetSecondsString("Chasing timerB");
        }
    }

    void mainMenu()
    {
        ChasingBackend.Self.backToMainMenu();
    }

}
