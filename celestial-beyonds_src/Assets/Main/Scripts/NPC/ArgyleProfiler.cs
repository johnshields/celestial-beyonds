using UnityEngine;
using UnityEngine.InputSystem;

public class ArgyleProfiler : MonoBehaviour
{
    public GameObject btn;
    public float delayAction = 1f;
    private bool _actionDone;
    private Animator _animator;
    private InputProfiler _controls;
    private int _idle, _talk1, _talk2, _talk3, _talk4, _talk5, _talk6;
    private bool _triggered;

    private void Awake()
    {
        _controls = new InputProfiler();
    }

    private void Start()
    {
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

    private void Update()
    {
        print("Triggered Talk: " + _triggered);
        if (_triggered && btn.gameObject.activeInHierarchy && !_actionDone)
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

            _actionDone = true;
            Invoke(nameof(ResetAction), delayAction);
        }
        else
        {
            _animator.SetTrigger(_idle);
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered)
            btn.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_triggered)
            btn.SetActive(true);
    }

    private void ResetAction()
    {
        _actionDone = false;
    }

    private void TalkArgyle(InputAction.CallbackContext obj)
    {
        _triggered = true;
    }
}