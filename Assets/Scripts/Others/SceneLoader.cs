using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private TextMeshProUGUI loadingText;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Begin loading the scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Prevent the scene from activating immediately
        operation.allowSceneActivation = false;

        // Update the progress bar
        while (!operation.isDone)
        {
            // Progress ranges from 0 to 0.9
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.BarValue = progress * 100;
            loadingText.text = $"Loading... {progress * 100:0}%";

            // Check if loading is complete
            if (operation.progress >= 0.9f)
            {
                loadingText.text = "Press any key to continue...";
               // operation.allowSceneActivation = true;
                // Wait for user input to activate the scene
                if (Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
