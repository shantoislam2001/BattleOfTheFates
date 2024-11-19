using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("Card panel")]
   
    [SerializeField] private RectTransform cardsPanel; // Assign panel 2's RectTransform.
    [SerializeField] private float animationDuration = 0.5f; // Shared animation duration.
    [SerializeField] private Canvas canvas; // Assign your Canvas here.
    [SerializeField] private Vector2 idlePosition = Vector2.zero;
    [SerializeField] private GameObject cardsOpeningB;
    [SerializeField] private GameObject cardsClosingB;

    [Header("Enter button")]

    [SerializeField] private RectTransform enterButton;
    [SerializeField] private Vector2 enterButtonIdlePosition = Vector2.zero;

    private SlideAnimation cardsPanelAnimation;
    private SlideAnimation enterButtonAnimation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardsPanelAnimation= new SlideAnimation(cardsPanel, animationDuration, SlideAnimation.SlideDirection.RightToLeft, canvas, idlePosition);
        enterButtonAnimation= new SlideAnimation(enterButton, animationDuration, SlideAnimation.SlideDirection.DownToUp, canvas, enterButtonIdlePosition);
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


    // Update is called once per frame
    void Update()
    {
        
    }
}
