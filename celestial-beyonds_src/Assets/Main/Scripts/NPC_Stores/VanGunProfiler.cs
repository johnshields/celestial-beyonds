using System.Collections;
using Main.Scripts.Captain;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VanGunProfiler : MonoBehaviour
{
    public GameObject stationUI, peridotCounterUI, ammoHandle, pauseMenu, canAmmo, randoAudio, upgradeOption;
    public float delayAction = 1f, audioVol = .4f;
    public AudioClip sale, noSale;
    public int upgradeNum, upgradeCost;
    public bool saleActive, upgradeCannon, upgradeArmor;
    private bool _actionDone, _saidHello, _saidBye;
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
    }

    private void Start()
    {
        _audio.minDistance = 10;
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
        
        // to avoid lag on encounter.
        PlayRandomClip("Hellos", 0f);
        PlayRandomClip("Byes", 0f);
        PlayRandomClip("Sold", 0f);
        PlayRandomClip("NoSale", 0f);
        PlayRandomClip("MaxAmmo", 0f);
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
    
    private void Update()
    {
        if (_player.GetComponent<CaptainAnimAndSound>().lookingAtViktor) StoreOpen();
        else StoreClose();
    }

    private void StoreOpen()
    {
        // say Hello
        if (!_saidHello && !saleActive)
        {
            if (!_saidHello)
            {
                PlayRandomClip("Hellos", audioVol);
                _saidHello = true;   
            }
            saleActive = true;
            _saidBye = false;
            stationUI.SetActive(true);
            SwitchAnim();
            if (_actionDone) return;
            _actionDone = true;
            Invoke(nameof(ResetAction), delayAction);
        }
    }
    
    private void StoreClose()
    {
        if (!_saidBye && _saidHello)
        {
            PlayRandomClip("Byes", audioVol);
            _saidBye = true;
            _saidHello = false;
        }
        saleActive = false;
        stationUI.SetActive(false);
        
    }

    private void TalkViktor(InputAction.CallbackContext obj)
    {
        if (saleActive && !pauseMenu.GetComponent<InGameMenus>().pausedActive)
        {
            // only sell ammo if they do not have maxAmmo or no peridots.
            if (canAmmo.GetComponent<CannonAmmo>().cannonAmmo != canAmmo.GetComponent<CannonAmmo>().maxAmmo &&
                PlayerMemory.peridots != 0)
            {
                PlayRandomClip("Sold", audioVol);
                _audio.PlayOneShot(sale, 0.1f);
                canAmmo.GetComponent<CannonAmmo>().FillUpCannon(10);
                _peridotCounter.GetComponent<PeridotCounter>().SellPeridots(1);
                SwitchAnim();
            }
            // decline the sale if the player has less than 0 peridots + flash peridotCounter.
            else if (PlayerMemory.peridots <= 0)
            {
                print("Not enough peridots");
                PlayRandomClip("NoSale", audioVol);
                _audio.PlayOneShot(noSale, 0.1f);
                peridotCounterUI.GetComponent<Image>().color = new Color32(255, 0, 0, 225);
                StartCoroutine(ResetCounterColor());
            }
            // decline sale if ammo is full + flash cannonMeter.
            else if (canAmmo.GetComponent<CannonAmmo>().cannonAmmo == canAmmo.GetComponent<CannonAmmo>().maxAmmo)
            {
                print("Ammo full!");
                ammoHandle.GetComponent<Image>().color = new Color32(52, 255, 0, 225);
                PlayRandomClip("MaxAmmo", audioVol);
                StartCoroutine(ResetCounterColor());
            }

            if (_actionDone) return;
            _actionDone = true;
            Invoke(nameof(ResetAction), delayAction);
        }
    }
    
    private void ViktorUpgrade(InputAction.CallbackContext obj)
    {
        if (PlayerMemory.peridots >= upgradeCost && _player.GetComponent<CaptainAnimAndSound>().lookingAtViktor)
        {
            PlayRandomClip("Sold", audioVol);
            _audio.PlayOneShot(sale, 0.1f);
            _peridotCounter.GetComponent<PeridotCounter>().SellPeridots(upgradeCost);
            canAmmo.GetComponent<CannonAmmo>().cannonAmmo = canAmmo.GetComponent<CannonAmmo>().maxAmmo;
            if(!Rambo.cheatActivated)
                UpgradeCannon(upgradeNum);
        }
        else if(PlayerMemory.peridots < upgradeCost && _player.GetComponent<CaptainAnimAndSound>().lookingAtViktor)
        {
            print("Not enough peridots");
            PlayRandomClip("NoSale", audioVol);
            _audio.PlayOneShot(noSale, 0.1f);
            peridotCounterUI.GetComponent<Image>().color = new Color32(255, 0, 0, 225);
            StartCoroutine(ResetCounterColor());
        }
    }


    private IEnumerator ResetCounterColor()
    {
        yield return new WaitForSeconds(1);
        peridotCounterUI.GetComponent<Image>().color = new Color32(255, 255, 255, 225);
        ammoHandle.GetComponent<Image>().color = new Color32(255, 255, 255, 225);
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
            _player.GetComponent<CaptainAnimAndSound>().pbUpgrade = true;
            upgradeOption.SetActive(false);
            print("CANNON BLASTER Upgraded to PEPPERBOX BLASTER!");
            Bools.pbUpgraded = true;
            Bools.cdUpgraded = false;
            PlayerMemory.cannonUpgrade = 1;
        }
        else if (upgrade == 2 && !upgradeCannon)
        {
            upgradeCannon = true;
            _player.GetComponent<CaptainAnimAndSound>().cdUpgrade = true;
            upgradeOption.SetActive(false);
            print("PEPPERBOX BLASTER Upgraded to THE CELESTIAL DEFIER!");
            Bools.pbUpgraded = false;
            Bools.cdUpgraded = true;
            PlayerMemory.cannonUpgrade = 2;
        }
        else if (upgrade == 3 && !upgradeArmor)
        {
            upgradeArmor = true;
            _player.GetComponent<CaptainAnimAndSound>().aUpgrade = true;
            _player.GetComponent<CaptainHealth>().currentHealth = 200;
            upgradeOption.SetActive(false);
            print("ARMOR UPGRADED!");
            Bools.aUpgraded = true;
            PlayerMemory.armorUpgrade = 1;
        }
    }
    
}