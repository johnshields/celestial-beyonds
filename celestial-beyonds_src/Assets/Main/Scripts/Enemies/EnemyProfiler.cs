using Main.Scripts.Captain;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Main.Scripts.Enemies
{
    public class EnemyProfiler : MonoBehaviour
    {
        public int enemyMaxHealth = 3, enemyHealth, meleeDamage, cannonDamage;
        public NavMeshAgent agent;
        public Transform player;
        public LayerMask groundMask, playerMask;
        private NavMeshHit _hit;
        private float _distanceToEdge = 5;

        // patrolling
        public Vector3 walkPoint;
        public float walkPointRange;

        // states
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;
        public float delayAction = 2f;

        private Animator _animator;
        private int _idle, _walk, _attack;
        private bool _walkPointSet, _actionDone;

        // misc
        public GameObject miniMenu;

        private void Awake()
        {
            enemyHealth = enemyMaxHealth;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
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
            if (playerInSightRange && !playerInAttackRange && !player.GetComponent<CaptainHealth>().capDead)
                ChasePlayer();
            if (playerInAttackRange && playerInSightRange && !player.GetComponent<CaptainHealth>().capDead)
                AttackMode();
            if (player.GetComponent<CaptainHealth>().capDead) Patrol();

            // ref: https://forum.unity.com/threads/ai-getting-stuck-into-corner-of-the-map.1213311/
            if (NavMesh.FindClosestEdge(transform.position, out _hit, NavMesh.AllAreas))
                _distanceToEdge = _hit.distance;
            if (_distanceToEdge < 2f) SearchWalkPoint();
        }

        private void AnimationState(bool idle, bool walk, bool attack)
        {
            _animator.SetBool(_idle, idle);
            _animator.SetBool(_walk, walk);
            _animator.SetBool(_attack, attack);
        }

        private void Patrol()
        {
            if (!_walkPointSet) SearchWalkPoint();

            if (_walkPointSet)
            {
                agent.SetDestination(walkPoint);
                AnimationState(false, true, false);
            }

            var distanceToWalkPoint = transform.position - walkPoint;

            // walkPoint reached
            if (distanceToWalkPoint.magnitude < 1f)
                _walkPointSet = false;
        }

        private void SearchWalkPoint()
        {
            // calculate random point in range
            AnimationState(true, false, false);
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
            if (agent.isActiveAndEnabled)
                agent.SetDestination(player.position);
        }

        private void AttackMode()
        {
            // Enemy does not move and looks at player.
            AnimationState(true, false, false);
            if (agent.isActiveAndEnabled)
                agent.SetDestination(player.position);
            transform.LookAt(player);

            if (!_actionDone)
            {
                AnimationState(false, false, true);
                Invoke(nameof(ResetAction), delayAction);
            }
        }

        private void ResetAction()
        {
            _actionDone = true;
            Invoke(nameof(ActivateAction), delayAction);
        }

        private void ActivateAction()
        {
            _actionDone = false;
        }

        public void TakeDamage(GameObject enemy)
        {
            if (player.GetComponent<CaptainAnimAndSound>().meleeActive)
                enemyHealth -= meleeDamage;
            else if (player.GetComponent<CaptainAnimAndSound>().cannonFire)
                enemyHealth -= cannonDamage;

            print(enemy.name + " Health: " + enemyHealth);

            if (enemyHealth <= 0)
            {
                print(enemy.name + " Terminated!");
                miniMenu.GetComponent<MiniMenu>().enemiesNum += 1;
                Destroy(enemy);
            }
        }
    }
}