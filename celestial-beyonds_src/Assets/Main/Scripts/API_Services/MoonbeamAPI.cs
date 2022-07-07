using System.Collections;
using Main.Scripts.Moonbeam;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

public class MoonbeamAPI : MonoBehaviour
{
    private const string _uri = "http://moonbeambot.live/api/chat";
    public GameObject dialogueOptionOne, dialogueOptionTwo, dialogueOptionThree, dialogueUI;
    public AudioClip[] moonbeamVoice;
    private AudioSource _audio;
    private InputProfiler _controls;
    private string _response;
    private GameObject _responseText, _moonbeam;
    private int _whichDialogue;
    public bool chatting;

    private void Awake()
    {
        _controls = new InputProfiler();
        _responseText = GameObject.FindGameObjectWithTag("ResponseText");
        _moonbeam = GameObject.FindGameObjectWithTag("Moonbeam");
        _audio = GetComponent<AudioSource>();
        StartCoroutine(GetRequest(_uri));
    }

    private void OnEnable()
    {
        _controls.Profiler.DialogueOptionOne.started += DialogueOptionOne;
        _controls.Profiler.DialogueOptionTwo.started += DialogueOptionTwo;
        _controls.Profiler.DialogueOptionThree.started += DialogueOptionThree;
        _controls.Profiler.ActivateDialogue.started += ActivateDialogue;
        _controls.Profiler.Enable();
    }

    private void OnDisable()
    {
        _controls.Profiler.DialogueOptionOne.started -= DialogueOptionOne;
        _controls.Profiler.DialogueOptionTwo.started -= DialogueOptionTwo;
        _controls.Profiler.DialogueOptionThree.started -= DialogueOptionThree;
        _controls.Profiler.ActivateDialogue.started -= ActivateDialogue;
        _controls.Profiler.Disable();
    }

    private void OnGUI()
    {
        _responseText.GetComponent<TextMeshProUGUI>().text = _response;
    }

    private void ActivateDialogue(InputAction.CallbackContext obj)
    {
        if (!chatting && _moonbeam.GetComponent<MoonbeamProfiler>().dialogueActive)
        {
            print("Dialogue Active");
            dialogueUI.SetActive(true);
            _moonbeam.GetComponent<MoonbeamProfiler>().mDialogueBtn.SetActive(false);
            chatting = true;
        }
        else if (chatting && _moonbeam.GetComponent<MoonbeamProfiler>().dialogueActive)
        {
            print("Dialogue not Active");
            _response = "";
            dialogueUI.SetActive(false);
            chatting = false;
        }
    }


    private void DialogueOptionOne(InputAction.CallbackContext obj)
    {
        if (chatting)
        {
            print("User says: " + dialogueOptionOne.GetComponent<TextMeshProUGUI>().text);
            _whichDialogue = 1;
            ChangeTextColor(_whichDialogue);
            StartCoroutine(PostRequest(_uri));
        }
    }

    private void DialogueOptionTwo(InputAction.CallbackContext obj)
    {
        if (chatting)
        {
            print("User says: " + dialogueOptionTwo.GetComponent<TextMeshProUGUI>().text);
            _whichDialogue = 2;
            ChangeTextColor(_whichDialogue);
            StartCoroutine(PostRequest(_uri));
        }
    }

    private void DialogueOptionThree(InputAction.CallbackContext obj)
    {
        if (chatting)
        {
            print("User says: " + dialogueOptionThree.GetComponent<TextMeshProUGUI>().text);
            _whichDialogue = 3;
            ChangeTextColor(_whichDialogue);
            StartCoroutine(PostRequest(_uri));
        }
    }

    private void ChangeTextColor(int whichOne)
    {
        if (whichOne == 1)
        {
            dialogueOptionOne.GetComponent<TextMeshProUGUI>().color = new Color32(20, 255, 0, 225);
            dialogueOptionTwo.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
            dialogueOptionThree.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
        }
        else if (whichOne == 2)
        {
            dialogueOptionOne.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
            dialogueOptionTwo.GetComponent<TextMeshProUGUI>().color = new Color32(20, 255, 0, 225);
            dialogueOptionThree.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
        }
        else if (whichOne == 3)
        {
            dialogueOptionOne.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
            dialogueOptionTwo.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
            dialogueOptionThree.GetComponent<TextMeshProUGUI>().color = new Color32(20, 255, 0, 225);
        }
    }

    private IEnumerator GetRequest(string uri)
    {
        //yield return new WaitForSeconds(1);
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

    private IEnumerator PostRequest(string uri)
    {
        var form = new WWWForm();

        switch (_whichDialogue)
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
            _audio.Stop();
            _audio.PlayOneShot(moonbeamVoice[Random.Range(0, moonbeamVoice.Length)], 0.5f);
        }
        else
        {
            print("Moonbeam says: " + webRequest.downloadHandler.text);
            _response = webRequest.downloadHandler.text;
            _audio.Stop();
            _audio.PlayOneShot(moonbeamVoice[Random.Range(0, moonbeamVoice.Length)], 0.5f);
        }
    }
}