using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MoonbeamAPI : MonoBehaviour
{
    private AudioSource _audio;
    private string _response;
    private const string _uri = "https://api.moonbeambot.live/api/chat";
    private GameObject _mb;
    public GameObject randoAudio;
    public bool itIsAQuestion, disabledMoonbeam;

    private void Awake()
    {
        //disabledMoonbeam = true;
        _response = string.Empty;
        
        _audio = GetComponent<AudioSource>();
        _mb = GameObject.FindGameObjectWithTag("Moonbeam");
        if (!disabledMoonbeam)
            StartCoroutine(GetRequest(_uri));
    }

    private void Start()
    {
        PlayRandomClip(0f);
    }

    private IEnumerator GetRequest(string uri)
    {
        if (!disabledMoonbeam)
        {
            // Send GET request.
            using var webRequest = UnityWebRequest.Get(uri);
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                print("Connection to API - Error: " + webRequest.error);
                _response = "Sorry, I seem to have a screw lose.";
            }
            else
                print("Connection to API: " + webRequest.result);   
            
            // When server is down.
            if (webRequest.error == "Request timeout")
            {
                print("Error timeout: " + webRequest.error);
                _response = "Sorry, my API is down.";
                _mb.GetComponent<MoonbeamDialogue>().response = _response;
                PlayRandomClip(0.5f);
            }
        }
    }

    public IEnumerator PostRequest(string uri)
    {
        if (!disabledMoonbeam)
        {
            var form = new WWWForm();
            GetComponent<DialogueForm>().SetUpForm(form);
            // Send POST request.
            using var webRequest = UnityWebRequest.Post(uri, form);
            yield return webRequest.SendWebRequest();
            // if there is a Request error
            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError ||  
                webRequest.result == UnityWebRequest.Result.DataProcessingError)
            {
                print("Error getting response: " + webRequest.error);
                _response = "Sorry, there seems to be a screw lose.";
                _mb.GetComponent<MoonbeamDialogue>().response = _response;
                PlayRandomClip(0.5f);
            }
            // if Moonbeam can indeed respond.
            else
            {
                // response from API.
                print("Moonbeam says: " + webRequest.downloadHandler.text);
                _response = webRequest.downloadHandler.text;
                _mb.GetComponent<MoonbeamDialogue>().response = _response;
                IsItAQuestion(); // checks to see if its a question for Dialogue Trees.
                PlayRandomClip(0.5f);
            }

            // When server is down.
            if (webRequest.error == "Request timeout")
            {
                print("Error timeout: " + webRequest.error);
                _response = "Sorry, my API is down.";
                _mb.GetComponent<MoonbeamDialogue>().response = _response;
                PlayRandomClip(0.5f);
            }
        }
    }

    private void IsItAQuestion()
    {
        // see if it's a "you?" question
        var words = _response.Split(' ');
        var lastWord = words[words.Length - 1];
        if (lastWord == "you?" || lastWord == "You?")
        {
            itIsAQuestion = true;
            print($"itIsAQuestion: {itIsAQuestion}" + $"Moonbeam asked: {lastWord}");
        }
        else
        {
            itIsAQuestion = false;
            print($"itIsAQuestion: {itIsAQuestion}");
        }
    }

    public void PlayRandomClip(float vol)
    {
        _audio.Stop();
        _audio.PlayOneShot(randoAudio.GetComponent<AudioRandomizer>().GetRandomClip("Moonbeam/SFX"), vol);
    }
}