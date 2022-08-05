using UnityEngine;

public class Peridots : MonoBehaviour
{
    public AudioClip pickupSound;
    public GameObject miniMenu;
    public int peridotValue = 1;
    private Component _peridotCounter;
    private GameObject _player;
    private Rigidbody _rb;
    private bool _hasTarget;
    private Vector3 _targetPos;

    private void Start()
    {
        _rb = gameObject.AddComponent<Rigidbody>();
        _rb.useGravity = false;
        _player = GameObject.FindGameObjectWithTag("Player");
        _peridotCounter = _player.GetComponent<PeridotCounter>();
    }

    private void FixedUpdate()
    {
        // ref - https://youtu.be/x8_LJ22QnlE
        if (!_hasTarget || gameObject.name == "BigPeridot") return;
        var targetDir = (_targetPos - transform.position).normalized;
        _rb.velocity = new Vector3(targetDir.x, targetDir.y, targetDir.z) * 8f;
    }

    // Allows only the Player to collect the _peridotCounter.
    // Plus adds collected peridots to the _peridotCounter.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _player) return;
        AudioSource.PlayClipAtPoint(pickupSound,  transform.position, 0.1f);
        // Destroy peridot and add to the _peridotCounter
        Destroy(gameObject);
        _peridotCounter.GetComponent<PeridotCounter>().peridots += peridotValue;
        miniMenu.GetComponent<MiniMenu>().peridotsNum += 1;
    }

    public void SetTarget(Vector3 position)
    {
        _targetPos = position;
        _hasTarget = true;
    }
}