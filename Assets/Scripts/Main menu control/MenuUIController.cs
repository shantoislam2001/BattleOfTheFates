using UnityEngine;

public class MenuUIController : MonoBehaviour
{
    [Header("Character list panel")]
    public static UIController Self { get; private set; }
    [SerializeField] private RectTransform characterListPanel; // Assign panel 2's RectTransform.
    [SerializeField] private float animationDuration = 0.5f; // Shared animation duration.
    [SerializeField] private Canvas canvas; // Assign your Canvas here.
    [SerializeField] private Vector2 characterListPanelPosition = Vector2.zero;
    

    private SlideAnimation characterListAnimation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterListAnimation = new SlideAnimation(characterListPanel, animationDuration, SlideAnimation.SlideDirection.LeftToRight, canvas, characterListPanelPosition);
    }

    public void characterListOpen()
    {
        characterListAnimation.OpenPanel();
    }

    public void characterListClose()
    {
        characterListAnimation.ClosePanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
