using Main.Scripts.Captain;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AristauesProfiler : MonoBehaviour
{
    public int aristauesMaxHealth = 3000, AristauesHealth, meleeDamage, cannonDamage;
    public GameObject aHealthBar, vvg;
    public Slider aHealthBarSlider;
    public NavMeshAgent aristaues;
    public Transform player;
    public LayerMask groundMask, playerMask;
    private NavMeshHit _hit;
    private float _distanceToEdge = 5;

    // patrolling
    public Vector3 walkPoint;
    public float walkPointRange;

    // states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, terminated;
    public float delayAction = 2f;

    private Animator _animator;
    private int _idle, _run, _attack_1, _attack_2;
    private bool _walkPointSet, _actionDone, _attackMode;

    // misc
    public GameObject pauseMenu, photoMode;

    private void Awake()
    {
        AristauesHealth = aristauesMaxHealth;
        aHealthBarSlider = aHealthBar.GetComponent<Slider>();
        aristaues = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        _idle = Animator.StringToHash("IdleActive");
        _run = Animator.StringToHash("RunActive");
        _attack_1 = Animator.StringToHash("Attack_1");
        _attack_2 = Animator.StringToHash("Attack_2");
    }

    private void Update()
    {
        // Health
        aHealthBarSlider.value = AristauesHealth;

        if (!terminated)
        {
            var tp = transform.position;
            playerInSightRange = Physics.CheckSphere(tp, sightRange, playerMask);
            playerInAttackRange = Physics.CheckSphere(tp, attackRange, playerMask);

            if (!playerInSightRange && !playerInAttackRange) Patrol();
            if (!pauseMenu.GetComponent<InGameMenus>().pausedActive)
            {
                if (playerInSightRange && !playerInAttackRange && !player.GetComponent<CaptainHealth>().capDead)
                    ChasePlayer();
                if (playerInAttackRange && playerInSightRange && !player.GetComponent<CaptainHealth>().capDead)
                    AttackMode();
                if (player.GetComponent<CaptainHealth>().capDead) 
                    AnimationState(true, false, false);
            }

            // ref: https://forum.unity.com/threads/ai-getting-stuck-into-corner-of-the-map.1213311/
            if (NavMesh.FindClosestEdge(transform.position, out _hit, NavMesh.AllAreas))
                _distanceToEdge = _hit.distance;
            if (_distanceToEdge < 2f) SearchWalkPoint();

            if (photoMode.GetComponent<PhotoMode>().photoMode || vvg.GetComponent<VanGunProfiler>().saleActive)
                aristaues.stoppingDistance = 8;
            else
                aristaues.stoppingDistance = 3;   
        }
    }

    private void AnimationState(bool idle, bool run, bool attack)
    {
        _animator.SetBool(_idle, idle);
        _animator.SetBool(_run, run);
        if(!_attackMode) return;
        var attackBool = Random.Range(0, 2);
        switch (attackBool)
        {
            // attack
            case 0:
                _animator.SetBool(_attack_1, attack);
                break;
            case 1:
                _animator.SetBool(_attack_2, attack);
                break;
        }
    }

    private void Patrol()
    {
        if (!_walkPointSet) SearchWalkPoint();
        if (_walkPointSet)
        {
            AnimationState(false, true, false);
            aristaues.SetDestination(walkPoint);
        }

        var distanceToWalkPoint = transform.position - walkPoint;

        // walkPoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            _walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        // calculate random point in range
        var randomZ = Random.Range(-walkPointRange, walkPointRange);
        var randomX = Random.Range(-walkPointRange, walkPointRange);

        AnimationState(true, false, false);

        // check if point is on ground
        var tp = transform.position;
        walkPoint = new Vector3(tp.x + randomX, tp.y, tp.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
            _walkPointSet = true;
    }

    private void ChasePlayer()
    {
        AnimationState(false, true, false);
        if (aristaues.isActiveAndEnabled)
            aristaues.SetDestination(player.position);
    }

    private void AttackMode()
    {
        // Enemy does not move and looks at player.
        AnimationState(true, false, false);
        if (aristaues.isActiveAndEnabled)
            aristaues.SetDestination(player.position);
        transform.LookAt(player);

        if (!_actionDone)
        {
            _attackMode = true;
            AnimationState(false, false, true);
            Invoke(nameof(ResetAction), delayAction);
        }
    }

    private void ResetAction()
    {
        _actionDone = true;
        if (_attackMode) _attackMode = false;
        Invoke(nameof(ActivateAction), delayAction);
    }

    private void ActivateAction()
    {
        _actionDone = false;
    }

    public void TakeDamage(GameObject enemy)
    {
        if (player.GetComponent<CaptainAnimAndSound>().meleeActive)
            AristauesHealth -= meleeDamage;
        else if (player.GetComponent<CaptainAnimAndSound>().cannonFire)
            AristauesHealth -= cannonDamage;

        print(enemy.name + " Health: " + AristauesHealth);

        if (AristauesHealth <= 0 && !terminated)
        {
            terminated = true;
            print(enemy.name + " Terminated!");
        }
    }
}