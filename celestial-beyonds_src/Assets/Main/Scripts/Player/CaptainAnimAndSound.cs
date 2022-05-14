using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * PlayerAnimAndSound
 * Script that controls the Player's animations & sounds.
 */
namespace Main.Scripts.Player
{
    public class CaptainAnimAndSound : MonoBehaviour
    {
        public float maxSpeed = 5f;
        private Animator _animator;
        private Rigidbody _rb;
        private InputProfiler _controls;
        private GameObject _player;
        private int _profile, _jump;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _controls = new InputProfiler();
        }

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _animator = GetComponent<Animator>();
            _profile = Animator.StringToHash("Profile");
            _jump = Animator.StringToHash("JumpActive");
        }

        private void Update()
        { 
            _animator.SetFloat(_profile, _rb.velocity.magnitude / maxSpeed);
        }
    
        private void OnEnable()
        {
            _controls.Profiler.Jump.started += Jump;
            _controls.Profiler.Enable();
        }

        private void OnDisable()
        {
            _controls.Profiler.Jump.started -= Jump;
            _controls.Profiler.Disable();
        }
    
        private void Jump(InputAction.CallbackContext obj)
        {
            if (!CaptainProfiler.grounded || _rb.velocity.x != 0) return;
            _animator.SetFloat(_profile, 3.5f);
            _animator.SetTrigger(_jump);
        }
    }
}