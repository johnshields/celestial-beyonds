using UnityEngine;

public class DungeonPuzzle : MonoBehaviour
{
    public GameObject puzzleInit, lightTrigger, lightHolder;
    public AudioClip openSFX;
    private AudioSource _audio;
    private Animator _animator;
    private int _lights, _door;
    private bool _lightOn;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _lights = Animator.StringToHash("Light");
        _door = Animator.StringToHash("DoorOpen");
        print("Dungeon Puzzle Setup.");
    }

    private void Update()
    {
        if (puzzleInit.GetComponent<PuzzleInit>().initPuzzle && !_lightOn)
        {
            lightTrigger.GetComponent<BoxCollider>().enabled = true;
            _lightOn = true;
            print("_lightOn: " + _lightOn);
        }
    }

    public void ActivateLight()
    {
        lightHolder.GetComponent<Animator>().SetTrigger(_lights);
    }

    public void OpenDungeon()
    {
        _animator.SetTrigger(_door);
    }

    private void OpenSFX()
    {
        _audio.PlayOneShot(openSFX);
    }
}
