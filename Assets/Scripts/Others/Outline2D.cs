using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Outline2D : MonoBehaviour
{
    public SpriteRenderer targetSpriteRenderer; // The target SpriteRenderer
    public float outlineWidth = 0.05f; // Outline width
    public float animationSpeed = 2f; // Speed of the fade-in/out animation
    public Color outlineColor = Color.yellow; // Color of the outline

    private LineRenderer lineRenderer;
    private float alpha = 0f; // Current alpha of the outline
    private bool isFadingIn = true; // Fade direction

    void Start()
    {
        targetSpriteRenderer = GetComponent<SpriteRenderer>();

          // Initialize the LineRenderer
          lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.loop = true;
        lineRenderer.startWidth = outlineWidth;
        lineRenderer.endWidth = outlineWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = new Color(outlineColor.r, outlineColor.g, outlineColor.b, alpha);
        lineRenderer.endColor = new Color(outlineColor.r, outlineColor.g, outlineColor.b, alpha);

        // Generate outline points from the sprite
        GenerateOutline();

        // Start the fade-in/out animation
        StartCoroutine(AnimateOutline());
    }

    void GenerateOutline()
    {
        // Get the sprite's vertices in local space
        Sprite sprite = targetSpriteRenderer.sprite;
        Vector2[] spriteVertices = sprite.vertices;
        ushort[] spriteTriangles = sprite.triangles;

        // Calculate the world space points for the outline
        Vector3[] outlinePoints = new Vector3[spriteTriangles.Length];
        for (int i = 0; i < spriteTriangles.Length; i++)
        {
            Vector2 vertex = spriteVertices[spriteTriangles[i]];
            Vector3 worldPoint = targetSpriteRenderer.transform.TransformPoint(vertex);
            outlinePoints[i] = worldPoint;
        }

        // Assign points to the LineRenderer
        lineRenderer.positionCount = outlinePoints.Length;
        lineRenderer.SetPositions(outlinePoints);
    }

    IEnumerator AnimateOutline()
    {
        while (true)
        {
            // Animate the alpha value for fade-in/out
            if (isFadingIn)
            {
                alpha += Time.deltaTime * animationSpeed;
                if (alpha >= 1f)
                {
                    alpha = 1f;
                    isFadingIn = false;
                }
            }
            else
            {
                alpha -= Time.deltaTime * animationSpeed;
                if (alpha <= 0f)
                {
                    alpha = 0f;
                    isFadingIn = true;
                }
            }

            // Update the LineRenderer's color
            lineRenderer.startColor = new Color(outlineColor.r, outlineColor.g, outlineColor.b, alpha);
            lineRenderer.endColor = new Color(outlineColor.r, outlineColor.g, outlineColor.b, alpha);

            yield return null;
        }
    }
}
