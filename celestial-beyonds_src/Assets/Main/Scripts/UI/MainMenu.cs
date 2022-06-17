using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private UIActionsProfiler _controls;
    public GameObject _fader;
    
    private void Awake()
    {
        _controls = new UIActionsProfiler();
    }

    private void Start()
    {
        _fader.GetComponent<Animator>().SetBool("FadeIn", true);
        _fader.GetComponent<Animator>().SetBool("FadeOut", false);
    }

    private void OnEnable()
    {
        _controls.UIActions.StartGame.started += StartGame;
        _controls.UIActions.StartGame.Enable();
    }

    private void OnDisable()
    {
        _controls.UIActions.StartGame.started -= StartGame;
        _controls.UIActions.StartGame.Disable();
    }
    
    private void StartGame(InputAction.CallbackContext obj)
    {
        StartCoroutine(LaunchGame());
    }
    
    private IEnumerator LaunchGame()
    {
        _fader.GetComponent<Animator>().SetBool("FadeIn", false);
        _fader.GetComponent<Animator>().SetBool("FadeOut", true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
