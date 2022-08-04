using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AmmoJars : MonoBehaviour
{
    public AudioClip pickupSound;
    public int ammoAmount = 5;
    public GameObject ammo, ammoHandle;
    private Rigidbody _rb;
    private bool _hasTarget;
    private Vector3 _targetPos;

    private void Start()
    {
        _rb = gameObject.AddComponent<Rigidbody>();
        _rb.useGravity = false;
    }
    
    private void FixedUpdate()
    {
        if (!_hasTarget) return;
        var targetDir = (_targetPos - transform.position).normalized;
        _rb.velocity = new Vector3(targetDir.x, targetDir.y, targetDir.z) * 8f;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && ammo.GetComponent<CannonAmmo>().cannonAmmo !=
            ammo.GetComponent<CannonAmmo>().maxAmmo)
        {
            ammo.GetComponent<CannonAmmo>().cannonAmmo += ammoAmount;
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 0.1f);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player") && 
                 ammo.GetComponent<CannonAmmo>().cannonAmmo == ammo.GetComponent<CannonAmmo>().maxAmmo)
        {
            ammoHandle.GetComponent<Image>().color = new Color32(52, 255, 0, 225);
            StartCoroutine(ResetHandleColor());
        }
    }
    
    private IEnumerator ResetHandleColor()
    {
        yield return new WaitForSeconds(1);
        ammoHandle.GetComponent<Image>().color = new Color32(255, 255, 255, 225);
    }
    
    public void SetTarget(Vector3 position)
    {
        _targetPos = position;
        _hasTarget = true;
    }
}
