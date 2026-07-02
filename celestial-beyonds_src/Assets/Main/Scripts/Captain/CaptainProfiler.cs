using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * ref - https://youtu.be/WIl6ysorTE0
 */
namespace Main.Scripts.Captain
{
    public class CaptainProfiler : MonoBehaviour
    {
        public bool grounded;
        public float movementForce = 1f, jumpForce = 5f;

        public Camera playerCam;

        [SerializeField] private float jumpDelay = 0.25f;
        [SerializeField] private float jumpRelockTime = 0.2f;

        // Length of the jump clip windup; playback is compressed so takeoff lands on jumpDelay.
        private const float _jumpWindup = 0.5f;

        // input
        private InputProfiler _controls;
        private InputAction _moveKeys, _moveController;

        // movement
        private Rigidbody _rb;
        private Animator _animator;
        private Vector3 forceDir = Vector3.zero;
        private bool _canJump = true;

        // misc
        public GameObject pauseMenu;
        private InGameMenus _menus;

        private void Awake()
        {
            _controls = new InputProfiler();
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _menus = pauseMenu.GetComponent<InGameMenus>();
            grounded = true;
        }

        private void FixedUpdate()
        {
            if (!_menus.pausedActive)
            {
                forceDir += GetCameraRight(playerCam) * (_moveKeys.ReadValue<Vector2>().x * movementForce);
                forceDir += GetCameraForward(playerCam) * (_moveKeys.ReadValue<Vector2>().y * movementForce);
                forceDir += GetCameraRight(playerCam) * (_moveController.ReadValue<Vector2>().x * movementForce);
                forceDir += GetCameraForward(playerCam) * (_moveController.ReadValue<Vector2>().y * movementForce);

                _rb.AddForce(forceDir, ForceMode.Impulse);
                forceDir = Vector3.zero;

                LookAt();   
            }
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

        private void OnCollisionEnter(Collision collision)
        {
            var up = transform.up;
            foreach (var contact in collision.contacts)
            {
                if (Vector3.Dot(contact.normal, up) < 0.5f) continue;
                grounded = true;
                Invoke(nameof(EnableJump), jumpRelockTime);
                return;
            }
        }

        private void EnableJump()
        {
            _canJump = true;
        }

        private void LookAt()
        {
            if (!_menus.pausedActive)
            {
                var direction = _rb.linearVelocity;
                direction.y = 0f;

                if (_moveKeys.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
                    _rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
                else if (_moveController.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
                    _rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
                else
                    _rb.angularVelocity = Vector3.zero;   
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

        private void JumpActive(InputAction.CallbackContext obj)
        {
            if (_menus.pausedActive) return;
            if (!grounded || !_canJump) return;

            grounded = false;
            _canJump = false;
            _animator.speed = _jumpWindup / jumpDelay;
            StartCoroutine(DoAction());
        }

        private IEnumerator DoAction()
        {
            yield return new WaitForSeconds(jumpDelay);
            _animator.speed = 1f;

            var up = transform.up;
            var velocity = _rb.linearVelocity;
            velocity -= up * Vector3.Dot(velocity, up);
            _rb.linearVelocity = velocity;
            _rb.AddForce(up * jumpForce, ForceMode.Impulse);
        }
    }
}