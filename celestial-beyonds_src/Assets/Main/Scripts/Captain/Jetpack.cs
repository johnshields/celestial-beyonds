using System.Collections;
using Main.Scripts.Captain;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/*
 * https://youtu.be/8j_NhBVYz8o
 */
public class Jetpack : MonoBehaviour
{
    public bool jetpackActive;
    public float maxFuel = 4f, thrustForce = 0.3f, currentFuel, resetSFXTime = 4f;
    public Transform groundedObj;
    public GameObject flames, fuelBar;
    public AudioClip jetpackSFX;
    public AudioSource _jpAudio;
    private Rigidbody _rb;
    private bool _alreadyPlayed;
    private InputProfiler _controls;
    private Slider _fuelBarSlider;

    private void Awake()
    {
        _controls = new InputProfiler();
    }

    private void Start()
    {
        currentFuel = maxFuel;
        _rb = GetComponent<Rigidbody>();
        _fuelBarSlider = fuelBar.GetComponent<Slider>();
    }

    private void Update()
    {
        if (!GetComponent<CaptainAnimAndSound>().pauseMenu.GetComponent<InGameMenus>().pausedActive)
        {
            _fuelBarSlider.value = currentFuel;
            if (jetpackActive && currentFuel > 0f)
            {
                GetComponent<CaptainProfiler>().grounded = false;
                jetpackActive = true;
                currentFuel -= Time.deltaTime;
                _rb.AddForce(_rb.transform.up * thrustForce, ForceMode.Impulse);
            }
            else if (Physics.Raycast(groundedObj.position, Vector3.down, 0.01f,
                         LayerMask.GetMask("GroundedObject")) && currentFuel < maxFuel)
            {
                jetpackActive = false;
                if (currentFuel < maxFuel)
                    currentFuel += Time.deltaTime;
            }
            else
            {
                jetpackActive = false;
                if (currentFuel < maxFuel)
                    currentFuel += Time.deltaTime;
            }
            flames.SetActive(jetpackActive);
            if (!jetpackActive)
                _jpAudio.Stop();   
        }
        else jetpackActive = false;
    }

    private void OnEnable()
    {
        _controls.Profiler.JetPack.started += JetpackActive;
        _controls.Profiler.JetPack.Enable();
    }

    private void OnDisable()
    {
        _controls.Profiler.JetPack.started -= JetpackActive;
        _controls.Profiler.JetPack.Disable();
    }

    private void JetpackActive(InputAction.CallbackContext obj)
    {
        if (!jetpackActive && !_alreadyPlayed)
        {
            jetpackActive = true;
            _jpAudio.PlayOneShot(jetpackSFX, 0.1f);
            _alreadyPlayed = true;
            StartCoroutine(ResetAudio());
        }
        else if (jetpackActive)
        {
            jetpackActive = false;
            resetSFXTime = 0f;
        }
    }

    private IEnumerator ResetAudio()
    {
        yield return new WaitForSeconds(resetSFXTime);
        _alreadyPlayed = false;
    }
}