using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class SyncCinematic : MonoBehaviour
{
    public GameObject musicAudio, timeline, continueBtn, mainTitle;
    private InputProfiler _controls;
    private bool _played;

    private void Awake()
    {
        _controls = new InputProfiler();
        StartCoroutine(ActivateContinueBtn());
    }
    
    private void OnEnable()
    {
        _controls.UIActions.PlayCinematic.started += PlayCinematic;
        _controls.UIActions.Enable();
    }

    private void OnDisable()
    {
        _controls.UIActions.PlayCinematic.started -= PlayCinematic;
        _controls.UIActions.Disable();
    }

    private void PlayCinematic(InputAction.CallbackContext obj)
    {
        if(!_played)
            SyncCinAndMusic();
    }

    private void SyncCinAndMusic()
    {
        _played = true;
        continueBtn.SetActive(false);
        timeline.GetComponent<PlayableDirector>().Play();
        musicAudio.GetComponent<AudioSource>().Play();
        mainTitle.SetActive(true);
    }

    private IEnumerator ActivateContinueBtn()
    {
        yield return new WaitForSeconds(2f);
        continueBtn.SetActive(true);
    }
}