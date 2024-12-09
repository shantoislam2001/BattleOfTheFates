using TMPro;
using UnityEngine;

public class SlotA : MonoBehaviour
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
    public GameObject timerUI;
    

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

        if(p1isAI)
        {
            GameObject.Find(p1Name).GetComponent<Cards>().aiCardThrowing();
            p1card =  GameObject.Find(p1Name).GetComponent<Cards>().throwedCard;
            if(p1card != "Empty")
            {
                Invoke("A1cardHideInTable", 9f);
            }
           
            
        }else
        {
            UIController.Self.cdTimerActive();
        }
     

        if (p2isAI)
        {
            GameObject.Find(p2Name).GetComponent<Cards>().aiCardThrowing();
            p2card = GameObject.Find(p2Name).GetComponent<Cards>().throwedCard;
            if(p1card != "Empty")
            {
                Invoke("A2cardHideInTable", 9f);
            }
           
        }else
        {
            UIController.Self.cdTimerActive();
        }
      

        timer.StartTimer("Chasing timer", 15f, winer);
      //  UIController.Self.cdTimerActive();

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
            p2card =  GameObject.Find(p2Name).GetComponent<Cards>().throwedCard;
            UIController.Self.cdTimerInactive();
        }

        if ((p1card != "Empty"))
        {
            A1cards.transform.Find("Hide").gameObject.SetActive(false);
            A1cards.transform.Find(p1card + " card").gameObject.SetActive(true);
        } else
        {
            if (p1isAI)
            {
                ai.SetTargetForAI(p1Name, new Vector3(Random.Range(420f, 425f), -0.02000013f, Random.Range(320f, 325f)));
                GameObject.Find(p1Name).tag = "Untagged";
                Invoke("setTagP1", 30f);

                if (p2card != "Empty") {
                    if (p2isAI)
                    {
                        ChasingBackend.addWiner(p2Name);
                        ai.SetTargetForAI(p2Name, new Vector3(Random.Range(420f, 425f), -0.02000013f, Random.Range(320f, 325f)));
                        GameObject.Find(p2Name).tag = "Untagged";
                        Invoke("setTagP2", 30f);
                    }
                    else
                    {
                        UIController.Self.winPopupActive(p1card);
                        p1Trigger.SetActive(true);
                        Invoke("cardInactive", 5f);
                       
                    }
                } else
                {
                    if (p2isAI)
                    {
                        ai.SetTargetForAI(p2Name, new Vector3(Random.Range(420f, 425f), -0.02000013f, Random.Range(320f, 325f)));
                        GameObject.Find(p2Name).tag = "Untagged";
                        Invoke("setTagP2", 30f);
                        p1Trigger.SetActive(true);

                        return;
                    }
                    else
                    {
                        UIController.Self.lostPopupActive(p1card);
                        p1Trigger.SetActive(true);
                        return;
                    }
                }
              
            }
            else
            {
                UIController.Self.lostPopupActive(p2card);
               
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
                ai.SetTargetForAI(p2Name, new Vector3(Random.Range(420f, 425f), -0.02000013f, Random.Range(320f, 325f)));
                GameObject.Find(p2Name).tag = "Untagged";
                Invoke("setTagP2", 30f);

               
              
            }
            else
            {
                UIController.Self.lostPopupActive(p1card);

            }

            if (p1isAI)
            {
                ChasingBackend.addWiner(p1Name);
                ai.SetTargetForAI(p1Name, new Vector3(Random.Range(420f, 425f), -0.02000013f, Random.Range(320f, 325f)));
                GameObject.Find(p1Name).tag = "Untagged";
                Invoke("setTagP1", 30f);
            }else
            {
                UIController.Self.winPopupActive(p2card);
            }
            p1Trigger.SetActive(true);
            Invoke("cardInactive", 5f);
        }


        if(p1card != "Empty" && p2card != "Empty")
        {
            winerCard = ChasingBackend.getWiner(p1card, p2card);
        }
           
        Debug.Log(winerCard);

        if (winerCard == p1card)
        {
            if(p1isAI)
            {
                ai.SetTargetForAI(p1Name, new Vector3(Random.Range(420f, 425f), -0.02000013f, Random.Range(320f, 325f)));
                GameObject.Find(p1Name).tag = "Untagged";
                Invoke("setTagP1", 30f);
                Invoke("cardInactive", 6f);
                p1Trigger.SetActive(true);
                ChasingBackend.addWiner(p1Name);

                if (p2isAI)
                {
                    ai.SetTargetForAI(p2Name, new Vector3(Random.Range(420f, 425f), -0.02000013f, Random.Range(320f, 325f)));
                    GameObject.Find(p2Name).tag = "Untagged";
                    Invoke("setTagP2", 30f);
                }else
                {
                    UIController.Self.lostPopupActive(p1card);
                }

            } else
            {
                Invoke("cardInactive", 6f);
                p1Trigger.SetActive(true);
                Debug.Log("you are win");
                UIController.Self.winPopupActive(p2card);

                if (p2isAI)
                {
                    ai.SetTargetForAI(p2Name, new Vector3(Random.Range(420f, 425f), -0.02000013f, Random.Range(320f, 325f)));
                    GameObject.Find(p2Name).tag = "Untagged";
                    Invoke("setTagP2", 30f);
                }

            }

        }
        Debug.Log("winer card " + winerCard + " p2 card " + p2card);
        if (winerCard == p2card)
        {
            
            if (p2isAI)
            {
                ai.SetTargetForAI(p2Name, new Vector3(Random.Range(420f, 425f), -0.02000013f, Random.Range(320f, 325f)));
                GameObject.Find(p2Name).tag = "Untagged";
                Invoke("setTagP2", 30f);
                Invoke("cardInactive", 6f);
                p1Trigger.SetActive(true);
                ChasingBackend.addWiner(p2Name);

                if (p1isAI)
                {
                    ai.SetTargetForAI(p1Name, new Vector3(Random.Range(420f, 425f), -0.02000013f, Random.Range(320f, 325f)));
                    GameObject.Find(p1Name).tag = "Untagged";
                    Invoke("setTagP1", 30f);
                } else
                {
                    UIController.Self.lostPopupActive(p2card);
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
                    ai.SetTargetForAI(p1Name, new Vector3(Random.Range(420f, 425f), -0.02000013f, Random.Range(320f, 325f)));
                    GameObject.Find(p1Name).tag = "Untagged";
                    Invoke("setTagP1", 30f);
                }

            }

        }

        if (winerCard == "Equal")
        {
            if(!p1isAI)
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

    void setTagP1()
    {
        GameObject.Find(p1Name).tag = "AI";
    }

    void setTagP2()
    {
        GameObject.Find(p2Name).tag = "AI";
    }

    void cardInactive()
    {
        if(p1card != "Empty")
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
        if(timerUI.activeSelf)
        {
            timerText.text = timer.GetSecondsString("Chasing timer");
        }
    }
}
