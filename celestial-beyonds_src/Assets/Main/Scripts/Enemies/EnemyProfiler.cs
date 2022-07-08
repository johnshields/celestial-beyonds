using UnityEngine;
using UnityEngine.AI;

namespace Main.Scripts.Enemies
{
    public class EnemyProfiler : MonoBehaviour
    {
        public NavMeshAgent agent;
        public Transform player;
        public LayerMask groundMask, playerMask;
        public int enemyMaxHealth = 3, enemyHealth;

        // patrolling
        public Vector3 walkPoint;
        public float walkPointRange;

        // states
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;
        public float delayAction = 1f;
        public AudioClip[] enemySFX;

        private Animator _animator;
        private AudioSource _audio;
        private int _idle, _walk, _attack;
        private bool _walkPointSet, _actionDone;

        private void Awake()
        {
            enemyHealth = enemyMaxHealth;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            _audio = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
            _idle = Animator.StringToHash("IdleActive");
            _walk = Animator.StringToHash("WalkActive");
            _attack = Animator.StringToHash("AttackActive");
        }

        private void Update()
        {
            var tp = transform.position;
            playerInSightRange = Physics.CheckSphere(tp, sightRange, playerMask);
            playerInAttackRange = Physics.CheckSphere(tp, attackRange, playerMask);

            if (!playerInSightRange && !playerInAttackRange) Patrol();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange && !CaptainHealth.capDead) AttackMode();
        }

        private void AnimationState(bool idle, bool walk, bool attack)
        {
            _animator.SetBool(_idle, idle);
            _animator.SetBool(_walk, walk);
            _animator.SetBool(_attack, attack);
        }

        private void Patrol()
        {
            sightRange = 20f;

            if (!_walkPointSet) SearchWalkPoint();

            if (_walkPointSet)
            {
                agent.SetDestination(walkPoint);
                AnimationState(true, false, false);
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
            var tp = transform.position;
            walkPoint = new Vector3(tp.x + randomX, tp.y, tp.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
                _walkPointSet = true;
        }

        private void ChasePlayer()
        {
            AnimationState(false, true, false);
            agent.SetDestination(player.position);
            sightRange = 30f;
        }

        private void AttackMode()
        {
            // Enemy does not move and looks at player.
            AnimationState(true, false, false);
            agent.SetDestination(transform.position);
            transform.LookAt(player);

            if (!_actionDone)
            {
                AnimationState(false, false, true);
                _audio.PlayOneShot(enemySFX[0]);
                _actionDone = true;
                Invoke(nameof(ResetAction), delayAction);
            }
        }

        private void ResetAction()
        {
            _actionDone = false;
        }

        public void TakeDamage(int amount, GameObject enemy)
        {
            enemyHealth -= amount;
            print(enemy.name + " Health: " + enemyHealth);

            if (enemyHealth <= 0)
            {
                print(enemy.name + " Terminated!");
                var position = transform.position;
                AudioSource.PlayClipAtPoint(enemySFX[1], position, 0.1f);
                Destroy(enemy);
            }
        }
    }
}