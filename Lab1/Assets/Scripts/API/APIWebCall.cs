using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;

public class APIWebCall : MonoBehaviour
{
    public static IEnumerator PostMethod(string jsonData)
    {
        string url = "https://gpp-project2.anvil.app/_/api/metric";
        using (UnityWebRequest request = UnityWebRequest.Put(url, jsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();

            if (!request.isNetworkError && request.responseCode == (int)HttpStatusCode.OK)
                Debug.Log("Data successfully sent to the server");
            else
                Debug.Log("Error sending data to the server: Error " + request.responseCode);
        }
    }
}

//[System.Serializable]
public class GameState
{
    public string version;
    public string timestamp;
    public string user_id;
    public string event_name;
    public int data;
}