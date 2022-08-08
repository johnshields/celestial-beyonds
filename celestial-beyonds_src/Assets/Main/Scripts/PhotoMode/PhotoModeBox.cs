using System;
using System.Collections;
using System.Globalization;
using System.IO;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * ref - https://gamedevplanet.com/how-to-take-a-screenshot-from-within-your-game-in-unity/
 */
public class PhotoModeBox : MonoBehaviour
{
    public GameObject photoModeUI;
    public float movementForce = 1f, fieldOfView = 80;
    public AudioClip shutter;
    public TextMeshProUGUI fieldOfViewInput;
    private Vector3 forceDir = Vector3.zero;
    private InputProfiler _controls;
    private InputAction _moveKeys, _moveController;

    private void Awake()
    {
        _controls = new InputProfiler();
    }

    private void FixedUpdate()
    {
        forceDir += GetCameraRight(GetComponent<Camera>()) * (_moveKeys.ReadValue<Vector2>().x * movementForce);
        forceDir += GetCameraForward(GetComponent<Camera>()) * (_moveKeys.ReadValue<Vector2>().y * movementForce);
        forceDir += GetCameraRight(GetComponent<Camera>()) *
                    (_moveController.ReadValue<Vector2>().x * movementForce);
        forceDir += GetCameraForward(GetComponent<Camera>()) *
                    (_moveController.ReadValue<Vector2>().y * movementForce);
        forceDir = Vector3.zero;

        GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = fieldOfView;
    }

    private void OnEnable()
    {
        _controls.Profiler.TakePhoto.started += TakePhoto;
        _controls.Profiler.FieldDepthUp.started += ZoomIn;
        _controls.Profiler.FieldDepthDown.started += ZoomOut;
        _moveKeys = _controls.Profiler.MoveKeys;
        _moveController = _controls.Profiler.MoveController;
        _controls.Profiler.Enable();
    }

    private void OnDisable()
    {
        _controls.Profiler.TakePhoto.started -= TakePhoto;
        _controls.Profiler.FieldDepthUp.started -= ZoomIn;
        _controls.Profiler.FieldDepthDown.started -= ZoomOut;
        _controls.Profiler.Disable();
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

    private void ZoomIn(InputAction.CallbackContext obj)
    {
        fieldOfView -= 5;
        if (fieldOfView < 10)
        {
            fieldOfView = 10f;
            print("fieldOfView is to low: " + fieldOfView);
        }
    }

    private void ZoomOut(InputAction.CallbackContext obj)
    {
        fieldOfView += 5;
        if (fieldOfView > 100f)
        {
            fieldOfView = 100f;
            print("fieldOfView is to high: " + fieldOfView);
        }
    }

    private void TakePhoto(InputAction.CallbackContext obj)
    {
        photoModeUI.SetActive(false);
        AudioSource.PlayClipAtPoint(shutter, transform.position, 0.8f);
        StartCoroutine(CamTimer());
    }

    // ref - https://stackoverflow.com/a/71082189
    private IEnumerator CamTimer()
    {
        yield return new WaitForSeconds(1);
        // Setup directory.
        const string folderPath = "/celestial-beyonds-photos/";
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);
        Guid.NewGuid();
        var photo_guid = Guid.NewGuid().ToString();
        // 25-07-2022_13-58-10
        var datetime_stamp = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        var photo_id = photo_guid + "__" + datetime_stamp + ".jpeg";
        // TakePhoto
        ScreenCapture.CaptureScreenshot(Path.Combine(folderPath, photo_id), 2);
        print("Screenshot taken: " + folderPath + photo_id);
        yield return new WaitForSeconds(1);
        photoModeUI.SetActive(true);
        CopyToClipBoard(datetime_stamp);
    }

    private void CopyToClipBoard(string id)
    {
        var textEditor = new TextEditor { text = id };
        textEditor.SelectAll();
        textEditor.Copy();
        print("Photo ID: Copied to clipboard!");
    }

    private void OnGUI()
    {
        var fov = fieldOfView.ToString(CultureInfo.CurrentCulture);
        fieldOfViewInput.text = fov;
    }
}