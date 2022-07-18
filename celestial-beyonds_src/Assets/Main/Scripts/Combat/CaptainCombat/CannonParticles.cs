using System.Text.RegularExpressions;
using Main.Scripts.Enemies;
using UnityEngine;

public class CannonParticles : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        print("Cannon hit: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyProfiler>().TakeDamage(other.gameObject);
            other.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }
        else if (other.gameObject.CompareTag("CollectableBox"))
        {
            other.gameObject.GetComponent<CollectableBox>().IfCannon();
        }
        
        // To avoid conflict with calling SpiderWebs
        var spiderObjTxt = Regex.Replace(other.gameObject.name, "^[a-zA-Z]+$", "");
        if (other.name == spiderObjTxt && other.gameObject.CompareTag("Enemy"))
        {
            // causes conflict when hit other enemies
            //var spiderBlood = "Enemies/Spiders/" + other.name + "/spider/BloodParticle";   
            //GameObject.Find(spiderBlood).GetComponent<ParticleSystem>().Play();
        }
        else
            print("Not a spider.");
    }
}