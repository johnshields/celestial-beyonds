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
        private int _profile, _jump, _melee0ne, _meleeTwo;

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
            _melee0ne = Animator.StringToHash("Melee_1");
            _meleeTwo = Animator.StringToHash("Melee_2");
        }

        private void Update()
        {
            _animator.SetFloat(_profile, _rb.velocity.magnitude / maxSpeed);
        }

        private void OnEnable()
        {
            _controls.Profiler.Jump.started += Jump;
            _controls.Profiler.Attack.started += MeleeAttack;
            _controls.Profiler.Enable();
        }

        private void OnDisable()
        {
            _controls.Profiler.Jump.started -= Jump;
            _controls.Profiler.Attack.started -= MeleeAttack;
            _controls.Profiler.Disable();
        }

        private void Jump(InputAction.CallbackContext obj)
        {
            if (CaptainProfiler.grounded)
                _animator.SetTrigger(_jump);
        }

        private void MeleeAttack(InputAction.CallbackContext obj)
        {
            // var attackBool = Random.Range(0, 2);
            // print(attackBool);
            // switch (attackBool)
            // {
            //     case 0:
            //         _animator.SetTrigger(_melee0ne);
            //         break;
            //     case 1:
            //         _animator.SetTrigger(_meleeTwo);
            //         break;
            // }
        }
    }
}