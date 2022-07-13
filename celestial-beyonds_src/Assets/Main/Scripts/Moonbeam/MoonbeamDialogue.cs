using System.Collections;
using System.Collections.Generic;
using Main.Scripts.Moonbeam;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoonbeamDialogue : MonoBehaviour
{
    private const string _uri = "https://api.moonbeambot.live/api/chat";
    private GameObject _responseText, _moonbeamAPI, _artifacts;
    private InputProfiler _controls;
    public GameObject dialogueBoxes, dOptions, artifactUI, askPrompt;
    // [0]-[2] (G1), [0]-[2] (A1)
    public GameObject[] artifactQuestions, dialogueOptions;
    public string response;
    public int whichDialogue;
    public bool chatting, asking;

    private void Awake()
    {
        _controls = new InputProfiler();
        _artifacts = GameObject.FindGameObjectWithTag("Artifacts");
        _responseText = GameObject.FindGameObjectWithTag("ResponseText");
        _moonbeamAPI = GameObject.FindGameObjectWithTag("MoonbeamAPI");
    }

    private void OnEnable()
    {
        _controls.Profiler.DialogueOptionOne.started += DialogueOptionOne;
        _controls.Profiler.DialogueOptionTwo.started += DialogueOptionTwo;
        _controls.Profiler.DialogueOptionThree.started += DialogueOptionThree;
        _controls.Profiler.ActivateDialogue.started += ActivateDialogue;
        _controls.Profiler.AskMoonbeam.started += AskMoonbeam;
        _controls.Profiler.Enable();
    }

    private void OnDisable()
    {
        _controls.Profiler.DialogueOptionOne.started -= DialogueOptionOne;
        _controls.Profiler.DialogueOptionTwo.started -= DialogueOptionTwo;
        _controls.Profiler.DialogueOptionThree.started -= DialogueOptionThree;
        _controls.Profiler.ActivateDialogue.started -= ActivateDialogue;
        _controls.Profiler.AskMoonbeam.started -= AskMoonbeam;
        _controls.Profiler.Disable();
    }

    private void OnGUI()
    {
        _responseText.GetComponent<TextMeshProUGUI>().text = response;
    }

    private void ActivateDialogue(InputAction.CallbackContext obj)
    {
        if (!chatting && GetComponent<MoonbeamProfiler>().dialogueActive)
        {
            print("Dialogue Active");
            dialogueBoxes.SetActive(true);
            GetComponent<MoonbeamProfiler>().mDialogueBtn.SetActive(false);
            chatting = true;
        }
        else if (chatting)
        {
            print("Dialogue not Active");
            response = "";
            dialogueBoxes.SetActive(false);
            chatting = false;
        }
    }

    private void AskMoonbeam(InputAction.CallbackContext obj)
    {
        print("ask");
        if (askPrompt.activeInHierarchy && !asking)
        {
            print("Artifact Dialogue Active");
            dialogueBoxes.SetActive(true);
            artifactUI.SetActive(true);
            dOptions.SetActive(false);
            ChangeTextColor(whichDialogue);
            asking = true;
        }
        else if (asking)
        {
            response = "";
            dialogueBoxes.SetActive(false);
            artifactUI.SetActive(false);
            dOptions.SetActive(true);
            asking = false; 
        }
    }

    private void DialogueOptionOne(InputAction.CallbackContext obj)
    {
        if (chatting && !asking)
        {
            print("User says: " + dialogueOptions[0].GetComponent<TextMeshProUGUI>().text);
            whichDialogue = 1;
            ChangeTextColor(whichDialogue);
            StartCoroutine(_moonbeamAPI.GetComponent<MoonbeamAPI>().PostRequest(_uri));
        }
        else if (!chatting && asking)
        {
            print("User says: " + artifactQuestions[0].GetComponent<TextMeshProUGUI>().text);
            whichDialogue = 4;
            ChangeTextColor(whichDialogue);
            StartCoroutine(_moonbeamAPI.GetComponent<MoonbeamAPI>().PostRequest(_uri));
        }
    }

    private void DialogueOptionTwo(InputAction.CallbackContext obj)
    {
        if (chatting && !asking)
        {
            print("User says: " + dialogueOptions[1].GetComponent<TextMeshProUGUI>().text);
            whichDialogue = 2;
            ChangeTextColor(whichDialogue);
            StartCoroutine(_moonbeamAPI.GetComponent<MoonbeamAPI>().PostRequest(_uri));
        }
        else if (!chatting && asking)
        {
            print("User says: " + artifactQuestions[1].GetComponent<TextMeshProUGUI>().text);
            whichDialogue = 5;
            ChangeTextColor(whichDialogue);
            StartCoroutine(_moonbeamAPI.GetComponent<MoonbeamAPI>().PostRequest(_uri));
        }
    }

    private void DialogueOptionThree(InputAction.CallbackContext obj)
    {
        if (chatting && !asking)
        {
            print("User says: " + dialogueOptions[2].GetComponent<TextMeshProUGUI>().text);
            whichDialogue = 3;
            ChangeTextColor(whichDialogue);
            StartCoroutine(_moonbeamAPI.GetComponent<MoonbeamAPI>().PostRequest(_uri));
        }
        else if (!chatting && asking)
        {
            print("User says: " + artifactQuestions[2].GetComponent<TextMeshProUGUI>().text);
            whichDialogue = 6;
            ChangeTextColor(whichDialogue);
            StartCoroutine(_moonbeamAPI.GetComponent<MoonbeamAPI>().PostRequest(_uri));
        }
    }

    private void ChangeTextColor(int whichOne)
    {
        switch (whichOne)
        {
            case 1:
                dialogueOptions[0].GetComponent<TextMeshProUGUI>().color = new Color32(20, 255, 0, 225);
                dialogueOptions[1].GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
                dialogueOptions[2].GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
                break;
            case 2:
                dialogueOptions[0].GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
                dialogueOptions[1].GetComponent<TextMeshProUGUI>().color = new Color32(20, 255, 0, 225);
                dialogueOptions[2].GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
                break;
            case 3:
                dialogueOptions[0].GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
                dialogueOptions[1].GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
                dialogueOptions[2].GetComponent<TextMeshProUGUI>().color = new Color32(20, 255, 0, 225);
                break;
            case 4:
                artifactQuestions[0].GetComponent<TextMeshProUGUI>().color = new Color32(20, 255, 0, 225);
                artifactQuestions[1].GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
                artifactQuestions[2].GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
                break;
            case 5:
                artifactQuestions[0].GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
                artifactQuestions[1].GetComponent<TextMeshProUGUI>().color = new Color32(20, 255, 0, 225);
                artifactQuestions[2].GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
                break;
            case 6:
                artifactQuestions[0].GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
                artifactQuestions[1].GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 225);
                artifactQuestions[2].GetComponent<TextMeshProUGUI>().color = new Color32(20, 255, 0, 225);
                break;
        }
    }
}