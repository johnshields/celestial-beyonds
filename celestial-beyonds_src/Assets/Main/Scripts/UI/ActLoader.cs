using System.Collections;
using Main.Scripts.UI.CursorControls;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActLoader : MonoBehaviour
{
    public GameObject actPanel, fader;
    private GameObject _cursor;

    private void Awake()
    {
        _cursor = GameObject.FindGameObjectWithTag("Cursor");
    }

    private void Update()
    {
        // Sub Buttons - Close
        if (_cursor.GetComponent<ControllerCursor>().clickedElement == "ActOne")
            LoadActOne();
        else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "ActTwo")
            LoadActTwo();
        else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "ActThree")
            LoadActThree();
        else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "Endgame")
            LoadEndgame();
        else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "Epilogue")
            LoadEpilogue();
    }

    public void LoadActOne()
    {
        _cursor.GetComponent<ControllerCursor>().clickedElement = string.Empty;
        if (actPanel.activeInHierarchy)
            StartCoroutine(LaunchAct("003_CelestialWaltz"));
    }

    public void LoadActTwo()
    {
        _cursor.GetComponent<ControllerCursor>().clickedElement = string.Empty;
        if (actPanel.activeInHierarchy)
            StartCoroutine(LaunchAct("005_LunarPulse"));
    }
    
    public void LoadActThree()
    {
        _cursor.GetComponent<ControllerCursor>().clickedElement = string.Empty;
        if (actPanel.activeInHierarchy)
            StartCoroutine(LaunchAct("007_Man_of_Celestial_Man_of_Faith"));
    }
    
    public void LoadEndgame()
    {
        _cursor.GetComponent<ControllerCursor>().clickedElement = string.Empty;
        if (actPanel.activeInHierarchy)
            StartCoroutine(LaunchAct("009_Intro_Endgame"));
    }
    
    public void LoadEpilogue()
    {
        _cursor.GetComponent<ControllerCursor>().clickedElement = string.Empty;
        if (actPanel.activeInHierarchy)
            StartCoroutine(LaunchAct("010_TheGoldenRecord"));
    }

    private IEnumerator LaunchAct(string level)
    {
        actPanel.SetActive(false);
        fader.SetActive(true);
        print("Loading into: " + level);
        fader.GetComponent<Animator>().SetBool($"FadeIn", false);
        fader.GetComponent<Animator>().SetBool($"FadeOut", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    }
}