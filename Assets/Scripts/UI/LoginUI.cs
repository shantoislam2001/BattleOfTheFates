using UnityEngine;

public class LoginUI : MonoBehaviour
{
    [SerializeField] private GameObject email;
    [SerializeField] private GameObject password;
    [SerializeField] private GameObject login;
    [SerializeField] private GameObject create;
    [SerializeField] private GameObject createAccount;
    [SerializeField] private GameObject loginAccount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void creatAccount ()
    {
        email.SetActive (true);
        login.SetActive (false);
        createAccount.SetActive (false);
        loginAccount.SetActive (true);
        password.SetActive (true);
        create.SetActive (true);
        
    }

    public void LoginAccount ()
    {
        email.SetActive(true);
        login.SetActive(true);
        createAccount.SetActive(true);
        loginAccount.SetActive(false);
        password.SetActive(true);
        create.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
