using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private static LoadingScreen instance;
    private GameObject loadingScreen;
    private RectTransform circularLoader;
    private Canvas canvas;

    private bool isRotating = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            CreateLoadingScreen();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isRotating && circularLoader != null)
        {
            circularLoader.Rotate(Vector3.forward, -200 * Time.deltaTime);
        }
    }

    private void CreateLoadingScreen()
    {
        // Create Canvas
        GameObject canvasGO = new GameObject("LoadingScreenCanvas");
        canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 1000;

        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        canvasGO.AddComponent<GraphicRaycaster>();

        // Create Loading Screen Panel
        loadingScreen = new GameObject("LoadingScreen");
        loadingScreen.transform.SetParent(canvasGO.transform, false);
        RectTransform panelRect = loadingScreen.AddComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.sizeDelta = Vector2.zero;

        Image background = loadingScreen.AddComponent<Image>();
        background.color = new Color(0, 0, 0, 0.6f); // Semi-transparent black

        // Create Circular Loader
        GameObject loaderGO = new GameObject("CircularLoader");
        loaderGO.transform.SetParent(loadingScreen.transform, false);
        circularLoader = loaderGO.AddComponent<RectTransform>();
        circularLoader.sizeDelta = new Vector2(100, 100);

        Image loaderImage = loaderGO.AddComponent<Image>();
        loaderImage.sprite = Resources.Load<Sprite>("CircularLoader"); // Ensure you have a sprite named CircularLoader in Resources
        loaderImage.preserveAspect = true;

        loadingScreen.SetActive(false); // Initially hidden
    }

    public static void ShowLoadingScreen()
    {
        if (instance != null && instance.loadingScreen != null)
        {
            instance.loadingScreen.SetActive(true);
            instance.isRotating = true;
        }
    }

    public static void HideLoadingScreen()
    {
        if (instance != null && instance.loadingScreen != null)
        {
            instance.loadingScreen.SetActive(false);
            instance.isRotating = false;
        }
    }
}
