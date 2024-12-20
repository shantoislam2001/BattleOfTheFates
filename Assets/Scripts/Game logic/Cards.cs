using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

public class Cards : MonoBehaviour
{
    public int prince;
    public int stepmother;
    public int witch;
    public int king;
    public int rebel;
    public int fate;
    public List<string> card = new List<string>();
    public string throwedCard;
    public string currentSlot;
    public string triggeredCard;
    public bool played = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
          prince = 2;
          stepmother = 2;
          witch = 2;
          king = 2;
          rebel = 2;
          fate = 5;
        
    }

    public void aiCardThrowing()
    {
        throwedCard = throwCard();
    }

    public string throwCard()
    {
       if(prince > 0)
        {
            card.Add("Prince");
        }

       if(stepmother > 0)
        {
            card.Add("Stepmother");
        }

       if(witch > 0)
        {
            card.Add("Witch");
        }

       if(king > 0)
        {
            card.Add("King");
        }

       if(rebel > 0)
        {
            card.Add("Rebel");
        }

       if (fate > 0)
        {
            card.Add("Fate");
        }
       
       if (card.Count == 0)
        {
            return "Empty";
        }

        int r = Random.Range(0, card.Count);
        return card[r];
    }

    // card throwing system for player 
    public void princeCard()
    {
        Cards cards = GameObject.FindGameObjectWithTag("Player").GetComponent<Cards>();
        if (cards.prince > 0)
        {
          
            string currentSlot = cards.currentSlot;
            cards.prince--;
            cards.throwedCard = "Prince";
            GameObject.Find("Tables").transform.Find("Slot "+currentSlot).transform.Find("Cards").transform.Find("Hide").gameObject.SetActive(true);    
            UIController.Self.cardThrowPanelClose();
        }
    }

    public void stepmotherCard()
    {
        Cards cards = GameObject.FindGameObjectWithTag("Player").GetComponent<Cards>();
        if (cards.stepmother > 0)
        {
           
            string currentSlot = cards.currentSlot;
            cards.stepmother--;
            cards.throwedCard = "Stepmother";
            GameObject.Find("Tables").transform.Find("Slot " + currentSlot).transform.Find("Cards").transform.Find("Hide").gameObject.SetActive(true);
            UIController.Self.cardThrowPanelClose();
        }
    }

    public void witchCard()
    {
        Cards cards = GameObject.FindGameObjectWithTag("Player").GetComponent<Cards>();
        if (cards.witch > 0)
        {

            string currentSlot = cards.currentSlot;
            cards.witch--;
            cards.throwedCard = "Witch";
            GameObject.Find("Tables").transform.Find("Slot " + currentSlot).transform.Find("Cards").transform.Find("Hide").gameObject.SetActive(true);
            UIController.Self.cardThrowPanelClose();
        }
    }

    public void kingCard()
    {
        Cards cards = GameObject.FindGameObjectWithTag("Player").GetComponent<Cards>();
        if (cards.king > 0)
        {

            string currentSlot = cards.currentSlot;
            cards.king--;
            cards.throwedCard = "King";
            GameObject.Find("Tables").transform.Find("Slot " + currentSlot).transform.Find("Cards").transform.Find("Hide").gameObject.SetActive(true);
            UIController.Self.cardThrowPanelClose();
        }
    }

    public void rebelCard()
    {
        Cards cards = GameObject.FindGameObjectWithTag("Player").GetComponent<Cards>();
        if (cards.rebel > 0)
        {

            string currentSlot = cards.currentSlot;
            cards.rebel--;
            cards.throwedCard = "Rebel";
            GameObject.Find("Tables").transform.Find("Slot " + currentSlot).transform.Find("Cards").transform.Find("Hide").gameObject.SetActive(true);
            UIController.Self.cardThrowPanelClose();
        }
    }

    public void fateCard()
    {
        Cards cards = GameObject.FindGameObjectWithTag("Player").GetComponent<Cards>();
        if (cards.fate > 0)
        {

            string currentSlot = cards.currentSlot;
            cards.fate--;
            cards.throwedCard = "Fate";
            GameObject.Find("Tables").transform.Find("Slot " + currentSlot).transform.Find("Cards").transform.Find("Hide").gameObject.SetActive(true);
            UIController.Self.cardThrowPanelClose();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
