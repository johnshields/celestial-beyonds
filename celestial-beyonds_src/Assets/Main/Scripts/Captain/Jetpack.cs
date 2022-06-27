using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

public class Jetpack : MonoBehaviour
{
    public float maxFuel = 4f, thrustForce = 0.1f;
    public Rigidbody _rb;
    public Transform groundedObj;
    public GameObject flames;
    private float _currentFuel;
    private InputProfiler _controls;
    public static bool _jetpackActive;

    private void Awake()
    {
        _controls = new InputProfiler();
    }

    private void Start()
    {
        _currentFuel = maxFuel;
        _rb = GetComponent<Rigidbody>();
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
    }

    private void Update()
    {
        print("jetpack active: " + _jetpackActive + "\n fuel: " + _currentFuel);
        
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
    }
}
