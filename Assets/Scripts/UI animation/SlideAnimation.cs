using UnityEngine;

public class UISlideAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform panel; // Assign your UI panel here.
    [SerializeField] private float slideDuration = 0.5f; // Duration of the animation.
    [SerializeField] private Vector2 idlePosition = Vector2.zero; // On-screen position.
    [SerializeField] private Vector2 offScreenPosition = new Vector2(1920, 0); // Off-screen position.
    [SerializeField] private GameObject openingButton;
    [SerializeField] private GameObject closingButton;


    private void Start()
    {
        // Ensure the panel starts in an inactive state and off-screen.
        panel.gameObject.SetActive(false);
        panel.anchoredPosition = offScreenPosition;
    }

    public void OpenPanel()
    {
        openingButton.SetActive(false);
        closingButton.SetActive(true);
        // Activate the panel before starting the animation.
        panel.gameObject.SetActive(true);

        // Animate the panel to the idle position.
        LeanTween.move(panel, idlePosition, slideDuration)
                 .setEase(LeanTweenType.easeOutExpo);
    }

    public void ClosePanel()
    {
        openingButton.SetActive(true);
        closingButton.SetActive(false);
        // Animate the panel to the off-screen position.
        LeanTween.move(panel, offScreenPosition, slideDuration)
                 .setEase(LeanTweenType.easeInExpo)
                 .setOnComplete(() =>
                 {
                     // Deactivate the panel after the animation finishes.
                     panel.gameObject.SetActive(false);
                 });
    }
}
