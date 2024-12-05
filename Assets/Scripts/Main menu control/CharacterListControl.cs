using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;

public class CharacterListControl : MonoBehaviour
{
    public GlobalData globalData;
    
    public GameObject prevCharacter;
    public Vector3 position = new Vector3(-2311f, 745.5875f, -414.7499f);
    Quaternion rotation = Quaternion.identity;
    public float rotationY = -90f;

    public GameObject m1;
    public GameObject f1;

    public void characterSelect(string n)
    {
        if(prevCharacter != null)
        {
            Destroy(prevCharacter);

        }

        if (n == "m1")
        {
           
           prevCharacter = Instantiate( m1,  position, rotation);
            readyToShow(prevCharacter);
            globalData.character = n;
        }
        else if (n == "f1")
        {

            prevCharacter = Instantiate(f1, position, rotation);
            readyToShow(prevCharacter);
            globalData.character = n;
        }
    }

    void readyToShow(GameObject g)
    {
        g.transform.eulerAngles = new Vector3(0f, rotationY, 0f);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
