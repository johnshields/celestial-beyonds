using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ArgyleProfiler : MonoBehaviour
{
    public GameObject stationUI, peridotCounterUI, pollenMeter, pauseMenu, ammo;
    public float delayAction = 1f;
    public AudioClip[] argyleHellos, argyleByes, argyleFeelings, argyleSales, argyleNoSales;
    public AudioClip sale, noSale;
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
        _controls.Profiler.StoreInteraction.started += TalkArgyle;
        _controls.Profiler.Enable();
    }

    private void OnDisable()
    {
        _controls.Profiler.StoreInteraction.started -= TalkArgyle;
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
            _audio.Stop();
            _audio.PlayOneShot(argyleHellos[Random.Range(0, argyleHellos.Length)]);
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
            _audio.Stop();
            _audio.PlayOneShot(argyleByes[Random.Range(0, argyleByes.Length)]);
        }
    }

    private void TalkArgyle(InputAction.CallbackContext obj)
    {
        if (_saleActive && !pauseMenu.GetComponent<InGameMenus>().pausedActive)
        {
            // only sell player pollen if they do not have maxAmmo or no peridots.
            if (ammo.GetComponent<PollinatorAmmo>().pollenAmmo != ammo.GetComponent<PollinatorAmmo>().maxAmmo &&
                _peridotCounter.GetComponent<PeridotCounter>().peridots != 0)
            {
                print("Pollen sold");
                _audio.Stop();
                _audio.PlayOneShot(argyleSales[Random.Range(0, argyleSales.Length)]);
                _audio.PlayOneShot(sale, 0.1f);
                ammo.GetComponent<PollinatorAmmo>().FillUpPollen(10);
                _peridotCounter.GetComponent<PeridotCounter>().SellPeridots(1);
                SwitchAnim();
            }
            // decline the sale if the player has less than 0 peridots + flash peridotCounter.
            else if (_peridotCounter.GetComponent<PeridotCounter>().peridots <= 0)
            {
                print("Not enough peridots");
                _audio.Stop();
                _audio.PlayOneShot(argyleNoSales[Random.Range(0, argyleNoSales.Length)]);
                _audio.PlayOneShot(noSale, 0.1f);
                peridotCounterUI.GetComponent<Image>().color = new Color32(255, 0, 0, 225);
                StartCoroutine(ResetCounterColor(0));
            }
            // decline sale if pollen is full + flash pollenMeter.
            else if (ammo.GetComponent<PollinatorAmmo>().pollenAmmo == ammo.GetComponent<PollinatorAmmo>().maxAmmo)
            {
                print("pollen full");
                _audio.Stop();
                _audio.PlayOneShot(argyleFeelings[Random.Range(0, argyleFeelings.Length)]);
                pollenMeter.GetComponent<Image>().color = new Color32(52, 255, 0, 225);
                StartCoroutine(ResetCounterColor(1));
            }

            if (_actionDone) return;
            _actionDone = true;
            Invoke(nameof(ResetAction), delayAction);
        }
    }

    private IEnumerator ResetCounterColor(int whichCounter)
    {
        yield return new WaitForSeconds(1);
        if (whichCounter == 0)
            peridotCounterUI.GetComponent<Image>().color = new Color32(255, 255, 255, 225);
        else if (whichCounter == 1)
            pollenMeter.GetComponent<Image>().color = new Color32(255, 255, 255, 225);
    }

    private void ResetAction()
    {
        _actionDone = false;
    }

    private void SwitchAnim()
    {
        var talkBool = Random.Range(0, 7);
        print("Argyle anim: " + talkBool);
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
}