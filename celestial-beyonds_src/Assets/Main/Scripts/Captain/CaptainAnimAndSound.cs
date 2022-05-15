using UnityEngine;
using UnityEngine.InputSystem;

/*
 * PlayerAnimAndSound
 * Script that controls the Player's animations & sounds.
 */
namespace Main.Scripts.Captain
{
    public class CaptainAnimAndSound : MonoBehaviour
    {
        public float maxSpeed = 5f;
        private Animator _animator;
        private Rigidbody _rb;
        private InputProfiler _controls;
        private GameObject _player, _footsteps;
        private int _profile, _jump, _dodge;
        private int _melee0ne, _meleeTwo, _meleeThree, _meleeFour, _meleeFive;
        private AudioSource _audio;
        public AudioClip[] meleeSFX;
        public float delayAction = 1f, dodge;
        private bool _actionDone;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _controls = new InputProfiler();
        }

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _footsteps = GameObject.FindGameObjectWithTag("Footsteps");
            _animator = GetComponent<Animator>();
            _audio = GetComponent<AudioSource>();
            _profile = Animator.StringToHash("Profile");
            _jump = Animator.StringToHash("JumpActive");
            _melee0ne = Animator.StringToHash("Melee_1");
            _meleeTwo = Animator.StringToHash("Melee_2");
            _meleeThree = Animator.StringToHash("Melee_3");
            _meleeFour = Animator.StringToHash("Melee_4");
            _meleeFive = Animator.StringToHash("Melee_5");
            _dodge = Animator.StringToHash("Dodge");
        }

        private void Update()
        {
            _animator.SetFloat(_profile, _rb.velocity.magnitude / maxSpeed);
        }

        private void OnEnable()
        {
            _controls.Profiler.Jump.started += Jump;
            _controls.Profiler.Attack.started += MeleeAttack;
            _controls.Profiler.Dodge.started += Dodge;
            _controls.Profiler.Enable();
        }

        private void OnDisable()
        {
            _controls.Profiler.Jump.started -= Jump;
            _controls.Profiler.Attack.started -= MeleeAttack;
            _controls.Profiler.Dodge.started -= Dodge;
            _controls.Profiler.Disable();
        }
        
        private void Jump(InputAction.CallbackContext obj)
        {
            if (!CaptainProfiler.grounded || _actionDone) return;
            _animator.SetTrigger(_jump);   
            _actionDone = true;
            Invoke(nameof(ResetAction), delayAction);
        }

        private void MeleeAttack(InputAction.CallbackContext obj)
        {
            var attackBool = Random.Range(0, 5);
            if (!_actionDone)
            {
                switch (attackBool)
                {
                    case 0:
                        _animator.SetTrigger(_melee0ne);
                        break;
                    case 1:
                        _animator.SetTrigger(_meleeTwo);
                        break;
                    case 2:
                        _animator.SetTrigger(_meleeThree);
                        break;
                    case 3:
                        _animator.SetTrigger(_meleeFour);
                        break;
                    case 4:
                        _animator.SetTrigger(_meleeFive);
                        break;
                }

                _actionDone = true;
                Invoke(nameof(ResetAction), delayAction);
            }
        }
        
        private void Dodge(InputAction.CallbackContext obj)
        {
            if (_actionDone) return;
            _animator.SetTrigger(_dodge);
            _rb.velocity = transform.TransformDirection(0, 0, dodge);
            _actionDone = true;
            Invoke(nameof(ResetAction), delayAction);
        }

        private void ResetAction()
        {
            _actionDone = false;
        }

        private void Footsteps()
        {
            _footsteps.GetComponent<CaptainFootsteps>().FootstepSounds();
        }

        private void Melee()
        {
            _audio.PlayOneShot(meleeSFX[Random.Range(0, meleeSFX.Length)], 0.1f);
        }
    }
}