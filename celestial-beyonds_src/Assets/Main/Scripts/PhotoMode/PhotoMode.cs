using System;
using System.Collections;
using System.Globalization;
using System.IO;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PhotoMode : MonoBehaviour
{
    public GameObject mainCam, pauseMenu, photoModeUI, UI;
    public float movementForce = 1f, fieldOfView = 80;
    public bool photoMode, pmEnabledInScene;
    public string photoID;
    public AudioClip shutter;
    public TextMeshProUGUI fieldOfViewInput;
    public Text photoIDTxt;
    private Vector3 forceDir = Vector3.zero;
    private InputProfiler _controls;
    private InputAction _moveKeys, _moveController;

    private void Awake()
    {
        _controls = new InputProfiler();
        photoMode = false;
        if(pmEnabledInScene)
            SwitchCam(true, false);
    }

    private void FixedUpdate()
    {
        if (photoMode)
        {
            forceDir += GetCameraRight(GetComponent<Camera>()) * (_moveKeys.ReadValue<Vector2>().x * movementForce);
            forceDir += GetCameraForward(GetComponent<Camera>()) * (_moveKeys.ReadValue<Vector2>().y * movementForce);
            forceDir += GetCameraRight(GetComponent<Camera>()) *
                        (_moveController.ReadValue<Vector2>().x * movementForce);
            forceDir += GetCameraForward(GetComponent<Camera>()) *
                        (_moveController.ReadValue<Vector2>().y * movementForce);
            forceDir = Vector3.zero;
        }

        GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = fieldOfView;
    }

    private void OnEnable()
    {
        _controls.Profiler.TurnOnPhotoMode.started += TurnOnPhotoMode;
        _controls.Profiler.TakePhoto.started += TakePhoto;
        _controls.Profiler.FieldDepthUp.started += ZoomIn;
        _controls.Profiler.FieldDepthDown.started += ZoomOut;
        _moveKeys = _controls.Profiler.MoveKeys;
        _moveController = _controls.Profiler.MoveController;
        _controls.Profiler.Enable();
    }

    private void OnDisable()
    {
        _controls.Profiler.TurnOnPhotoMode.started -= TurnOnPhotoMode;
        _controls.Profiler.TakePhoto.started -= TakePhoto;
        _controls.Profiler.FieldDepthUp.started -= ZoomIn;
        _controls.Profiler.FieldDepthDown.started -= ZoomOut;
        _controls.Profiler.Disable();
    }

    private void TurnOnPhotoMode(InputAction.CallbackContext obj)
    {
        if (!photoMode && pauseMenu.GetComponent<InGameMenus>().pausedActive && pmEnabledInScene)
        {
            photoMode = true;
            print("PhotoMode Active: " + photoMode);
            SwitchCam(false, true);
            photoModeUI.SetActive(true);
            UI.SetActive(false);
        }
        else if (photoMode)
        {
            photoMode = false;
            print("PhotoMode Active: " + photoMode);
            SwitchCam(true, false);
            photoModeUI.SetActive(false);
            UI.SetActive(true);
        }
    }

    private Vector3 GetCameraForward(Component cam)
    {
        var forward = cam.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Component cam)
    {
        var right = cam.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void SwitchCam(bool main, bool photo)
    {
        mainCam.GetComponent<CinemachineBrain>().enabled = main;
        mainCam.GetComponent<CinemachineFreeLook>().enabled = main;
        mainCam.GetComponent<CinemachineInputProvider>().enabled = main;
        mainCam.GetComponent<Camera>().enabled = main;
        GetComponent<CinemachineBrain>().enabled = photo;
        GetComponent<CinemachineFreeLook>().enabled = photo;
        GetComponent<CinemachineInputProvider>().enabled = photo;
        GetComponent<Camera>().enabled = photo;
    }

    private void ZoomIn(InputAction.CallbackContext obj)
    {
        if (photoMode)
            fieldOfView -= 5;
        if (fieldOfView < 10)
        {
            fieldOfView = 10f;
            print("fieldOfView is to low: " + fieldOfView);
        }
    }

    private void ZoomOut(InputAction.CallbackContext obj)
    {
        if (photoMode)
            fieldOfView += 5;
        if (fieldOfView > 100f)
        {
            fieldOfView = 100f;
            print("fieldOfView is to high: " + fieldOfView);
        }
    }

    private void OnGUI()
    {
        if (photoMode)
        {
            var fov = fieldOfView.ToString(CultureInfo.CurrentCulture);
            fieldOfViewInput.text = fov;
            photoIDTxt.text = "Photo ID: " + photoID;
        }
    }


    // ref - https://stackoverflow.com/a/71082189
    private void TakePhoto(InputAction.CallbackContext obj)
    {
        if (photoMode)
        {
            photoModeUI.SetActive(false);
            AudioSource.PlayClipAtPoint(shutter, transform.position, 0.8f);
            StartCoroutine(CamTimer());
        }
    }

    private IEnumerator CamTimer()
    {
        yield return new WaitForSeconds(1);
        // TakePhoto
        const string folderPath = "Assets/Screenshots/"; // TODO - update to DB.
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);
        Guid.NewGuid();
        var photo_guid = Guid.NewGuid().ToString();
        var datetime_stamp = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        var photo_id = "tcb-photo" + "__" + datetime_stamp + "__" + photo_guid + ".jpeg";
        ScreenCapture.CaptureScreenshot(Path.Combine(folderPath, photo_id), 4);
        print("Screenshot taken: " + folderPath + photo_id);
        photoID = photo_id;
        yield return new WaitForSeconds(1);
        photoModeUI.SetActive(true);
    }
}