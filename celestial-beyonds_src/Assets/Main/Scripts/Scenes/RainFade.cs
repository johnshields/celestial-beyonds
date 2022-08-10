using UnityEngine;

public class RainFade : MonoBehaviour
{
    private Animator _animator;
    private int _fadeIn, _fadeOut;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _fadeIn = Animator.StringToHash("FadeIn");
        _fadeOut = Animator.StringToHash("FadeOut");
        RainFader(0);
        print($"RainFaded in {true}");
    }

    public void RainFader(int fade)
    {
        switch (fade)
        {
            case 0:
                _animator.SetBool(_fadeIn, true);
                _animator.SetBool(_fadeOut, false);
                break;
            case 1:
                _animator.SetBool(_fadeIn, false);
                _animator.SetBool(_fadeOut, true);
                break;
        }
    }
}
