using UnityEngine;

public class SpiderCombat : MonoBehaviour
{
    private bool _webShot;
    private string _spiderWeb;
    private GameObject _spider;

    private void Start()
    {
        _spiderWeb = "Enemies/Spiders/" + gameObject.name + "/spider/WebParticle";
        _spider = GameObject.Find(_spiderWeb);
    }

    private void SpiderWeb()
    {
        if (!_webShot)
        {
            _spider.GetComponent<ParticleSystem>().Play();
            Invoke(nameof(ResetWeb), 3);
        }
    }

    private void ResetWeb()
    {
        _webShot = false;
        _spider.GetComponent<ParticleSystem>().Play();
    }
}