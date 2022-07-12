using System.Collections;
using Main.Scripts.Captain;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour
{
    public static bool _jetpackActive;
    public float maxFuel = 4f, thrustForce = 0.3f;
    public Rigidbody _rb;
    public Transform groundedObj;
    public GameObject flames, fuelBar;
    public float _currentFuel;
    public AudioClip jetpackSFX;
    public AudioSource _jpAudio;
    private bool _alreadyPlayed;
    private InputProfiler _controls;
    private Slider _fuelBarSlider;

    private void Awake()
    {
        _controls = new InputProfiler();
    }

    private void Start()
    {
        _currentFuel = maxFuel;
        _rb = GetComponent<Rigidbody>();
        _fuelBarSlider = fuelBar.GetComponent<Slider>();
    }

    private void Update()
    {
        _fuelBarSlider.value = _currentFuel;
        if (_jetpackActive && _currentFuel > 0f)
        {
            _jetpackActive = true;
            _currentFuel -= Time.deltaTime;
            _rb.AddForce(_rb.transform.up * thrustForce, ForceMode.Impulse);
        }
        else if (Physics.Raycast(groundedObj.position, Vector3.down, 0.01f,
                     LayerMask.GetMask("GroundedObject")) && _currentFuel < maxFuel)
        {
            _jetpackActive = false;
            if (_currentFuel < maxFuel)
                _currentFuel += Time.deltaTime;
        }
        else
        {
            _jetpackActive = false;
            if (_currentFuel < maxFuel)
                _currentFuel += Time.deltaTime;
        }

        flames.SetActive(_jetpackActive);

        if (!_jetpackActive)
            _jpAudio.Stop();
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
        _jetpackActive = true;
        if (!_alreadyPlayed)
        {
            _jpAudio.PlayOneShot(jetpackSFX, 0.1f);
            _alreadyPlayed = true;
            StartCoroutine(ResetAudio());
        }
    }

    private IEnumerator ResetAudio()
    {
        yield return new WaitForSeconds(4f);
        _alreadyPlayed = false;
    }
}