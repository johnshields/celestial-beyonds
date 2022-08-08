using UnityEngine;

public class VoyagerDust : MonoBehaviour
{
    public GameObject dustParticle;
    private bool _done;

    private void Update()
    {
        if (dustParticle.activeInHierarchy && !_done)
        {
            print("do");
            dustParticle.GetComponent<ParticleSystem>().Play();
            _done = true;
        }
    }
}
