using UnityEngine;

public class BeamParticles : MonoBehaviour
{
    public GameObject[] particleBeams;
    public AudioClip moonbeams;
    
    private void ParticleBeams(int status)
    {
        switch (status)
        {
            case 1:
                AudioSource.PlayClipAtPoint(moonbeams, transform.position, 0.2f);
                particleBeams[0].GetComponent<ParticleSystem>().Play();
                particleBeams[1].GetComponent<ParticleSystem>().Play();
                break;
            case 0:
                particleBeams[0].GetComponent<ParticleSystem>().Stop();
                particleBeams[1].GetComponent<ParticleSystem>().Stop();
                break;
        }
    }
}
