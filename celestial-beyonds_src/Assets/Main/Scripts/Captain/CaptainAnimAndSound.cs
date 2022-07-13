using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/*
 * PlayerAnimAndSound
 * Script that controls the Player's animations & sounds.
 */
namespace Main.Scripts.Captain
{
    public class CaptainAnimAndSound : MonoBehaviour
    {
        public float maxSpeed = 5f;
        public AudioClip[] meleeSFX;
        public AudioClip cannonSFX, pollenSFX, capScreamSFX;
        public float delayAction = 1f, dodge;
        public GameObject pollenMeter, pauseMenu;
        public bool meleeActive, cannonFire, pollenFire;
        private bool _actionDone, _unarmed, _armed;
        private Animator _animator;
        private AudioSource _audio;
        private InputProfiler _controls;
        private int _melee0ne, _meleeTwo, _meleeThree, _meleeFour, _meleeFive;
        private GameObject _player, _footsteps, _scraper, _cannon, _pollinator;
        private int _profile, _jump, _dodge, _armedActive, _shoot, _wShoot, _rShoot, _dead;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _controls = new InputProfiler();
            PlayerState(true, false);
        }

        private void Start()
        {
            _audio = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();

            _player = GameObject.FindGameObjectWithTag("Player");
            _scraper = GameObject.FindGameObjectWithTag("Scraper");
            _cannon = GameObject.FindGameObjectWithTag("Cannon");
            _pollinator = GameObject.FindGameObjectWithTag("Pollinator");
            _footsteps = GameObject.FindGameObjectWithTag("Footsteps");
            WeaponSelect(false, false, false);

            _profile = Animator.StringToHash("Profile");
            _jump = Animator.StringToHash("JumpActive");
            _melee0ne = Animator.StringToHash("Melee_1");
            _meleeTwo = Animator.StringToHash("Melee_2");
            _meleeThree = Animator.StringToHash("Melee_3");
            _meleeFour = Animator.StringToHash("Melee_4");
            _meleeFive = Animator.StringToHash("Melee_5");
            _dodge = Animator.StringToHash("Dodge");
            _armedActive = Animator.StringToHash("Armed");
            _shoot = Animator.StringToHash("Shoot");
            _wShoot = Animator.StringToHash("W_Shoot");
            _rShoot = Animator.StringToHash("R_Shoot");
            _dead = Animator.StringToHash("Dead");
        }

        private void Update()
        {
            if (!Jetpack._jetpackActive)
                _animator.SetFloat(_profile, _rb.velocity.magnitude / maxSpeed);
            else
                _rb.angularVelocity = Vector3.zero;

            if (_unarmed)
                _animator.SetBool(_armedActive, false);
            else if (_armed) _animator.SetBool(_armedActive, true);

            if (cannonFire)
                _pollinator.GetComponent<Pollinator>().StopPollenParticles();
            else if (pollenFire)
                _cannon.GetComponent<CannonBlaster>().StopCannonParticles();
        }

        private void OnEnable()
        {
            _controls.Profiler.Jump.started += Jump;
            _controls.Profiler.Attack.started += MeleeAttack;
            _controls.Profiler.Dodge.started += Dodge;
            _controls.Profiler.Shoot.started += ShootCannon;
            _controls.Profiler.Unarmed.started += Unarmed;
            _controls.Profiler.Pollinate.started += Pollinate;
            _controls.Profiler.Enable();
        }

        private void OnDisable()
        {
            _controls.Profiler.Jump.started -= Jump;
            _controls.Profiler.Attack.started -= MeleeAttack;
            _controls.Profiler.Dodge.started -= Dodge;
            _controls.Profiler.Shoot.started -= ShootCannon;
            _controls.Profiler.Unarmed.started -= Unarmed;
            _controls.Profiler.Pollinate.started -= Pollinate;
            _controls.Profiler.Disable();
        }

        private void PlayerState(bool unarmed, bool armed)
        {
            _unarmed = unarmed;
            _armed = armed;
        }

        private void WeaponSelect(bool s, bool c, bool p)
        {
            _scraper.SetActive(s);
            _cannon.SetActive(c);
            _pollinator.SetActive(p);
        }

        private void Jump(InputAction.CallbackContext obj)
        {
            if (!pauseMenu.GetComponent<InGameMenus>().pausedActive)
            {
                if (!CaptainProfiler.grounded || (_actionDone && !Jetpack._jetpackActive)) return;
                _animator.SetTrigger(_jump);
                _actionDone = true;
                Invoke(nameof(ResetAction), delayAction);
            }
        }

