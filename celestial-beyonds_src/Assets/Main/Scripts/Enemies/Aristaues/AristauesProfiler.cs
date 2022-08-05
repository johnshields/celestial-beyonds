using System.Collections;
using Main.Scripts.Captain;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/*
 * ref - https://youtu.be/UjkSFoLxesw
 */
public class AristauesProfiler : MonoBehaviour
{
    public int aristauesMaxHealth = 3000, AristauesHealth, meleeDamage, cannonDamage;
    public GameObject aHealthBar, vvg, fader;
    public Slider aHealthBarSlider;
    public NavMeshAgent aristaues;
    public Transform player;
    public LayerMask groundMask, playerMask;

    // patrolling
    public Vector3 walkPoint;
    public float walkPointRange;
    private bool _walkPointSet;


    // states
    public float sightRange, attackRange, delayAction = 3;
    public bool playerInSightRange, playerInAttackRange, terminated;
    private bool _actionDone;

    private Animator _animator;
    private int _idle, _walk, _run, _attack_1, _attack_2, _fall;

    // misc
    public GameObject pauseMenu, photoMode;
    private Rigidbody _rb;

    private void Awake()
    {
        AristauesHealth = aristauesMaxHealth;
        aHealthBarSlider = aHealthBar.GetComponent<Slider>();
        aristaues = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        _idle = Animator.StringToHash("IdleActive");
        _walk = Animator.StringToHash("WalkActive");
        _run = Animator.StringToHash("RunActive");
        _attack_1 = Animator.StringToHash("Attack_1");
        _attack_2 = Animator.StringToHash("Attack_2");
        _fall = Animator.StringToHash("Fall");
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

            if (!pauseMenu.GetComponent<InGameMenus>().pausedActive && !player.GetComponent<CaptainHealth>().capDead)
            {
                if (!playerInSightRange && !playerInAttackRange) Patrol();
                if (playerInSightRange && !playerInAttackRange) ChasePlayer();
                if (playerInAttackRange && playerInSightRange) AttackMode();
            }

            if (!pauseMenu.GetComponent<InGameMenus>().pausedActive || !photoMode.GetComponent<PhotoMode>().photoMode)
            {
                if (playerInSightRange && !playerInAttackRange && !player.GetComponent<CaptainHealth>().capDead)
                    ChasePlayer();
                if (playerInAttackRange && playerInSightRange && !player.GetComponent<CaptainHealth>().capDead)
                    AttackMode();
                if (player.GetComponent<CaptainHealth>().capDead)
                {
                    AnimationState(true, false, false, false, false);
                    _rb.constraints = RigidbodyConstraints.FreezeAll;
                }
            }

            // Stop Aristaues during PhotoMode.
            if (photoMode.GetComponent<PhotoMode>().photoMode)
            {
                aristaues.stoppingDistance = 15;   
                AnimationState(true, false, false, false, false);
            }
            else
                aristaues.stoppingDistance = 3;
        }
    }

    private void AnimationState(bool idle, bool walk, bool run, bool attack1, bool attack2)
    {
        _animator.SetBool(_idle, idle);
        _animator.SetBool(_walk, walk);
        _animator.SetBool(_run, run);
        _animator.SetBool(_attack_1, attack1);
        _animator.SetBool(_attack_2, attack2);
    }

    private void Patrol()
    {
        if (!_walkPointSet) SearchWalkPoint();

        if (_walkPointSet)
        {
            aristaues.SetDestination(walkPoint);
            AnimationState(false, true, false, false, false);
        }

        var distanceToWalkPoint = transform.position - walkPoint;

        // walkPoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            _walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // calculate random point in range
        var randomZ = Random.Range(-walkPointRange, walkPointRange);
        var randomX = Random.Range(-walkPointRange, walkPointRange);

        // check if point is on ground
        var position = transform.position;
        walkPoint = new Vector3(position.x + randomX, position.y, position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
            _walkPointSet = true;
    }


    private void ChasePlayer()
    {
        if (aristaues.isActiveAndEnabled)
        {
            AnimationState(false, false, true, false, false);
            aristaues.SetDestination(player.position);
            print("Chase");
        }
    }

    private void AttackMode()
    {
        // Enemy does not move and looks at player
        aristaues.SetDestination(player.position);
        transform.LookAt(player);
        AnimationState(false, true, false, false, false);

        if (!_actionDone)
        {
            var attackBool = Random.Range(0, 2);
            switch (attackBool)
            {
                case 0:
                    print("Attack 1");
                    AnimationState(false, false, false, true, false);
                    StartCoroutine(AttackComplete(delayAction));
                    break;
                case 1:
                    print("Attack 2");
                    AnimationState(false, false, false, false, true);
                    StartCoroutine(AttackComplete(delayAction));
                    break;
            }
        }
    }
    
    private IEnumerator AttackComplete(float time)
    {
        yield return new WaitForSeconds(time);
        _actionDone = true;
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1f);
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
            print(enemy.name + " Defeated!");
            AnimationState(true, false, false, false, false);
            _animator.SetTrigger(_fall);
        }
    }

    private void CallGR()
    {
        StartCoroutine(LoadGoldenRecord());
    }
    
    private IEnumerator LoadGoldenRecord()
    {
        print("Loading: 010_TheGoldenRecord...");
        fader.GetComponent<Animator>().SetBool($"FadeIn", false);
        fader.GetComponent<Animator>().SetBool($"FadeOut", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("010_TheGoldenRecord");
    }
}