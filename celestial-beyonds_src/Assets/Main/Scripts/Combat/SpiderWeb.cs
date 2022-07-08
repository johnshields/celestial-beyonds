using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    private bool _webShot;

    private void OnParticleCollision(GameObject other)
    {
        if (!_webShot)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CaptainHealth>().PlayerTakeDamage(10);
            Invoke(nameof(ResetWeb), 3);
        }
    }

    private void ResetWeb()
    {
        _webShot = false;
    }
}