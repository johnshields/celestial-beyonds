using System.Collections;
using UnityEngine;

public class CannonBlaster : MonoBehaviour
{
    public GameObject particles;
    private int status;

    public void FireCannon()
    {
        status = 0;
        StartCoroutine(CannonWait());
    }

    public void HaltCannon()
    {
        status = 1;
        StartCoroutine(CannonWait());
    }

    private IEnumerator CannonWait()
    {
        switch (status)
        {
            case 0:
                yield return new WaitForSeconds(0.2f);
                particles.GetComponent<ParticleSystem>().Play();
                break;
            case 1:
                yield return new WaitForSeconds(0.5f);
                particles.GetComponent<ParticleSystem>().Stop();
                break;
        }
    }
}