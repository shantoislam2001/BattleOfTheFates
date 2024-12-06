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
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void startGame()
    {
         p1isAI = GameObject.Find(p1Name).CompareTag("AI");
         p2isAI = GameObject.Find(p2Name).CompareTag("AI");

        if(p1isAI)
        {
            GameObject.Find(p1Name).GetComponent<Cards>().aiCardThrowing();
            p1card = GameObject.Find(p1Name).GetComponent<Cards>().throwedCard;
            Invoke("A1cardHideInTable", 5f);
            Debug.Log("player 1 card "+p1card);
        }

        if (p2isAI)
        {
            GameObject.Find(p2Name).GetComponent<Cards>().aiCardThrowing();
            p2card = GameObject.Find(p2Name).GetComponent<Cards>().throwedCard;
            Invoke("A2cardHideInTable", 5f);
        }

        Invoke("winer",10f);

    }

    void winer()
    {
        if (p1isAI == false)
        {
            p1card = GameObject.Find(p1Name).GetComponent<Cards>().throwedCard;
        }

        if (p2isAI == false)
        {
            p2card = GameObject.Find(p2Name).GetComponent<Cards>().throwedCard;
        }
        A1cards.transform.Find("Hide").gameObject.SetActive(false);
        A1cards.transform.Find(p1card+" card").gameObject.SetActive(true);
        A2cards.transform.Find("Hide").gameObject.SetActive(false);
        A2cards.transform.Find(p2card+" card").gameObject.SetActive(true);
        winerCard = ChasingBackend.getWiner(p1card, p2card);
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

                if (p2isAI)
                {
                    ai.SetTargetForAI(p2Name, new Vector3(Random.Range(420f, 425f), -0.02000013f, Random.Range(320f, 325f)));
                    GameObject.Find(p2Name).tag = "Untagged";
                    Invoke("setTagP2", 30f);
                }

            } else
            {
                Invoke("cardInactive", 6f);
                p1Trigger.SetActive(true);
                Debug.Log("you are win");

                if (p2isAI)
                {
                    ai.SetTargetForAI(p2Name, new Vector3(Random.Range(420f, 425f), -0.02000013f, Random.Range(320f, 325f)));
                    GameObject.Find(p2Name).tag = "Untagged";
                    Invoke("setTagP2", 30f);
                }

            }

        }

        if (winerCard == p2card)
        {
            if (p2isAI)
            {
                ai.SetTargetForAI(p2Name, new Vector3(Random.Range(420f, 425f), -0.02000013f, Random.Range(320f, 325f)));
                GameObject.Find(p2Name).tag = "Untagged";
                Invoke("setTagP2", 30f);
                Invoke("cardInactive", 6f);
                p1Trigger.SetActive(true);

                if (p1isAI)
                {
                    ai.SetTargetForAI(p1Name, new Vector3(Random.Range(420f, 425f), -0.02000013f, Random.Range(320f, 325f)));
                    GameObject.Find(p1Name).tag = "Untagged";
                    Invoke("setTagP1", 30f);
                }

            }
            else
            {
                Invoke("cardInactive", 6f);
                p1Trigger.SetActive(true);
                Debug.Log("you are win");

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

            cardInactive();
            UIController.Self.cardThowPanelOpen();
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
        A1cards.transform.Find(p1card + " card").gameObject.SetActive(false);
        A2cards.transform.Find(p2card + " card").gameObject.SetActive(false );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
