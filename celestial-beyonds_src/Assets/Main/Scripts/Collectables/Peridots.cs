using UnityEngine;

public class Peridots : MonoBehaviour
{
    public AudioClip pickupSound;
    public GameObject miniMenu;
    public int peridotValue = 1;
    public static int peridotsCollectedInLvl;
    private GameObject _player;
    private Rigidbody _rb;
    private bool _hasTarget, _added;
    private Vector3 _targetPos;

    private void Awake()
    {
        peridotsCollectedInLvl = 0;
    }

    private void Start()
    {
        _rb = gameObject.AddComponent<Rigidbody>();
        _rb.useGravity = false;
        _player = GameObject.FindGameObjectWithTag("Player");
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
        PlayerMemory.peridots += peridotValue;
        if (!_added)
        {
            peridotsCollectedInLvl += 1;
            _added = true;
        }
        print("peridotsCollectedInLvl: " + peridotsCollectedInLvl);
    }

    public void SetTarget(Vector3 position)
    {
        _targetPos = position;
        _hasTarget = true;
    }
}