using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform player;              
    public Transform destination;        
    public RectTransform pointer;        

    public float maxDistance = 50f;      
    public float miniMapRadius = 100f;   

    private void Start()
    {
        Invoke("setPlayer", 3f);
    }

    void setPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(destination != null)
        {
            pointer.gameObject.SetActive(true);
            UpdatePointerPosition();
        } else
        {
            pointer.gameObject.SetActive(false);
        }
        
    }

    private void UpdatePointerPosition()
    {
       
        Vector3 offset = destination.position - player.position;
        float distance = offset.magnitude;

        
        Vector3 direction = Quaternion.Euler(0, -player.eulerAngles.y, 0) * offset.normalized;

        
        if (distance < maxDistance)
        {
            
            pointer.anchoredPosition = new Vector2(direction.x, direction.z) * (miniMapRadius * (distance / maxDistance));
        }
        else
        {
           
            pointer.anchoredPosition = new Vector2(direction.x, direction.z) * miniMapRadius;
        }

      
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        pointer.rotation = Quaternion.Euler(0, 0, angle - 90);  
    }
}
