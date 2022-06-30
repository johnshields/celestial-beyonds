using UnityEngine;

namespace Main.Scripts.Captain
{
    public class CaptainFootsteps : MonoBehaviour
    {
        public AudioClip[] footsteps;
        private AudioSource _audio;
        private GameObject _player;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _audio = GetComponent<AudioSource>();
        }

        public void FootstepSounds()
        {
            var rb = _player.GetComponent<Rigidbody>();
            if (rb.velocity.x != 0)
                _audio.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)], 0.1f);
        }
    }
}