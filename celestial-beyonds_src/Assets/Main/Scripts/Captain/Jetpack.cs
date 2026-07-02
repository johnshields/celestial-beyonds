using Main.Scripts.Captain;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour
{
    public bool jetpackActive;
    public float maxFuel = 4f, currentFuel;
    public GameObject flames, fuelBar;
    public AudioClip jetpackSFX;
    public AudioSource _jpAudio;

    [SerializeField] private float thrustForce = 48f;
    [SerializeField] private float maxRiseSpeed = 4f;
    [SerializeField] private float fallMultiplier = 2.5f;

    // Minimum fuel before the pack can fire again, stops empty-tank stutter spam.
    private const float _minActivationFuel = 0.5f;

    private bool _fastFall;
    private Rigidbody _rb;
    private CaptainProfiler _profiler;
    private InGameMenus _menus;
    private InputProfiler _controls;
    private Slider _fuelBarSlider;

    private void Awake()
    {
        _controls = new InputProfiler();
    }

    private void Start()
    {
        currentFuel = maxFuel;
        flames.SetActive(false);
        _rb = GetComponent<Rigidbody>();
        _profiler = GetComponent<CaptainProfiler>();
        _menus = GetComponent<CaptainAnimAndSound>().pauseMenu.GetComponent<InGameMenus>();
        _fuelBarSlider = fuelBar.GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _controls.Profiler.JetPack.started += ToggleJetpack;
        _controls.Profiler.JetPack.Enable();
    }

    private void OnDisable()
    {
        _controls.Profiler.JetPack.started -= ToggleJetpack;
        _controls.Profiler.JetPack.Disable();
    }

    private void Update()
    {
        if (_menus.pausedActive)
        {
            Deactivate();
            return;
        }

        if (jetpackActive)
        {
            currentFuel -= Time.deltaTime;
            if (currentFuel <= 0f)
            {
                currentFuel = 0f;
                Deactivate();
            }
        }
        else if (currentFuel < maxFuel)
        {
            currentFuel = Mathf.Min(currentFuel + Time.deltaTime, maxFuel);
        }

        _fuelBarSlider.value = currentFuel;
    }

    private void FixedUpdate()
    {
        var up = transform.up;
        var riseSpeed = Vector3.Dot(_rb.linearVelocity, up);

        if (jetpackActive)
        {
            _profiler.grounded = false;

            if (riseSpeed < maxRiseSpeed)
                _rb.AddForce(up * thrustForce, ForceMode.Force);
            else
                _rb.linearVelocity += up * (maxRiseSpeed - riseSpeed);
            return;
        }

        if (_fastFall && _profiler.grounded)
            _fastFall = false;

        if (_fastFall && riseSpeed < 0f)
            _rb.AddForce(-up * (fallMultiplier * -Physics.gravity.y), ForceMode.Acceleration);
    }

    private void ToggleJetpack(InputAction.CallbackContext obj)
    {
        if (jetpackActive)
        {
            Deactivate();
            return;
        }

        if (currentFuel < _minActivationFuel) return;

        jetpackActive = true;
        _fastFall = false;
        flames.SetActive(true);
        _jpAudio.PlayOneShot(jetpackSFX, 0.1f);
    }

    private void Deactivate()
    {
        if (!jetpackActive) return;
        jetpackActive = false;
        _fastFall = !_profiler.grounded;
        flames.SetActive(false);
        _jpAudio.Stop();
    }
}
