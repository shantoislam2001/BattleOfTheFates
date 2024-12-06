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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
          prince = 2;
          stepmother = 2;
          witch = 2;
          king = 5;
          rebel = 2;
          fate = 2;
        
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
        if(prince > 0)
        {
            string currentSlot = GameObject.FindGameObjectWithTag("Player").GetComponent<Cards>().currentSlot;
            Debug.Log("cs "+currentSlot);
            prince--;
            throwedCard = "Prince";
            GameObject.Find("Tables").transform.Find("Slot "+currentSlot).transform.Find("Cards").transform.Find("Hide").gameObject.SetActive(true);    
            UIController.Self.cardThrowPanelClose();
        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
