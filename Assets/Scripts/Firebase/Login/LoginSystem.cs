using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Mono.Cecil.Cil;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LoginSystem : MonoBehaviour
{
    
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField password;
   

    private string emailS;
    private string passwordS;
   

    
    public FirestoreDB firestore;
    FirebaseAuth firebaseAuth = new FirebaseAuth("AIzaSyDRpM5hJA_7oHm9uW7aCM2DvJfrlzq-YS0");

    // Start is called once before the first execution of Update after the MonoBehaviour is created
     void  Start()
    {
       
        firestore = GetComponent<FirestoreDB>();

        email.onValueChanged.AddListener(emailInput);
         password.onValueChanged.AddListener(passwordInput);
       

    }

  

    // Update is called once per frame
    void Update()
    {
        
    }
    #region Register
    public   void create()
    {
        
        var registerInfo = new LoginInfo(emailS, passwordS);
        StartCoroutine(firebaseAuth.Register(registerInfo,
            onSuccess: response => createUser(emailS),
            onError: error => Debug.Log(error)));




    }

  
    public  void createUser( string email)
    {
        SceneManager.LoadScene("Lobby");

        string jsonData = $@"
{{
    ""fields"": {{
        
        ""email"": {{ ""stringValue"": ""{email}"" }}
    }}
}}";

        firestore.SendDataToFirestore("Users", emailS, jsonData);

    }

    private async Task CheckUsernameExists(string collection, string documentId)
    {
        try
        {
           // usernameIsNew = await firestore.DocumentExists(collection, documentId);
            
        }
        catch (Exception ex)
        {
            Debug.Log($"An error occurred while checking document existence: {ex.Message}");
        }
    }
    #endregion Register

    #region Login

    public void login()
    {
        try
        {
            var loginInfo = new LoginInfo(emailS, passwordS);
            StartCoroutine(firebaseAuth.Login(loginInfo,
                onSuccess: response => SceneManager.LoadScene("Lobby"),
                onError: error => Debug.Log(error)));
        }catch(Exception e) { }
    }

    #endregion Login


   



    void emailInput(string newText)
    {
        emailS = newText;
    }

    void passwordInput(string newText)
    {
        passwordS = newText;
    }



}
