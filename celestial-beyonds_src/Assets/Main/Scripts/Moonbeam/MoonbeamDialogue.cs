using System.Collections;
using Main.Scripts.Moonbeam;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoonbeamDialogue : MonoBehaviour
{
    private const string _uri = "https://api.moonbeambot.live/api/chat";
    private GameObject _responseText, _moonbeamAPI, _pl;
    private InputProfiler _controls;
    public GameObject dialogueBoxes, dOptions, askPrompt, pauseMenu, dialogueColor;
    public GameObject[] aQsHolder, artifactQuestions, dialogueOptions;
    public string[] gOptsOne, gOptsTwo, openingOpts, treeOpts;
    public string response;
    public int whichDialogue, artifactNum;
    public bool chatting, asking, generalQ, artifactQ;
    private bool _changed;

    private void Awake()
    {
        response = "";
        _controls = new InputProfiler();
        _responseText = GameObject.FindGameObjectWithTag("ResponseText");
        _moonbeamAPI = GameObject.FindGameObjectWithTag("MoonbeamAPI");
        _pl = GameObject.FindGameObjectWithTag("PollinationLevel");
    }

    private void OnEnable()
    {
        _controls.Profiler.DialogueOptionOne.started += DialogueOptionOne;
        _controls.Profiler.DialogueOptionTwo.started += DialogueOptionTwo;
        _controls.Profiler.DialogueOptionThree.started += DialogueOptionThree;
        _controls.Profiler.ActivateDialogue.started += ActivateDialogue;
        _controls.Profiler.CloseDialogue.started += CloseDialogue;
        _controls.Profiler.AskMoonbeam.started += AskMoonbeam;
        _controls.Profiler.Enable();
    }

    private void OnDisable()
    {
        _controls.Profiler.DialogueOptionOne.started -= DialogueOptionOne;
        _controls.Profiler.DialogueOptionTwo.started -= DialogueOptionTwo;
        _controls.Profiler.DialogueOptionThree.started -= DialogueOptionThree;
        _controls.Profiler.ActivateDialogue.started -= ActivateDialogue;
        _controls.Profiler.CloseDialogue.started -= CloseDialogue;
        _controls.Profiler.AskMoonbeam.started -= AskMoonbeam;
        _controls.Profiler.Disable();
    }

    private void OptsRandomizer()
    {
        // Cap asks Moonbeam e.g - "Hello", "How are you?"
        dialogueOptions[0].GetComponent<TextMeshProUGUI>().text = openingOpts[Random.Range(0, openingOpts.Length)];

        // General
        dialogueOptions[1].GetComponent<TextMeshProUGUI>().text = gOptsOne[Random.Range(0, gOptsOne.Length)];
        dialogueOptions[2].GetComponent<TextMeshProUGUI>().text = gOptsTwo[Random.Range(0, gOptsTwo.Length)];
    }

    // Key = T
    private void ActivateDialogue(InputAction.CallbackContext obj)
    {
        if (!pauseMenu.GetComponent<InGameMenus>().pausedActive)
        {
            // General Dialogue
            if (!chatting && GetComponent<MoonbeamProfiler>().dialogueActive)
            {
                OptsRandomizer();
                dialogueBoxes.SetActive(true);
                chatting = true;
                _changed = false;
            }
        }
    }

    // Key = I
    private void AskMoonbeam(InputAction.CallbackContext obj)
    {
        if (!pauseMenu.GetComponent<InGameMenus>().pausedActive)
        {
            if (!asking && askPrompt.activeInHierarchy) // open
                AskMoonbeamHelper(true, true, false, true);
        }
    }


    private void AskMoonbeamHelper(bool db, bool aUI, bool dOpts, bool a)
    {
        print(artifactNum);
        aQsHolder[artifactNum].SetActive(aUI);

        dialogueBoxes.SetActive(db);
        dOptions.SetActive(dOpts);
        asking = a;
        print("Artifact Dialogue Active: " + a);
    }

    // Key = Q
    private void CloseDialogue(InputAction.CallbackContext obj)
    {
        if (!pauseMenu.GetComponent<InGameMenus>().pausedActive)
        {
            if (chatting) // quit Chat
            {
                response = "";
                dialogueBoxes.SetActive(false);
                chatting = false;
            }
            else if (asking) // quit Ask
            {
                response = "";
                AskMoonbeamHelper(false, false, true, false);
            }
        }
    }

    private void DialogueOptionOne(InputAction.CallbackContext obj)
    {
        if (!pauseMenu.GetComponent<InGameMenus>().pausedActive)
        {
            if (chatting && !asking)
            {
                GeneralOrArtifactQ(true, false);
                ChangeDialogue(dialogueOptions[0].GetComponent<TextMeshProUGUI>().text, 100);
                dialogueColor.GetComponent<DialogueColorChanger>().ChangeTextColor(100);
            }
            else if (!chatting && asking)
            {
                GeneralOrArtifactQ(false, true);
                ArtifactQOnes();
                dialogueColor.GetComponent<DialogueColorChanger>().ChangeTextColor(whichDialogue);
            }
        }
    }

    private void DialogueOptionTwo(InputAction.CallbackContext obj)
    {
        if (!pauseMenu.GetComponent<InGameMenus>().pausedActive)
        {
            if (chatting && !asking)
            {
                GeneralOrArtifactQ(true, false);
                ChangeDialogue(dialogueOptions[1].GetComponent<TextMeshProUGUI>().text, 101);
                dialogueColor.GetComponent<DialogueColorChanger>().ChangeTextColor(101);
            }
            else if (!chatting && asking)
            {
                GeneralOrArtifactQ(false, true);
                ArtifactQTwos();
                dialogueColor.GetComponent<DialogueColorChanger>().ChangeTextColor(whichDialogue);
            }
        }
    }

    private void DialogueOptionThree(InputAction.CallbackContext obj)
    {
        if (!pauseMenu.GetComponent<InGameMenus>().pausedActive)
        {
            if (chatting && !asking)
            {
                GeneralOrArtifactQ(true, false);
                ChangeDialogue(dialogueOptions[2].GetComponent<TextMeshProUGUI>().text, 102);
                dialogueColor.GetComponent<DialogueColorChanger>().ChangeTextColor(102);
                if (_pl.GetComponent<PollinationLevel>().lineChanged)
                    _pl.GetComponent<PollinationLevel>().lvlCompLine = true;
            }
            else if (!chatting && asking)
            {
                GeneralOrArtifactQ(false, true);
                ArtifactQThrees();
                dialogueColor.GetComponent<DialogueColorChanger>().ChangeTextColor(whichDialogue);
            }
        }
    }

    private void GeneralOrArtifactQ(bool g, bool a)
    {
        generalQ = g;
        artifactQ = a;
    }

    private void OnGUI()
    {
        _responseText.GetComponent<TextMeshProUGUI>().text = response;

        if (_moonbeamAPI.GetComponent<MoonbeamAPI>().itIsAQuestion && !_changed)
            StartCoroutine(ChangeOptOnGUI());
    }

    private IEnumerator ChangeOptOnGUI()
    {
        _changed = true;
        yield return new WaitForSeconds(1.2f);
        // Cap's feeling e.g. - "I'm good."
        dialogueOptions[0].GetComponent<TextMeshProUGUI>().text = treeOpts[Random.Range(0, treeOpts.Length)];
    }

    private void ArtifactQOnes()
    {
        // different questions for different Artifacts.
        if (aQsHolder[0].activeInHierarchy) ArtifactQuestion(0);
        else if (aQsHolder[1].activeInHierarchy) ArtifactQuestion(3);
        else if (aQsHolder[2].activeInHierarchy) ArtifactQuestion(6);
        else if (aQsHolder[3].activeInHierarchy) ArtifactQuestion(9);
        else if (aQsHolder[4].activeInHierarchy) ArtifactQuestion(12);
        else if (aQsHolder[5].activeInHierarchy) ArtifactQuestion(15);
        else if (aQsHolder[6].activeInHierarchy) ArtifactQuestion(18);
        else if (aQsHolder[7].activeInHierarchy) ArtifactQuestion(21);
        else if (aQsHolder[8].activeInHierarchy) ArtifactQuestion(24);
        else if (aQsHolder[9].activeInHierarchy) ArtifactQuestion(27);
    }

    private void ArtifactQTwos()
    {
        if (aQsHolder[0].activeInHierarchy) ArtifactQuestion(1);
        else if (aQsHolder[1].activeInHierarchy) ArtifactQuestion(4);
        else if (aQsHolder[2].activeInHierarchy) ArtifactQuestion(7);
        else if (aQsHolder[3].activeInHierarchy) ArtifactQuestion(10);
        else if (aQsHolder[4].activeInHierarchy) ArtifactQuestion(13);
        else if (aQsHolder[5].activeInHierarchy) ArtifactQuestion(16);
        else if (aQsHolder[6].activeInHierarchy) ArtifactQuestion(19);
        else if (aQsHolder[7].activeInHierarchy) ArtifactQuestion(22);
        else if (aQsHolder[8].activeInHierarchy) ArtifactQuestion(25);
        else if (aQsHolder[9].activeInHierarchy) ArtifactQuestion(28);
    }

    private void ArtifactQThrees()
    {
        if (aQsHolder[0].activeInHierarchy) ArtifactQuestion(2);
        else if (aQsHolder[1].activeInHierarchy) ArtifactQuestion(5);
        else if (aQsHolder[2].activeInHierarchy) ArtifactQuestion(8);
        else if (aQsHolder[3].activeInHierarchy) ArtifactQuestion(11);
        else if (aQsHolder[4].activeInHierarchy) ArtifactQuestion(14);
        else if (aQsHolder[5].activeInHierarchy) ArtifactQuestion(17);
        else if (aQsHolder[6].activeInHierarchy) ArtifactQuestion(20);
        else if (aQsHolder[7].activeInHierarchy) ArtifactQuestion(23);
        else if (aQsHolder[8].activeInHierarchy) ArtifactQuestion(26);
        else if (aQsHolder[9].activeInHierarchy) ArtifactQuestion(29);
    }

    private void ArtifactQuestion(int q)
    {
        if (q == 0) ChangeDialogue(artifactQuestions[0].GetComponent<TextMeshProUGUI>().text, 0);
        else if (q == 1) ChangeDialogue(artifactQuestions[1].GetComponent<TextMeshProUGUI>().text, 1);
        else if (q == 2) ChangeDialogue(artifactQuestions[2].GetComponent<TextMeshProUGUI>().text, 2);
        else if (q == 3) ChangeDialogue(artifactQuestions[3].GetComponent<TextMeshProUGUI>().text, 3);
        else if (q == 4) ChangeDialogue(artifactQuestions[4].GetComponent<TextMeshProUGUI>().text, 4);
        else if (q == 5) ChangeDialogue(artifactQuestions[5].GetComponent<TextMeshProUGUI>().text, 5);
        else if (q == 6) ChangeDialogue(artifactQuestions[6].GetComponent<TextMeshProUGUI>().text, 6);
        else if (q == 7) ChangeDialogue(artifactQuestions[7].GetComponent<TextMeshProUGUI>().text, 7);
        else if (q == 8) ChangeDialogue(artifactQuestions[8].GetComponent<TextMeshProUGUI>().text, 8);
        else if (q == 9) ChangeDialogue(artifactQuestions[9].GetComponent<TextMeshProUGUI>().text, 9);
        else if (q == 10) ChangeDialogue(artifactQuestions[10].GetComponent<TextMeshProUGUI>().text, 10);
        else if (q == 11) ChangeDialogue(artifactQuestions[11].GetComponent<TextMeshProUGUI>().text, 11);
        else if (q == 12) ChangeDialogue(artifactQuestions[12].GetComponent<TextMeshProUGUI>().text, 12);
        else if (q == 13) ChangeDialogue(artifactQuestions[13].GetComponent<TextMeshProUGUI>().text, 13);
        else if (q == 14) ChangeDialogue(artifactQuestions[14].GetComponent<TextMeshProUGUI>().text, 14);
        else if (q == 15) ChangeDialogue(artifactQuestions[15].GetComponent<TextMeshProUGUI>().text, 15);
        else if (q == 16) ChangeDialogue(artifactQuestions[16].GetComponent<TextMeshProUGUI>().text, 16);
        else if (q == 17) ChangeDialogue(artifactQuestions[17].GetComponent<TextMeshProUGUI>().text, 17);
        else if (q == 18) ChangeDialogue(artifactQuestions[18].GetComponent<TextMeshProUGUI>().text, 18);
        else if (q == 19) ChangeDialogue(artifactQuestions[19].GetComponent<TextMeshProUGUI>().text, 19);
        else if (q == 20) ChangeDialogue(artifactQuestions[20].GetComponent<TextMeshProUGUI>().text, 20);
        else if (q == 21) ChangeDialogue(artifactQuestions[21].GetComponent<TextMeshProUGUI>().text, 21);
        else if (q == 22) ChangeDialogue(artifactQuestions[22].GetComponent<TextMeshProUGUI>().text, 22);
        else if (q == 23) ChangeDialogue(artifactQuestions[23].GetComponent<TextMeshProUGUI>().text, 23);
        else if (q == 24) ChangeDialogue(artifactQuestions[24].GetComponent<TextMeshProUGUI>().text, 24);
        else if (q == 25) ChangeDialogue(artifactQuestions[25].GetComponent<TextMeshProUGUI>().text, 25);
        else if (q == 26) ChangeDialogue(artifactQuestions[26].GetComponent<TextMeshProUGUI>().text, 26);
        else if (q == 27) ChangeDialogue(artifactQuestions[27].GetComponent<TextMeshProUGUI>().text, 27);
        else if (q == 28) ChangeDialogue(artifactQuestions[28].GetComponent<TextMeshProUGUI>().text, 28);
        else if (q == 29) ChangeDialogue(artifactQuestions[29].GetComponent<TextMeshProUGUI>().text, 29);
    }

    private void ChangeDialogue(string userInput, int dNum)
    {
        whichDialogue = dNum;
        print("User says: " + userInput + "dNum: " + dNum);
        StartCoroutine(_moonbeamAPI.GetComponent<MoonbeamAPI>().PostRequest(_uri));
    }
}