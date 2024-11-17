using Mono.Cecil.Cil;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class FirestoreDB : MonoBehaviour
{
   
    private string projectId = "battle-of-the-fates-2da89";
    private string apiKey = "AIzaSyDRpM5hJA_7oHm9uW7aCM2DvJfrlzq-YS0";

    private void Start()
    {
      
   
    }

    #region Post data

    public void SendDataToFirestore(string collectionName, string documentId, string jsonData)
    {
        StartCoroutine(PostDataToFirestore(collectionName, documentId, jsonData));
    }

    private IEnumerator PostDataToFirestore(string collectionName, string documentId, string jsonData)
    {
       
        string url = $"https://firestore.googleapis.com/v1/projects/{projectId}/databases/(default)/documents/{collectionName}/{documentId}?key={apiKey}";

        
        UnityWebRequest request = new UnityWebRequest(url, "PATCH"); // PATCH ব্যবহার করলে ডকুমেন্ট আপডেট করা যাবে
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        
        yield return request.SendWebRequest();

        
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Data successfully sent to Firestore: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error sending data: " + request.error);
        }
    }
    #endregion Post data
}
