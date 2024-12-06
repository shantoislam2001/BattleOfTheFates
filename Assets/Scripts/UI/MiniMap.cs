using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform player;              // প্লেয়ারের ট্রান্সফর্ম
    public Transform destination;         // গন্তব্যের ট্রান্সফর্ম
    public RectTransform pointer;         // মিনি ম্যাপের পয়েন্টারের RectTransform

    public float maxDistance = 50f;       // নিকটবর্তী দূরত্ব যেখানে পয়েন্টার প্রকৃত অবস্থানে দেখানো হবে
    public float miniMapRadius = 100f;    // মিনি ম্যাপের ব্যাসার্ধ

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
        // প্লেয়ার এবং গন্তব্যের মধ্যে দূরত্ব বের করা
        Vector3 offset = destination.position - player.position;
        float distance = offset.magnitude;

        // প্লেয়ারের দিকে ঘুরানোর জন্য গন্তব্যের ভেক্টরকে রোটেট করা
        Vector3 direction = Quaternion.Euler(0, -player.eulerAngles.y, 0) * offset.normalized;

        // গন্তব্য নিকটবর্তী থাকলে, আসল অবস্থানে পয়েন্টার দেখানো
        if (distance < maxDistance)
        {
            // মিনি ম্যাপের মধ্যে পয়েন্টারটি আসল অবস্থানে দেখানো
            pointer.anchoredPosition = new Vector2(direction.x, direction.z) * (miniMapRadius * (distance / maxDistance));
        }
        else
        {
            // দূরত্ব বেশি হলে, পয়েন্টারটি ম্যাপের সীমার মধ্যে সীমাবদ্ধ রাখা
            pointer.anchoredPosition = new Vector2(direction.x, direction.z) * miniMapRadius;
        }

        // গন্তব্যের দিকে পয়েন্টারটি ঘুরানো
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        pointer.rotation = Quaternion.Euler(0, 0, angle - 90);  // অ্যাঙ্গেল অ্যাডজাস্টমেন্ট
    }
}
