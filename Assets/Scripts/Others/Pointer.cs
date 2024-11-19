using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircularPositionMarker : MonoBehaviour
{
    [Header("Circle Settings")]
    public int segments = 100;         // Number of segments in the circle
    public float radius = 1f;          // Base radius of the circle
    public float animationSpeed = 2f;  // Speed of the pulsation animation
    public float pulseScale = 0.2f;    // Pulsation scale (how much the circle grows/shrinks)

    [Header("Color Settings")]
    public Gradient colorGradient;     // Gradient to define color changes
    public float colorChangeSpeed = 1f; // Speed of color transitions

    private LineRenderer lineRenderer;
    private float animationTime = 0f;  // Tracks time for radius animation
    private float colorTime = 0f;      // Tracks time for color changes

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1; // +1 to close the circle
        lineRenderer.loop = true; // Ensure the circle loops

        // Assign a material with a built-in shader that supports vertex colors
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Set initial gradient if not assigned
        if (colorGradient == null)
        {
            colorGradient = new Gradient
            {
                colorKeys = new GradientColorKey[]
                {
                    new GradientColorKey(Color.red, 0f),
                    new GradientColorKey(Color.blue, 0.5f),
                    new GradientColorKey(Color.green, 1f)
                },
                alphaKeys = new GradientAlphaKey[]
                {
                    new GradientAlphaKey(1f, 0f),
                    new GradientAlphaKey(1f, 1f)
                }
            };
        }
    }

    void Update()
    {
        AnimateCircle();
        UpdateColor();
    }

    void AnimateCircle()
    {
        animationTime += animationSpeed * Time.deltaTime;

        // Calculate pulsating radius
        float animatedRadius = radius + Mathf.Sin(animationTime) * pulseScale;

        // Draw the circle
        for (int i = 0; i < segments + 1; i++)
        {
            float angle = Mathf.Deg2Rad * (360f / segments * i);
            float x = Mathf.Cos(angle) * animatedRadius;
            float y = Mathf.Sin(angle) * animatedRadius;
            lineRenderer.SetPosition(i, new Vector3(x, 0, y));
        }
    }

    void UpdateColor()
    {
        // Update color over time
        colorTime += colorChangeSpeed * Time.deltaTime;
        colorTime %= 1f; // Keep time within the 0-1 range for looping

        // Evaluate color from gradient
        Color currentColor = colorGradient.Evaluate(colorTime);

        // Apply color to LineRenderer
        lineRenderer.startColor = currentColor;
        lineRenderer.endColor = currentColor;
    }
}
