using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Main.Scripts.Moonbeam
{
    public class MoonbeamProfiler : MonoBehaviour
    {
        public NavMeshAgent agent;
        private Transform _target;
        public LayerMask groundMask, playerMask;
        
        // wandering
        public Vector3 walkPoint;
        public float walkPointRange;

        // states
        public float sightRange, followRange;
        public bool playerInSightRange;
        public bool playerInFollowRange;

        // follow
        private bool _walkPointSet;

        private void Awake()
        {
            _target = GameObject.FindGameObjectWithTag("Target").transform;
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            var position = transform.position;
            playerInSightRange = Physics.CheckSphere(position, sightRange, playerMask);
            playerInFollowRange = Physics.CheckSphere(position, followRange, playerMask);

            if (!playerInSightRange && !playerInFollowRange) Wander();
            if (playerInSightRange && !playerInFollowRange) Follow();
            if (playerInFollowRange && playerInSightRange) SideBySide();
            
            transform.LookAt(_target);
        }

        private void Wander()
        {
            if (!_walkPointSet) SearchWalkPoint();

            if (_walkPointSet)
            {
                agent.SetDestination(walkPoint);
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

        private void Follow()
        {
            agent.SetDestination(_target.position);
        }

        private void SideBySide()
        {
            agent.SetDestination(transform.position);
        }
    }
}