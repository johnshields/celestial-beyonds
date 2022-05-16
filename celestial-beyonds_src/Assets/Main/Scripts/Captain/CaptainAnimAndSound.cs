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
        private GameObject _player, _footsteps, _scraper, _cannon;
        private int _profile, _jump, _dodge;
        private int _melee0ne, _meleeTwo, _meleeThree, _meleeFour, _meleeFive;
        private AudioSource _audio;
        public AudioClip[] meleeSFX;
        public float delayAction = 1f, dodge;
        private bool _actionDone, _unarmed, _armed, _melee;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _controls = new InputProfiler();
            PlayerState(true, false, false);
        }

        private void Start()
        {
            _audio = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();

            _player = GameObject.FindGameObjectWithTag("Player");
            _scraper = GameObject.FindGameObjectWithTag("Scraper");
            _cannon = GameObject.FindGameObjectWithTag("Cannon");
            _footsteps = GameObject.FindGameObjectWithTag("Footsteps");
            WeaponSelect(false, false);

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
            _controls.Profiler.Shoot.started += Shoot;
            _controls.Profiler.Unarmed.started += Unarmed;
            _controls.Profiler.Enable();
        }

        private void OnDisable()
        {
            _controls.Profiler.Jump.started -= Jump;
            _controls.Profiler.Attack.started -= MeleeAttack;
            _controls.Profiler.Dodge.started -= Dodge;
            _controls.Profiler.Shoot.started -= Shoot;
            _controls.Profiler.Unarmed.started -= Unarmed;
            _controls.Profiler.Disable();
        }

        private void PlayerState(bool unarmed, bool melee, bool armed)
        {
            _unarmed = unarmed;
            _melee = melee;
            _armed = armed;
        }

        private void SetLayerWeight(int index, float uw, float mw, float aw)
        {
            switch (index)
            {
                case 1:
                    _animator.SetLayerWeight(_animator.GetLayerIndex("Unarmed"), uw);
                    break;
                case 2:
                    _animator.SetLayerWeight(_animator.GetLayerIndex("Melee"), mw);
                    break;
                case 3:
                    _animator.SetLayerWeight(_animator.GetLayerIndex("Armed"), aw);
                    break;
            }
        }

        private void WeaponSelect(bool s, bool c)
        {
            _scraper.SetActive(s);
            _cannon.SetActive(c);
        }


        private void Unarmed(InputAction.CallbackContext obj)
        {
            WeaponSelect(false, false);
            PlayerState(true, false, false);
            SetLayerWeight(1, 1f, 0, 0);
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
            WeaponSelect(true, false);
            PlayerState(false, true, false);
            SetLayerWeight(2, 0, 1f, 0);
            var attackBool = Random.Range(0, 5);

            if (!_actionDone && _melee)
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

        private void Shoot(InputAction.CallbackContext obj)
        {
            WeaponSelect(false, true);
            PlayerState(false, false, true);
            SetLayerWeight(3, 0, 0, 1f);

            if (!_actionDone && _armed)
            {
                print("Shooting...");
                _actionDone = true;
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
            PlayerState(false, false, false);
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