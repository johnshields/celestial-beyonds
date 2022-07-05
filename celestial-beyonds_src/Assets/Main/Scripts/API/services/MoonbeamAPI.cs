using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MoonbeamAPI : MonoBehaviour
{
    private void Start()
    {
        const string uri = "http://moonbeambot.live/api/chat";
        StartCoroutine(GetRequest(uri));
    }

    private IEnumerator GetRequest(string uri)
    {
        using (var webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Connection to API - Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Connection to API: " + webRequest.result);
            }
        }
    }
}