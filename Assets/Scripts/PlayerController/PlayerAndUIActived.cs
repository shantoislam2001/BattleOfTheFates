using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAndUIActived : MonoBehaviour
{
    public GlobalData globalData;
    public GameObject joystick;
    public GameObject jumpButton;
    public GameObject camreaParent;
    public GameObject cameraA;
    public Vector3 position = new Vector3(419.1494f, 0.2f, 324.1544f);
    Quaternion rotation = Quaternion.identity;


    public GameObject m1;
    public GameObject f1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerSpown(globalData.character);
    }

    void playerSpown(string n)
    {
        if(n == "m1")
        {
            GameObject ob = Instantiate(m1, position, rotation);
            activeControls(ob);
        }
    }

    void activeControls(GameObject ob)
    {
        if(globalData.isAndroid)
        {
            ob.GetComponent<PlayerInput>().enabled = false;
            ob.GetComponent<TPPControllerW>().enabled = false;
            ob.GetComponent<PlayerController>().enabled = true;
            joystick.SetActive(true);
            jumpButton.SetActive(true);
            cameraA.SetActive(true);
            camreaParent.SetActive(false);
        }
        else
        {
            ob.GetComponent<PlayerInput>().enabled = true;
            ob.GetComponent<TPPControllerW>().enabled = true;
            ob.GetComponent<PlayerController>().enabled = false;
            joystick.SetActive(false);
            jumpButton.SetActive(false);
            cameraA.SetActive(false);
            camreaParent.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
