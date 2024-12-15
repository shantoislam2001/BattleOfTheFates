using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public MiniMap map;
    public CardSpawner spawner;
    [Header("Card panel")]
    public static UIController Self { get; private set; }
    [SerializeField] private RectTransform cardsPanel; // Assign panel 2's RectTransform.
    [SerializeField] private float animationDuration = 0.5f; // Shared animation duration.
    [SerializeField] private Canvas canvas; // Assign your Canvas here.
    [SerializeField] private Vector2 idlePosition = Vector2.zero;
    [SerializeField] private GameObject cardsOpeningB;
    [SerializeField] private GameObject cardsClosingB;
    [SerializeField] private GameObject pickCardB;

    [Header("Enter button")]

    [SerializeField] private RectTransform enterButton;
    [SerializeField] private Vector2 enterButtonIdlePosition = Vector2.zero;

    [Header("Card trow panel")]
    [SerializeField] private RectTransform cardThrowPanel;
    [SerializeField] private Vector2 cardThrowPanelPosition = Vector2.zero;

    [Header("Card pick up button")]
    [SerializeField] private RectTransform pickButton;
    [SerializeField] private Vector2 pickButtonPosition = Vector2.zero;

    [Header("Win Popup")]
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private RectTransform winPopup;
    [SerializeField] private Vector2 winPopupPosition = Vector2.zero;

    [Header("Tie Popup")]
    [SerializeField] private TextMeshProUGUI tieText;
    [SerializeField] private RectTransform tiePopup;
    [SerializeField] private Vector2 tiePopupPosition = Vector2.zero;


    [Header("Lost Popup")]
    [SerializeField] private TextMeshProUGUI lostText;
    [SerializeField] private RectTransform lostPopup;
    [SerializeField] private Vector2 lostPopupPosition = Vector2.zero;

    [Header("Countdown timer")]
    [SerializeField] private RectTransform cdTimer;
    [SerializeField] private Vector2 cdTimerPosition = Vector2.zero;

    [Header("Chasing end timer")]
    [SerializeField] private RectTransform ceTimer;
    [SerializeField] private Vector2 ceTimerPosition = Vector2.zero;

    [Header("Next round panel")]
    [SerializeField] private RectTransform nextRoundPanel;
    [SerializeField] private Vector2 nextRoundPanelPosition = Vector2.zero;

    [Header("Finding end timer")]
    [SerializeField] private RectTransform feTimer;
    [SerializeField] private Vector2 feTimerPosition = Vector2.zero;

    [Header("Finally win panel")]
    [SerializeField] private RectTransform finallyWinPanel;
    [SerializeField] private Vector2 finallyWinPanelPosition = Vector2.zero;

    private SlideAnimation cardsPanelAnimation;
    private SlideAnimation enterButtonAnimation;
    private SlideAnimation cardThrowAnimation;
    private SlideAnimation pickButtonAnimation;
    private SlideAnimation winPopupAnimation;
    private SlideAnimation lostPopupAnimation;
    private SlideAnimation tiePopupAnimation;
    private SlideAnimation cdTimerAnimation;
    private SlideAnimation ceTimerAnimation;
    private SlideAnimation feTimerAnimation;
    private SlideAnimation nextRoundPanelAnimation;
    private SlideAnimation finallyWinPanelAnimation;

    private Cards card;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        card = GameObject.FindGameObjectWithTag("Player").GetComponent<Cards>();
        cardsPanelAnimation= new SlideAnimation(cardsPanel, animationDuration, SlideAnimation.SlideDirection.RightToLeft, canvas, idlePosition);
        enterButtonAnimation= new SlideAnimation(enterButton, animationDuration, SlideAnimation.SlideDirection.DownToUp, canvas, enterButtonIdlePosition);
        cardThrowAnimation = new SlideAnimation(cardThrowPanel, animationDuration, SlideAnimation.SlideDirection.DownToUp, canvas, cardThrowPanelPosition);
        pickButtonAnimation = new SlideAnimation(pickButton, animationDuration, SlideAnimation.SlideDirection.RightToLeft, canvas, pickButtonPosition);
        winPopupAnimation = new SlideAnimation(winPopup, animationDuration, SlideAnimation.SlideDirection.LeftToRight, canvas, winPopupPosition);
        lostPopupAnimation = new SlideAnimation(lostPopup, animationDuration, SlideAnimation.SlideDirection.LeftToRight, canvas, lostPopupPosition);
        tiePopupAnimation = new SlideAnimation(tiePopup, animationDuration, SlideAnimation.SlideDirection.LeftToRight, canvas, tiePopupPosition);
        cdTimerAnimation = new SlideAnimation(cdTimer, animationDuration, SlideAnimation.SlideDirection.UpToDown, canvas, cdTimerPosition);
        ceTimerAnimation = new SlideAnimation(ceTimer, animationDuration, SlideAnimation.SlideDirection.RightToLeft, canvas, ceTimerPosition);
        feTimerAnimation = new SlideAnimation(feTimer, animationDuration, SlideAnimation.SlideDirection.RightToLeft, canvas, feTimerPosition);
        nextRoundPanelAnimation = new SlideAnimation(nextRoundPanel, animationDuration, SlideAnimation.SlideDirection.LeftToRight, canvas, nextRoundPanelPosition);
        finallyWinPanelAnimation = new SlideAnimation(finallyWinPanel, animationDuration, SlideAnimation.SlideDirection.LeftToRight, canvas, finallyWinPanelPosition);
        
    }

    // Cards 
    public void cardsOpening()
    {
        cardsPanelAnimation.OpenPanel();
        cardsOpeningB.SetActive(false);
        cardsClosingB.SetActive(true);
    }

    // Cards 
    public void cardsClosing()
    {
        cardsPanelAnimation.ClosePanel();
        cardsOpeningB.SetActive(true);
        cardsClosingB.SetActive(false);
    }

    // Enter button
    public void enterButtonActive()
    {
        enterButtonAnimation.OpenPanel();
    }

    // Enter button
    public void enterButtonInactive()
    {
        enterButtonAnimation.ClosePanel();
    }


    public void cardThowPanelOpen()
    {
        cardThrowAnimation.OpenPanel();
    }

    public void cardThrowPanelClose()
    {
        cardThrowAnimation.ClosePanel();
    }

    public void pickButtonActive()
    {
        pickButtonAnimation.OpenPanel();
    }

    public void pickButtonInactive()
    {
        pickButtonAnimation.ClosePanel();
    }

    public void winPopupActive(string t)
    {
        winPopupAnimation.OpenPanel();
        winText.text = "Oponent : " + t;

    }

    public void winPopupInactive()
    {
        winPopupAnimation.ClosePanel();
    }

    public void lostPopupActive(string t)
    {
        lostPopupAnimation.OpenPanel();
        lostText.text = "Oponent : " + t;
    }

    public void lostPopupInactive()
    {
        lostPopupAnimation.ClosePanel();
    }

    public void tiePopupActive(string t)
    {
        tiePopupAnimation.OpenPanel();
        tieText.text = "Oponent : " + t;
    }

    public void tiePopupInactive()
    {
        tiePopupAnimation.ClosePanel();
    }


    public void cdTimerActive()
    {
        cdTimerAnimation.OpenPanel();
    }

    public void cdTimerInactive()
    {
        cdTimerAnimation.ClosePanel();
    }

    public void ceTimerActive()
    {
        ceTimerAnimation.OpenPanel();
    }

    public void ceTimerInactive()
    {
        ceTimerAnimation.ClosePanel();
    }

    public void nextRoundPanelActive()
    {
        nextRoundPanelAnimation.OpenPanel();
    }

    public void nextRoundPanelInactive()
    {
        nextRoundPanelAnimation.ClosePanel();
    }

    public void feTimerActive()
    {
        feTimerAnimation.OpenPanel();
    }

    public void feTimerInactive()
    {
        feTimerAnimation.ClosePanel();
    }

    public void finallyWinActive()
    {
        finallyWinPanelAnimation.OpenPanel();
    }

    public void finallyWinInactive()
    {
        finallyWinPanelAnimation.ClosePanel();
    }


    #region Pick card button evant
    // for windows 
    void pickCardW()
    {
        if (pickCardB.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            pickButtonInactive();
            keepCard(card.triggeredCard);
          
           // map.destination = null;
           spawner.DestroyObjectByName(card.triggeredCard);
        }
    }
    
    public void cardPickButton()
    {
        pickButtonInactive();
        keepCard(card.triggeredCard);

        // map.destination = null;
        spawner.DestroyObjectByName(card.triggeredCard);
    }

    void keepCard(string c)
    {
        if(c.Contains("Prince") )
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




    #endregion Pick card button evant

    #region Card UI text control
    // card throw panel
    public TextMeshProUGUI princeCTT;
    public TextMeshProUGUI stepmotherCTT;
    public TextMeshProUGUI witchCTT;
    public TextMeshProUGUI kingCTT;
    public TextMeshProUGUI rebelCTT;
    public TextMeshProUGUI fateCTT;

    // Card display panel
    public TextMeshProUGUI princeCD;
    public TextMeshProUGUI stepmotherCD;
    public TextMeshProUGUI witchCD;
    public TextMeshProUGUI kingCD;
    public TextMeshProUGUI rebelCD;
    public TextMeshProUGUI fateCD;

    void setTextValue()
    {
        Cards card = GameObject.FindGameObjectWithTag("Player").GetComponent<Cards>();
        princeCTT.text = "" + card.prince;
        stepmotherCTT.text = "" + card.stepmother;
        witchCTT.text = "" + card.witch;
        rebelCTT.text = "" + card.rebel;
        kingCTT.text = "" + card.king;
        fateCTT.text = "" + card.fate;

        
        princeCD.text = "" + card.prince;
        stepmotherCD.text = "" + card.stepmother;
        witchCD.text = "" + card.witch;
        rebelCD.text = "" + card.rebel;
        kingCD.text = "" + card.king;
        fateCD.text = "" + card.fate;
    }


    #endregion Card UI text control

    // Update is called once per frame
    void Update()
    {
        pickCardW();
        setTextValue();
    }
}
