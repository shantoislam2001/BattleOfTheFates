using UnityEngine;

public class SlideAnimation
{
    public enum SlideDirection
    {
        LeftToRight,
        RightToLeft,
        UpToDown,
        DownToUp
    }

    private RectTransform panel; // Reference to the UI panel's RectTransform.
    private float slideDuration; // Duration of the animation.
    private Vector2 idlePosition; // On-screen position.
    private Vector2 offScreenPosition; // Off-screen position.

    // Constructor to initialize the animation properties.
    public SlideAnimation(RectTransform panel, float duration, SlideDirection direction, Canvas canvas, Vector2 idlePosition)
    {
        this.panel = panel;
        this.slideDuration = duration;
        this.idlePosition = idlePosition;

        // Get the canvas size to calculate positions.
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 canvasSize = canvasRect.sizeDelta;

        // Calculate positions based on the direction.
        idlePosition = Vector2.zero; // Center of the canvas (default on-screen position).
        switch (direction)
        {
            case SlideDirection.LeftToRight:
                offScreenPosition = new Vector2(-canvasSize.x, 0);
                break;
            case SlideDirection.RightToLeft:
                offScreenPosition = new Vector2(canvasSize.x, 0);
                break;
            case SlideDirection.UpToDown:
                offScreenPosition = new Vector2(0, canvasSize.y);
                break;
            case SlideDirection.DownToUp:
                offScreenPosition = new Vector2(0, -canvasSize.y);
                break;
            default:
                offScreenPosition = new Vector2(canvasSize.x, 0); // Default to RightToLeft.
                break;
        }

        // Ensure the panel starts in an inactive state and off-screen.
        panel.gameObject.SetActive(false);
        panel.anchoredPosition = offScreenPosition;
    }

    // Method to open the panel.
    public void OpenPanel()
    {
        // Activate the panel before starting the animation.
        panel.gameObject.SetActive(true);

        // Animate the panel to the idle position.
        LeanTween.move(panel, idlePosition, slideDuration)
                 .setEase(LeanTweenType.easeOutExpo);
    }

    // Method to close the panel.
    public void ClosePanel()
    {
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
