using System.Collections;
using Main.Scripts.Captain;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VanGunProfiler : MonoBehaviour
{
    public GameObject stationUI, peridotCounterUI, cannonMeter, pauseMenu, canAmmo, randoAudio, 
        upgradeOption, cannon;
    public float delayAction = 1f, audioVol = .4f;
    public AudioClip sale, noSale;
    public int upgradeNum, upgradeCost;
    public bool upgradeCannon;
    private bool _actionDone, _saleActive;
    private Animator _animator;
    private AudioSource _audio;
    private InputProfiler _controls;
    private int _idle, _talk1, _talk2, _talk3, _talk4, _talk5, _talk6;
    private Component _peridotCounter;
    private GameObject _player;

    private void Awake()
    {
        _controls = new InputProfiler();
        _audio = GetComponent<AudioSource>();

        // to avoid lag on encounter.
        PlayRandomClip("Hellos", 0f);
        PlayRandomClip("Byes", 0f);
        PlayRandomClip("Sold", 0f);
        PlayRandomClip("NoSale", 0f);
        PlayRandomClip("MaxAmmo", 0f);
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _peridotCounter = _player.GetComponent<PeridotCounter>();
        _animator = GetComponent<Animator>();
        _idle = Animator.StringToHash("Idle");
        _talk1 = Animator.StringToHash("Talk1");
        _talk2 = Animator.StringToHash("Talk2");
        _talk3 = Animator.StringToHash("Talk3");
        _talk4 = Animator.StringToHash("Talk4");
        _talk5 = Animator.StringToHash("Talk5");
        _talk6 = Animator.StringToHash("Talk6");
        _animator.SetTrigger(_idle);
    }

    private void OnEnable()
    {
        _controls.Profiler.StoreInteraction.started += TalkViktor;
        _controls.Profiler.UpgradeCannon.started += ViktorUpgrade;
        _controls.Profiler.Enable();
    }
    
    private void OnDisable()
    {
        _controls.Profiler.StoreInteraction.started -= TalkViktor;
        _controls.Profiler.UpgradeCannon.started -= ViktorUpgrade;
        _controls.Profiler.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        // say Hello
        if (other.gameObject == _player && !_saleActive)
        {
            _saleActive = true;
            stationUI.SetActive(true);
            SwitchAnim();
            if (_actionDone) return;
            // Say hello
            PlayRandomClip("Hellos", audioVol);
            _actionDone = true;
            Invoke(nameof(ResetAction), delayAction);
        }
        else
        {
            _saleActive = false;
            _animator.SetTrigger(_idle);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            _saleActive = false;
            stationUI.SetActive(false);
            PlayRandomClip("Byes", audioVol);
        }
    }

    private void TalkViktor(InputAction.CallbackContext obj)
    {
        if (_saleActive && !pauseMenu.GetComponent<InGameMenus>().pausedActive)
        {
            // only sell ammo if they do not have maxAmmo or no peridots.
            if (canAmmo.GetComponent<CannonAmmo>().cannonAmmo != canAmmo.GetComponent<CannonAmmo>().maxAmmo &&
                _peridotCounter.GetComponent<PeridotCounter>().peridots != 0)
            {
                PlayRandomClip("Sold", audioVol);
                _audio.PlayOneShot(sale, 0.1f);
                canAmmo.GetComponent<CannonAmmo>().FillUpCannon(10);
                _peridotCounter.GetComponent<PeridotCounter>().SellPeridots(1);
                SwitchAnim();
            }
            // decline the sale if the player has less than 0 peridots + flash peridotCounter.
            else if (_peridotCounter.GetComponent<PeridotCounter>().peridots <= 0)
            {
                print("Not enough peridots");
                PlayRandomClip("NoSale", audioVol);
                _audio.PlayOneShot(noSale, 0.1f);
                peridotCounterUI.GetComponent<Image>().color = new Color32(255, 0, 0, 225);
                StartCoroutine(ResetCounterColor(0));
            }
            // decline sale if ammo is full + flash cannonMeter.
            else if (canAmmo.GetComponent<CannonAmmo>().cannonAmmo == canAmmo.GetComponent<CannonAmmo>().maxAmmo)
            {
                print("Ammo full!");
                PlayRandomClip("MaxAmmo", audioVol);
                cannonMeter.GetComponent<Image>().color = new Color32(52, 255, 0, 225);
                StartCoroutine(ResetCounterColor(1));
            }

            if (_actionDone) return;
            _actionDone = true;
            Invoke(nameof(ResetAction), delayAction);
        }
    }
    
    private void ViktorUpgrade(InputAction.CallbackContext obj)
    {
        if (upgradeOption.activeInHierarchy && _peridotCounter.GetComponent<PeridotCounter>().peridots >= upgradeCost)
            UpgradeCannon(upgradeNum);
        else
        {
            print("Not enough peridots");
            PlayRandomClip("NoSale", audioVol);
            _audio.PlayOneShot(noSale, 0.1f);
            peridotCounterUI.GetComponent<Image>().color = new Color32(255, 0, 0, 225);
            StartCoroutine(ResetCounterColor(0));
        }
    }


    private IEnumerator ResetCounterColor(int whichCounter)
    {
        yield return new WaitForSeconds(1);
        if (whichCounter == 0)
            peridotCounterUI.GetComponent<Image>().color = new Color32(255, 255, 255, 225);
        else if (whichCounter == 1)
            cannonMeter.GetComponent<Image>().color = new Color32(255, 255, 255, 225);
    }

    private void ResetAction()
    {
        _actionDone = false;
    }

    private void SwitchAnim()
    {
        var talkBool = Random.Range(0, 7);
        print("Viktor anim: " + talkBool);
        switch (talkBool)
        {
            case 0:
                _animator.SetTrigger(_idle);
                break;
            case 1:
                _animator.SetTrigger(_talk1);
                break;
            case 2:
                _animator.SetTrigger(_talk2);
                break;
            case 3:
                _animator.SetTrigger(_talk3);
                break;
            case 4:
                _animator.SetTrigger(_talk4);
                break;
            case 5:
                _animator.SetTrigger(_talk5);
                break;
            case 6:
                _animator.SetTrigger(_talk6);
                break;
        }
    }

    private void PlayRandomClip(string path, float vol)
    {
        _audio.Stop();
        _audio.PlayOneShot(randoAudio.GetComponent<AudioRandomizer>().GetRandomClip("VanGun/" + path), vol);
    }

    private void UpgradeCannon(int upgrade)
    {
        if (upgrade == 1 && !upgradeCannon)
        {
            upgradeCannon = true;
            _player.GetComponent<CaptainAnimAndSound>().pepperBoxUpgrade = true;
            upgradeOption.SetActive(false);
            print("CANNON BLASTER Upgraded to PEPPERBOX BLASTER!");
        }
        else if (upgrade == 2 && !upgradeCannon)
        {
            upgradeCannon = true;
            _player.GetComponent<CaptainAnimAndSound>().celestialDefierUpgrade = true;
            upgradeOption.SetActive(false);
            print("PEPPERBOX BLASTER Upgraded to THE CELESTIAL DEFIER!");
        }
    }
}