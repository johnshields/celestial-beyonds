using UnityEngine;

public class BeamParticles : MonoBehaviour
{
    public GameObject[] particleBeams;
    
    private void ParticleBeams(int status)
    {
        switch (status)
        {
            case 1:
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