        private void Unarmed(InputAction.CallbackContext obj)
        {
            if (!pauseMenu.GetComponent<InGameMenus>().pausedActive)
            {
                PlayerState(true, false);
                if (_unarmed) WeaponSelect(false, false, false);
            }
        }


        private void MeleeAttack(InputAction.CallbackContext obj)
        {
            meleeActive = true;
            _cannon.GetComponent<CannonBlaster>().StopCannonParticles();
            _pollinator.GetComponent<Pollinator>().StopPollenParticles();
            WeaponSelect(true, false, false);
            PlayerState(true, false);
            // random animation
            var attackBool = Random.Range(0, 5);

            if (!pauseMenu.GetComponent<InGameMenus>().pausedActive)
            {
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
                }

                _actionDone = true;
                Invoke(nameof(ResetAction), delayAction);
            }
        }

        private void ShootCannon(InputAction.CallbackContext obj)
        {
            WeaponSelect(false, true, false);
            PlayerState(false, true);
            cannonFire = true;

            if (!pauseMenu.GetComponent<InGameMenus>().pausedActive)
            {
                if (!_actionDone && _armed && cannonFire)
                {
                    _animator.SetTrigger(_rb.velocity.magnitude >= 1f ? _rShoot : _shoot);
                    // call CannonBlaster
                    _cannon.GetComponent<CannonBlaster>().FireCannon();
                    _actionDone = true;
                    Invoke(nameof(ResetAction), delayAction);
                }
            }
        }

        private void Pollinate(InputAction.CallbackContext obj)
        {
            if (!pauseMenu.GetComponent<InGameMenus>().pausedActive)
            {
                WeaponSelect(false, false, true);
                PlayerState(false, true);
                if (_pollinator.GetComponent<Pollinator>().pollenAmmo >= 0)
                {
                    pollenFire = true;
                    if (!_actionDone && _armed)
                    {
                        _animator.SetTrigger(_rb.velocity.magnitude >= 1f ? _rShoot : _shoot);
                        _pollinator.GetComponent<Pollinator>().FirePollinator();
                        _actionDone = true;
                        Invoke(nameof(ResetAction), delayAction);
                    }
                }
                else
                {
                    print("out of pollen ammo");
                    pollenMeter.GetComponent<Image>().color = new Color32(255, 0, 0, 225);
                    StartCoroutine(ResetPollenMeter());
                }
            }
        }

        private IEnumerator ResetPollenMeter()
        {
            yield return new WaitForSeconds(1);
            pollenMeter.GetComponent<Image>().color = new Color32(255, 255, 255, 225);
        }

        private void Dodge(InputAction.CallbackContext obj)
        {
            if (!pauseMenu.GetComponent<InGameMenus>().pausedActive)
            {
                if (_actionDone) return;
                _animator.SetTrigger(_dodge);
                StartCoroutine(WaitToDodge());
                _actionDone = true;
                Invoke(nameof(ResetAction), delayAction);
            }
        }

        public void CapDeath()
        {
            _unarmed = true;
            _animator.SetTrigger(_dead);
            _player.GetComponent<CaptainProfiler>().enabled = false;
            _player.GetComponent<Jetpack>().enabled = false;
        }

        private void ResetAction()
        {
            meleeActive = false;
            _actionDone = false;
            if (cannonFire)
            {
                _cannon.GetComponent<CannonBlaster>().HaltCannon();
                cannonFire = false;
            }
            else if (pollenFire)
            {
                _pollinator.GetComponent<Pollinator>().HaltPollinator();
                pollenFire = false;
            }
        }

        private IEnumerator WaitToDodge()
        {
            yield return new WaitForSeconds(.5f);
            _rb.velocity = transform.TransformDirection(0, 0, dodge);
        }

        private void Footsteps()
        {
            _footsteps.GetComponent<CaptainFootsteps>().FootstepSounds();
        }

        private void Melee()
        {
            _audio.PlayOneShot(meleeSFX[Random.Range(0, meleeSFX.Length)], 0.1f);
        }

        private void CannonFireSFX()
        {
            if (cannonFire)
                _audio.PlayOneShot(cannonSFX, 0.1f);
            else if (pollenFire)
                _audio.PlayOneShot(pollenSFX, 0.1f);
        }

        private void CapScreamSFX()
        {
            _audio.PlayOneShot(capScreamSFX, 0.5f);
        }
    }
}