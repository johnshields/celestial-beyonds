using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class MoonbeamAPI : MonoBehaviour
{
    private const string _uri = "https://moonbeambot.live/api/chat";
    public GameObject dialogueOptionOne, dialogueOptionTwo, dialogueOptionThree, moonbeam;
    public AudioClip[] moonbeamVoice;
    private AudioSource _audio;
    private string _response;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        moonbeam = GameObject.FindGameObjectWithTag("Moonbeam");
        StartCoroutine(GetRequest(_uri));
    }

    private IEnumerator GetRequest(string uri)
    {
        // Send GET request.
        using var webRequest = UnityWebRequest.Get(uri);
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            print("Connection to API - Error: " + webRequest.error);
            print("Error with connection.");
            _response = "Sorry, I seem to have a screw lose.";
        }
        else
        {
            print("Connection to API: " + webRequest.result);
        }
    }

    public IEnumerator PostRequest(string uri)
    {
        var form = new WWWForm();
        
        switch (moonbeam.GetComponent<MoonbeamDialogue>().whichDialogue)
        {
            case 1:
                form.AddField("value", dialogueOptionOne.GetComponent<TextMeshProUGUI>().text);
                break;
            case 2:
                form.AddField("value", dialogueOptionTwo.GetComponent<TextMeshProUGUI>().text);
                break;
            case 3:
                form.AddField("value", dialogueOptionThree.GetComponent<TextMeshProUGUI>().text);
                break;
        }

        // Send POST request.
        using var webRequest = UnityWebRequest.Post(uri, form);
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
            webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            print("Error getting response: " + webRequest.error);
            _response = "Sorry, there seems to be a screw lose.";
            moonbeam.GetComponent<MoonbeamDialogue>().response = _response;
            _audio.Stop();
            _audio.PlayOneShot(moonbeamVoice[Random.Range(0, moonbeamVoice.Length)], 0.5f);
        }
        else
        {
            print("Moonbeam says: " + webRequest.downloadHandler.text);
            _response = webRequest.downloadHandler.text;
            moonbeam.GetComponent<MoonbeamDialogue>().response = _response;
            _audio.Stop();
            _audio.PlayOneShot(moonbeamVoice[Random.Range(0, moonbeamVoice.Length)], 0.5f);
        }
    }
}