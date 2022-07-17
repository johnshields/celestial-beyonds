using System.Text.RegularExpressions;
using Main.Scripts.Captain;
using Main.Scripts.Enemies;
using UnityEngine;

/*
 * CaptainCombat
 * Attached to Player's Scraper - triggered when it hits enemies.
 */
namespace Main.Scripts.Combat
{
    public class CaptainCombat : MonoBehaviour
    {
        private GameObject _player;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy") && _player.GetComponent<CaptainAnimAndSound>().meleeActive)
            {
                other.gameObject.GetComponent<EnemyProfiler>().TakeDamage(other.gameObject);
                other.gameObject.GetComponentInChildren<ParticleSystem>().Play();
                
                // To avoid conflict with calling SpiderWebs
                var spiderObjTxt = Regex.Replace(other.gameObject.name, "^[a-zA-Z]+$", "");
                if (other.name == spiderObjTxt)
                {
                    var spiderBlood = "Enemies/Spiders/" + other.name + "/spider/BloodParticle";   
                    GameObject.Find(spiderBlood).GetComponent<ParticleSystem>().Play();
                }
                else
                    print("Not a spider");
            }
        }
    }
}