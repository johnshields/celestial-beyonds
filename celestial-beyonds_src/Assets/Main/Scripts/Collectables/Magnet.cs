using UnityEngine;

public class Magnet : MonoBehaviour
{
    private GameObject _cap, _ammo;

    private void Start()
    {
        _cap = GameObject.FindGameObjectWithTag("Player");
        _ammo = GameObject.Find("Managers/Cannon/CannonAmmo");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Peridots>(out var peridots))
            peridots.SetTarget(transform.parent.position);
        
        if (other.TryGetComponent<HoneyJars>(out var honeyJars) && 
            _cap.GetComponent<CaptainHealth>().currentHealth != _cap.GetComponent<CaptainHealth>().maxHealth)
            honeyJars.SetTarget(transform.parent.position);   
        
        if (other.TryGetComponent<AmmoJars>(out var ammoJars) &&
            _ammo.GetComponent<CannonAmmo>().maxAmmo != _ammo.GetComponent<CannonAmmo>().cannonAmmo)
            ammoJars.SetTarget(transform.parent.position);
    }
}
