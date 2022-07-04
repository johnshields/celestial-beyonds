using UnityEngine;

public class Plants : MonoBehaviour
{
    private Animator _animator;
    private int _grow;
    private GameObject pl;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _grow = Animator.StringToHash("Grow");
        pl =  GameObject.FindGameObjectWithTag("PollinationLevel");
    }

    public void GrowPlant()
    {
        print("plant growing");
        _animator.SetTrigger(_grow);
        pl.GetComponent<PollinationLevel>().IncreasePollination();
    }
}
