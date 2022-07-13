using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class MoonbeamAPI : MonoBehaviour
{
    private const string _uri = "https://api.moonbeambot.live/api/chat";
    private GameObject _mb;
    public AudioClip[] moonbeamVoice;
    private AudioSource _audio;
    private string _response;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _mb = GameObject.FindGameObjectWithTag("Moonbeam");
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
        SetUpForm(form);
        // Send POST request.
        using var webRequest = UnityWebRequest.Post(uri, form);
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
            webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            print("Error getting response: " + webRequest.error);
            _response = "Sorry, there seems to be a screw lose.";
            _mb.GetComponent<MoonbeamDialogue>().response = _response;
            _audio.Stop();
            _audio.PlayOneShot(moonbeamVoice[Random.Range(0, moonbeamVoice.Length)], 0.5f);
        }
        else
        {
            print("Moonbeam says: " + webRequest.downloadHandler.text);
            _response = webRequest.downloadHandler.text;
            _mb.GetComponent<MoonbeamDialogue>().response = _response;
            _audio.Stop();
            _audio.PlayOneShot(moonbeamVoice[Random.Range(0, moonbeamVoice.Length)], 0.5f);
        }
    }


    private void SetUpForm(WWWForm form)
    {
        switch (_mb.GetComponent<MoonbeamDialogue>().whichDialogue)
        {
            // general
            case 1:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().dialogueOptions[0].GetComponent<TextMeshProUGUI>().text);
                break;
            case 2:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().dialogueOptions[1].GetComponent<TextMeshProUGUI>().text);
                break;
            case 3:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().dialogueOptions[2].GetComponent<TextMeshProUGUI>().text);
                break;
            // artifacts
            case 4:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[0].GetComponent<TextMeshProUGUI>().text);
                break;
            case 5:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[1].GetComponent<TextMeshProUGUI>().text);
                break;
            case 6:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[2].GetComponent<TextMeshProUGUI>().text);
                break;
        }
    }
}