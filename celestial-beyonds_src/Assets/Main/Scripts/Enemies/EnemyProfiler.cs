using System.Collections;
using Main.Scripts.Combat;
using UnityEngine;
using UnityEngine.AI;

namespace Main.Scripts.Enemies
{
    public class EnemyProfiler : MonoBehaviour
    {
        public NavMeshAgent agent;
        public Transform player;
        public LayerMask groundMask, playerMask;

        // patrolling
        public Vector3 walkPoint;
        public float walkPointRange;

        // states
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;
        private bool _walkPointSet, _actionDone;
        public float delayAction = 1f;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            var tp = transform.position;
            playerInSightRange = Physics.CheckSphere(tp, sightRange, playerMask);
            playerInAttackRange = Physics.CheckSphere(tp, attackRange, playerMask);

            if (!playerInSightRange && !playerInAttackRange) Patrol();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackMode();
        }

        private void Patrol()
        {
            sightRange = 20f;

            if (!_walkPointSet) SearchWalkPoint();

            if (_walkPointSet)
                agent.SetDestination(walkPoint);

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
            agent.SetDestination(player.position);
            sightRange = 30f;
        }

        private void AttackMode()
        {
            // Enemy does not move and looks at player.
            agent.SetDestination(transform.position);
            transform.LookAt(player);

            if (!_actionDone && CombatManager.playerHealth >= 0)
            {
                print("Enemy attacked!");
                _actionDone = true;
                Invoke(nameof(ResetAction), delayAction);
            }
        }

        private void ResetAction()
        {
            _actionDone = false;
        }
    }
}