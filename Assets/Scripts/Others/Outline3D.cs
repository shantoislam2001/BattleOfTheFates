using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Outline3D : MonoBehaviour
{
    public Transform targetObject; // The object to outline
    public float outlineWidth = 0.1f; // Width of the outline
    public float animationSpeed = 2f; // Speed of the animation
    public Color outlineColor = Color.yellow; // Outline color

    private LineRenderer lineRenderer;
    private Mesh targetMesh;
    private Vector3[] vertices;
    private int[] edges;
    private float alpha = 0f; // Current alpha of the outline
    private bool isFadingIn = true; // Fade direction

    void Start()
    {
        targetObject = GetComponent<Transform>();

          // Initialize LineRenderer
          lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = outlineWidth;
        lineRenderer.endWidth = outlineWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = new Color(outlineColor.r, outlineColor.g, outlineColor.b, alpha);
        lineRenderer.endColor = new Color(outlineColor.r, outlineColor.g, outlineColor.b, alpha);

        // Extract mesh and edges
        targetMesh = targetObject.GetComponent<MeshFilter>().mesh;
        GenerateOutline();
        StartCoroutine(AnimateOutline());
    }

    void GenerateOutline()
    {
        vertices = targetMesh.vertices;
        edges = targetMesh.triangles;

        // Assign points to LineRenderer
        Vector3[] outlinePoints = new Vector3[edges.Length];
        for (int i = 0; i < edges.Length; i++)
        {
            outlinePoints[i] = targetObject.TransformPoint(vertices[edges[i]]);
        }

        lineRenderer.positionCount = outlinePoints.Length;
        lineRenderer.SetPositions(outlinePoints);
    }

    IEnumerator AnimateOutline()
    {
        while (true)
        {
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

            lineRenderer.startColor = new Color(outlineColor.r, outlineColor.g, outlineColor.b, alpha);
            lineRenderer.endColor = new Color(outlineColor.r, outlineColor.g, outlineColor.b, alpha);

            yield return null;
        }
    }
}
