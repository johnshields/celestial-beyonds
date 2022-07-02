using UnityEngine;

public class Plants : MonoBehaviour
{
    private Animator _animator;
    private int _grow;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _grow = Animator.StringToHash("Grow");
    }

    public void GrowPlant()
    {
        print("plant growing");
        _animator.SetTrigger(_grow);
    }
}
