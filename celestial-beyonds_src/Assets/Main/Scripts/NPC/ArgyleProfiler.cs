using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ArgyleProfiler : MonoBehaviour
{
    public GameObject stationUI, peridotCounterUI, flowerImage, pollinator;
    public float delayAction = 1f;
    private bool _actionDone;
    private Animator _animator;
    private InputProfiler _controls;
    private int _idle, _talk1, _talk2, _talk3, _talk4, _talk5, _talk6;
    private GameObject _player;
    private Component _peridotCounter;

    private void Awake()
    {
        _controls = new InputProfiler();
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
        _controls.Profiler.TalkArgyle.started += TalkArgyle;
        _controls.Profiler.Enable();
    }

    private void OnDisable()
    {
        _controls.Profiler.TalkArgyle.started -= TalkArgyle;
        _controls.Profiler.Disable();
    }

    private void TalkArgyle(InputAction.CallbackContext obj)
    {
        if (pollinator.GetComponent<Pollinator>().pollenAmmo != 0 &&
            pollinator.GetComponent<Pollinator>().pollenAmmo !=
            pollinator.GetComponent<Pollinator>().maxAmmo &&
            _peridotCounter.GetComponent<PeridotCounter>().peridots != 0)
        {
            print("Pollen sold");
            pollinator.GetComponent<Pollinator>().FillUpPollen(50);
            _peridotCounter.GetComponent<PeridotCounter>().SellPeridots(5);
            SwitchAnim();
        }
        else if (_peridotCounter.GetComponent<PeridotCounter>().peridots <= 0)
        {
            print("Not enough peridots");
            peridotCounterUI.GetComponent<Image>().color = new Color32(255, 0, 0, 225);
            StartCoroutine(ResetCounterColor(0));
        }
        else if (_peridotCounter.GetComponent<PeridotCounter>().peridots <= 0 &&
                 pollinator.GetComponent<Pollinator>().pollenAmmo == 0)
        {
            print("Not enough peridots and out of pollen ammo");
            peridotCounterUI.GetComponent<Image>().color = new Color32(255, 0, 0, 225);
            StartCoroutine(ResetCounterColor(0));
        }
        else if (pollinator.GetComponent<Pollinator>().pollenAmmo ==
                 pollinator.GetComponent<Pollinator>().maxAmmo)
        {
            print("pollen full");
            flowerImage.GetComponent<Image>().color = new Color32(52, 255, 0, 225);
            StartCoroutine(ResetCounterColor(1));
        }

        if (_actionDone) return;
        _actionDone = true;
        Invoke(nameof(ResetAction), delayAction);
    }

    private IEnumerator ResetCounterColor(int whichCounter)
    {
        yield return new WaitForSeconds(1);
        if (whichCounter == 0)
            peridotCounterUI.GetComponent<Image>().color = new Color32(255, 255, 255, 225);
        else if (whichCounter == 1)
            flowerImage.GetComponent<Image>().color = new Color32(255, 255, 255, 225);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            stationUI.SetActive(true);
            SwitchAnim();
            if (_actionDone) return;
            _actionDone = true;
            Invoke(nameof(ResetAction), delayAction);
        }
        else
        {
            _animator.SetTrigger(_idle);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
            stationUI.SetActive(false);
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