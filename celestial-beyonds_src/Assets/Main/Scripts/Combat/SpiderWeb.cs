using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    private bool _webShot;
    private ParticleSystem _playerBloodParticles;

    private void Start()
    {
        _playerBloodParticles = GameObject.FindGameObjectWithTag("PlayerBlood").GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!_webShot)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CaptainHealth>().PlayerTakeDamage(10);
            _playerBloodParticles.Play();
            Invoke(nameof(ResetWeb), 3);
        }
    }

    private void ResetWeb()
    {
        _webShot = false;
    }
}