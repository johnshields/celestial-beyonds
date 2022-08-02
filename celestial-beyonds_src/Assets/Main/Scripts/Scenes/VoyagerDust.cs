using UnityEngine;

public class VoyagerDust : MonoBehaviour
{
    public GameObject dustParticle;
    private bool _done;

    private void Update()
    {
        if (dustParticle.activeInHierarchy && !_done)
        {
            dustParticle.GetComponent<ParticleSystem>().Play();
            _done = true;
        }
    }
}
