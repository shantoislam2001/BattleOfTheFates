using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FirebaseAuth
{
    private readonly string apiKey;

    public FirebaseAuth(string firebaseApiKey)
    {
        apiKey = firebaseApiKey;
    }

    // Login Method
    public IEnumerator Login(LoginInfo loginInfo, Action<string> onSuccess, Action<string> onError)
    {
        string url = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}";
        string json = JsonUtility.ToJson(loginInfo);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] jsonToSend = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(request.downloadHandler.text);
            }
            else
            {
                onError?.Invoke($"Login failed: {request.error}\n{request.downloadHandler.text}");
            }
        }
    }

    // Register Method
    public IEnumerator Register(LoginInfo registerInfo, Action<string> onSuccess, Action<string> onError)
    {
        string url = $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={apiKey}";
        string json = JsonUtility.ToJson(registerInfo);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] jsonToSend = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(request.downloadHandler.text);
            }
            else
            {
                onError?.Invoke($"Registration failed: {request.error}\n{request.downloadHandler.text}");
            }
        }
    }

    // Reset Password Method
    public IEnumerator ResetPassword(ResetPasswordInfo resetInfo, Action<string> onSuccess, Action<string> onError)
    {
        string url = $"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key={apiKey}";
        string json = JsonUtility.ToJson(resetInfo);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] jsonToSend = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(request.downloadHandler.text);
            }
            else
            {
                onError?.Invoke($"Password reset failed: {request.error}\n{request.downloadHandler.text}");
            }
        }
    }
}

[Serializable]
public class LoginInfo
{
    public string email;
    public string password;
    public bool returnSecureToken = true;

    public LoginInfo(string email, string password)
    {
        this.email = email;
        this.password = password;
    }
}

[Serializable]
public class ResetPasswordInfo
{
    public string requestType = "PASSWORD_RESET";
    public string email;

    public ResetPasswordInfo(string email)
    {
        this.email = email;
    }
}
