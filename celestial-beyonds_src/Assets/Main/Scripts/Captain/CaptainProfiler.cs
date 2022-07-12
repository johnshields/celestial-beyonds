using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Main.Scripts.Captain
{
    public class CaptainProfiler : MonoBehaviour
    {
        public static bool grounded;
        public float movementForce = 1f, jumpForce = 5f, maxSpeed = 5f;

        public Camera playerCam;

        // input
        private InputProfiler _controls;
        private InputAction _moveKeys, _moveController;

        // movement
        private Rigidbody _rb;
        private Vector3 forceDir = Vector3.zero;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _controls = new InputProfiler();
        }

        private void FixedUpdate()
        {
            forceDir += GetCameraRight(playerCam) * (_moveKeys.ReadValue<Vector2>().x * movementForce);
            forceDir += GetCameraForward(playerCam) * (_moveKeys.ReadValue<Vector2>().y * movementForce);
            forceDir += GetCameraRight(playerCam) * (_moveController.ReadValue<Vector2>().x * movementForce);
            forceDir += GetCameraForward(playerCam) * (_moveController.ReadValue<Vector2>().y * movementForce);

            _rb.AddForce(forceDir, ForceMode.Impulse);
            forceDir = Vector3.zero;

            LookAt();
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

        private void OnCollisionEnter()
        {
            grounded = true;
        }

        private void LookAt()
        {
            var direction = _rb.velocity;
            direction.y = 0f;

            if (_moveKeys.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
                _rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
            else if (_moveController.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
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
            if (!grounded) return;
            StartCoroutine(DoAction());
            grounded = false;
        }

        private IEnumerator DoAction()
        {
            yield return new WaitForSeconds(0.5f);
            _rb.velocity = transform.TransformDirection(0, jumpForce, 0f);
        }
    }
}