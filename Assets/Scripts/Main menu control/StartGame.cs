using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject panel;
    public SceneLoader loader;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void startNow()
    {
        panel.SetActive(true);
        loader.LoadScene("Map");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
