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
        public AudioClip cannonSFX, pollenSFX, noAmmoSFX, capScreamSFX;
        public float delayAction = 1f, dodge;
        public GameObject pollenMeter, pauseMenu, pollenAmmo, cannonMeter, cannonAmmo, viktor, argyle;
        public bool meleeActive, cannonFire, pollenFire, callMoonbeam, pepperBoxUpgrade, celestialDefierUpgrade;
        private bool _actionDone, _unarmed, _armed;
        private Animator _animator;
        private AudioSource _audio;
        private InputProfiler _controls;
        private int _melee0ne, _meleeTwo, _meleeThree, _meleeFour, _meleeFive;
        private GameObject _player, _footsteps, _scraper, _cannon, _pepperBox, _celestialDefier, _pollinator;
        private int _profile, _jump, _armedJump, _dodge, _armedActive, _shoot, _rShoot, _dead;
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
            _pepperBox = GameObject.FindGameObjectWithTag("PepperBoxBlaster");
            _celestialDefier = GameObject.FindGameObjectWithTag("CelestialDefier");
            _pollinator = GameObject.FindGameObjectWithTag("Pollinator");
            _footsteps = GameObject.FindGameObjectWithTag("Footsteps");
            WeaponSelect(false, false, false);
            _pepperBox.SetActive(false);
            _celestialDefier.SetActive(false);

            _profile = Animator.StringToHash("Profile");
            _jump = Animator.StringToHash("JumpActive");
            _armedJump = Animator.StringToHash("ArmedJump");
            _melee0ne = Animator.StringToHash("Melee_1");
            _meleeTwo = Animator.StringToHash("Melee_2");
            _meleeThree = Animator.StringToHash("Melee_3");
            _meleeFour = Animator.StringToHash("Melee_4");
            _meleeFive = Animator.StringToHash("Melee_5");
            _dodge = Animator.StringToHash("Dodge");
            _armedActive = Animator.StringToHash("Armed");
            _shoot = Animator.StringToHash("Shoot");
            _rShoot = Animator.StringToHash("R_Shoot");
            _dead = Animator.StringToHash("Dead");
        }

        private void Update()
        {
            // only animate if jetpack is not active (still passes through)
            if (!GetComponent<Jetpack>().jetpackActive)
                _animator.SetFloat(_profile, _rb.velocity.magnitude / maxSpeed);
            else
                _rb.angularVelocity = Vector3.zero;

            // unarmed/armed status
            if (_unarmed)
                _animator.SetBool(_armedActive, false);
            else if (_armed) _animator.SetBool(_armedActive, true);

            if (pauseMenu.GetComponent<InGameMenus>().pausedActive)
            {
                _cannon.GetComponent<CannonBlaster>().StopCannonParticles();
                _pollinator.GetComponent<Pollinator>().StopPollenParticles();   
            }

            // stop particles of the opposite gun.
            if (cannonFire)
                _pollinator.GetComponent<Pollinator>().StopPollenParticles();
            else if (pollenFire)
                _cannon.GetComponent<CannonBlaster>().StopCannonParticles();

            // PauseMenu - false to melee, cannonFire and pollenFire
            if (pauseMenu.GetComponent<InGameMenus>().pausedActive)
            {
                meleeActive = false;
                cannonFire = false;
                pollenFire = false;
            }
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
            _pollinator.SetActive(p);
            _cannon.SetActive(c);
            UpgradedCannon();
        }

        private void UpgradedCannon()
        {
            if (pepperBoxUpgrade && !celestialDefierUpgrade && _cannon.activeInHierarchy)
                _pepperBox.SetActive(true);
            else if (!pepperBoxUpgrade && celestialDefierUpgrade && _cannon.activeInHierarchy)
            {
                _celestialDefier.SetActive(true);
                // change mesh & mat
                _cannon.GetComponent<CelestialDefier>().SummonCelestialDefier();
                
            }
            else if (!pepperBoxUpgrade && !celestialDefierUpgrade && _cannon.activeInHierarchy)
            {
                _pepperBox.SetActive(false);
                _celestialDefier.SetActive(false);
            }
        }

        private void Jump(InputAction.CallbackContext obj)
        {
            if (!pauseMenu.GetComponent<InGameMenus>().pausedActive && !GetComponent<Jetpack>().jetpackActive)
            {
                if (GetComponent<CaptainProfiler>().grounded && !_actionDone && !_armed)
                {
                    _animator.SetTrigger(_jump);
                    _actionDone = true;
                    Invoke(nameof(ResetAction), delayAction);
                }
                else if (GetComponent<CaptainProfiler>().grounded && !_actionDone && _armed)
                {
                    AnimWeight(1, .5f);
                    _animator.SetTrigger(_armedJump);
                    _actionDone = true;
                    Invoke(nameof(ResetAction), 1.7f);
                }
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
            _pepperBox.SetActive(false);
            _celestialDefier.SetActive(false);
            _cannon.GetComponent<CannonBlaster>().StopCannonParticles();
            _pollinator.GetComponent<Pollinator>().StopPollenParticles();
            WeaponSelect(true, false, false);
            PlayerState(true, false);
            // random animation
            var attackBool = Random.Range(0, 5);

            if (!pauseMenu.GetComponent<InGameMenus>().pausedActive &&
                !GetComponent<CaptainHealth>().capDead &&
                !argyle.activeInHierarchy && !viktor.activeInHierarchy)
            {
                if (!_actionDone && !callMoonbeam)
                {
                    callMoonbeam = true;
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
            if (!GetComponent<CaptainHealth>().capDead)
            {
                if (!pauseMenu.GetComponent<InGameMenus>().pausedActive &&
                    !argyle.activeInHierarchy && !viktor.activeInHierarchy)
                {
                    WeaponSelect(false, true, false);
                    PlayerState(false, true);
                    if (cannonAmmo.GetComponent<CannonAmmo>().cannonAmmo != 0)
                    {
                        cannonFire = true;
                        if (!_actionDone && _armed && cannonFire && !callMoonbeam)
                        {
                            callMoonbeam = true;
                            _animator.SetTrigger(_rb.velocity.magnitude >= 1f ? _rShoot : _shoot);
                            // call CannonBlaster
                            _cannon.GetComponent<CannonBlaster>().FireCannon();
                            _actionDone = true;
                            Invoke(nameof(ResetAction), delayAction);
                        }
                    }
                    else
                    {
                        print("out of cannon ammo");
                        _audio.PlayOneShot(noAmmoSFX);
                        cannonMeter.GetComponent<Image>().color = new Color32(255, 0, 0, 225);
                        StartCoroutine(ResetAmmoMeter(0));
                    }
                }
            }
        }

        private void Pollinate(InputAction.CallbackContext obj)
        {
            if (!GetComponent<CaptainHealth>().capDead)
            {
                if (!pauseMenu.GetComponent<InGameMenus>().pausedActive &&
                    !argyle.activeInHierarchy && !viktor.activeInHierarchy)
                {
                    WeaponSelect(false, false, true);
                    PlayerState(false, true);
                    if (pollenAmmo.GetComponent<PollinatorAmmo>().pollenAmmo != 0)
                    {
                        pollenFire = true;
                        if (!_actionDone && _armed && pollenFire)
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
                        _audio.PlayOneShot(noAmmoSFX);
                        pollenMeter.GetComponent<Image>().color = new Color32(255, 0, 0, 225);
                        StartCoroutine(ResetAmmoMeter(1));
                    }
                }
            }
        }

        private IEnumerator ResetAmmoMeter(int which)
        {
            yield return new WaitForSeconds(1);
            switch (which)
            {
                case 0:
                    cannonMeter.GetComponent<Image>().color = new Color32(255, 255, 255, 225);
                    break;
                case 1:
                    pollenMeter.GetComponent<Image>().color = new Color32(255, 255, 255, 225);
                    break;
            }
        }

        private void Dodge(InputAction.CallbackContext obj)
        {
            if (!pauseMenu.GetComponent<InGameMenus>().pausedActive && !GetComponent<CaptainHealth>().capDead)
            {
                if (_actionDone) return;
                AnimWeight(1f, 0f);
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
            callMoonbeam = false;
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

            ResetAnimWeight();
        }

        private IEnumerator WaitToDodge()
        {
            yield return new WaitForSeconds(.25f);
            _rb.velocity = transform.TransformDirection(0, 0, dodge);
            Invoke(nameof(ResetAnimWeight), delayAction);
        }

        private void AnimWeight(float actions, float shoot)
        {
            if (!_armed) return;
            _animator.SetLayerWeight(1, actions); // Dodge
            _animator.SetLayerWeight(3, shoot);
            _animator.SetLayerWeight(4, shoot);
        }


        private void ResetAnimWeight()
        {
            _animator.SetLayerWeight(1, 1f);
            _animator.SetLayerWeight(3, 1f);
            _animator.SetLayerWeight(4, 1f);
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