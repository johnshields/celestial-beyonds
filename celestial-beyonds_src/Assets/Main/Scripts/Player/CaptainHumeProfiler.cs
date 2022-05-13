using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CaptainHumeProfiler : MonoBehaviour
{
    // input
    private InputProfiler _controls;
    private InputAction _moveKeys, _moveController;
    
    // movement
    private Rigidbody _rb;
    public float movementForce = 1f, jumpForce = 5f, maxSpeed = 5f;
    private Vector3 forceDir = Vector3.zero;

    public Camera playerCam;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _controls = new InputProfiler();
    }

    private void OnEnable()
    {
        _controls.Profiler.Jump.started += JumpActive;
        _moveKeys = _controls.Profiler.MoveKeys;
        _moveController = _controls.Profiler.MoveController;
        _controls.Profiler.Enable();
    }
    
    private void OnDisable()
    {
        _controls.Profiler.Jump.started -= JumpActive;
        _controls.Profiler.Disable();
    }

    private void FixedUpdate()
    {
        forceDir += _moveKeys.ReadValue<Vector2>().x * GetCameraRight(playerCam) * movementForce;
        forceDir += _moveKeys.ReadValue<Vector2>().y * GetCameraForward(playerCam) * movementForce;
        forceDir += _moveController.ReadValue<Vector2>().x * GetCameraRight(playerCam) * movementForce;
        forceDir += _moveController.ReadValue<Vector2>().y * GetCameraForward(playerCam) * movementForce;
        
        _rb.AddForce(forceDir, ForceMode.Impulse);
        forceDir = Vector3.zero;

        if (_rb.velocity.y < 0f)
            _rb.velocity += Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

        var hVelocity = _rb.velocity;
        hVelocity.y = 0;
        if (hVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            _rb.velocity = hVelocity.normalized * maxSpeed + Vector3.up * _rb.velocity.y;
        
        LookAt();
    }

    private void LookAt()
    {
        var direction = _rb.velocity;
        direction.y = 0f;

        if (_moveKeys.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            _rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else if(_moveController.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            _rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            _rb.angularVelocity = Vector3.zero;
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

    private void JumpActive(InputAction.CallbackContext obj)
    {
        if (Grounded())
        {
            forceDir += Vector3.up * jumpForce;
        }
    }

    private bool Grounded()
    {
        var ray = new Ray(transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out _, 0.3f))
            return true;
        else
            return false;
    }
}
