using UnityEngine;

public class UIController : MonoBehaviour
{
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


    private SlideAnimation cardsPanelAnimation;
    private SlideAnimation enterButtonAnimation;
    private SlideAnimation cardThrowAnimation;
    private SlideAnimation pickButtonAnimation;

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

    #region Pick card button evant
    // for windows 
    void pickCardW()
    {
        if (pickCardB.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            pickButtonInactive();
            keepCard(card.triggeredCard);
            Destroy(GameObject.Find(card.triggeredCard));
        }
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


    // Update is called once per frame
    void Update()
    {
            pickCardW();
    }
}
