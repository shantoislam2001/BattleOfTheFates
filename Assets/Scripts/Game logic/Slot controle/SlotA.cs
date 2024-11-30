using UnityEngine;

public class SlotA : MonoBehaviour
{
    public static string p1Name;
    public static string p2Name;
    public GameObject A1cards;
    public GameObject A2cards;
    private string p1card;
    private string p2card;
    private bool p1isAI;
    private bool p2isAI;
    private string winerCard;

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
        }

        if (p2isAI)
        {
            GameObject.Find(p2Name).GetComponent<Cards>().aiCardThrowing();
            p2card = GameObject.Find(p2Name).GetComponent<Cards>().throwedCard;
            Invoke("A2cardHideInTable", 5f);
        }



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
        A1cards.transform.Find(p1card).gameObject.SetActive(true);
        A2cards.transform.Find("Hide").gameObject.SetActive(false);
        A2cards.transform.Find(p2card).gameObject.SetActive(true);
        winerCard = ChasingBackend.getWiner(p1card, p2card);
        Debug.Log(winerCard);
    }

    void A1cardHideInTable()
    {
        A1cards.transform.Find("Hide").gameObject.SetActive(true);
    }

    void A2cardHideInTable()
    {
        A2cards.transform.Find("Hide").gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
