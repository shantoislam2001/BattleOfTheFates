using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System;


public class Learning : MonoBehaviour
{
    private const string projectId = "battle-of-the-fates-2da89"; // e.g., "my-firebase-project"
    private const string apiKey = "AIzaSyDRpM5hJA_7oHm9uW7aCM2DvJfrlzq-YS0"; // e.g., "AIzaSyXXXXXXX"


    private string firestoreUrl = $"https://firestore.googleapis.com/v1/projects/{projectId}/databases/(default)/documents";

    void Start()
    {
        

        // Call the method and handle the result with a callback
        GetFieldValueFromFirestore("Users", "shanto", "username", OnDataRetrieved);
    }

    private void OnDataRetrieved(string data)
    {
        if (data != null)
        {
           string retrievedData = data;
            Debug.Log("Data successfully retrieved: " + retrievedData);
        }
        else
        {
            Debug.LogWarning("Failed to retrieve data.");
        }
    }


    public void GetFieldValueFromFirestore(string collectionName, string documentId, string fieldName, Action<string> callback)
    {
        StartCoroutine(GetData(collectionName, documentId, fieldName, callback));
    }

    private IEnumerator GetData(string collectionName, string documentId, string fieldName, Action<string> callback)
    {
        string url = $"{firestoreUrl}/{collectionName}/{documentId}?key={apiKey}";

        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;

            // Parse JSON into the Firestore response object and extract field value
            FirestoreDocument document = JsonUtility.FromJson<FirestoreDocument>(jsonResponse);
            string fieldValue = GetFieldValue(document, fieldName);

            callback?.Invoke(fieldValue);
        }
        else
        {
            Debug.LogError("Error retrieving data from Firestore: " + request.error);
            callback?.Invoke(null);
        }
    }

    private string GetFieldValue(FirestoreDocument document, string fieldName)
    {
        if (document.fields != null && document.fields.ContainsKey(fieldName))
        {
            // Return the value as a string (change "stringValue" to match your field type if needed)
            return document.fields[fieldName].stringValue;
        }

        return null;
    }
}

// Firestore response structure classes
[Serializable]
public class FirestoreDocument
{
    public Dictionary<string, FirestoreField> fields;
}

[Serializable]
public class FirestoreField
{
    public string stringValue;
    public int integerValue;
    public bool booleanValue;

    // Add other Firestore data types here as needed
}