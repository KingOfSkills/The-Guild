using System;
using TheGuild.Combat;
using TheGuild.Core;
using TheGuild.Movement;
using UnityEngine;

namespace TheGuild.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseRadius = 5f;
        [SerializeField] private float suspisiousTime = 5f;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float pauseTime = 3f;

        private Fighter fighter;
        private Health health;
        private Mover mover;

        private GameObject player;

        private Vector3 guardPosition;
        private int currentWaypointIndex = 0;
        private float atWaypointRadius = .25f;
        private float timeAtWaypoint = 0f;

        private float timeSinceLastSawPlayer = 100f;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = FindObjectOfType<PlayerController>().gameObject;

            guardPosition = transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (IsInChaseRadius())
            {
                timeSinceLastSawPlayer = 0f;
                AttackBehaviour();
            }
            else if (!IsInChaseRadius() && timeSinceLastSawPlayer < suspisiousTime)
            {
                SuspisiousBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = patrolPath.GetWaypoint(currentWaypointIndex);

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeAtWaypoint += Time.deltaTime;
                    if (timeAtWaypoint > pauseTime)
                    {
                        GetNextWaypoint();
                        timeAtWaypoint = 0f;
                    }
                }
            }

            mover.StartMoveAction(nextPosition);
        }

        private void GetNextWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            if (Vector3.Distance(transform.position, patrolPath.GetWaypoint(currentWaypointIndex)) < atWaypointRadius)
            {
                return true;
            }
            return false;
        }

        private void SuspisiousBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        public bool IsInChaseRadius()
        {
            return Vector3.Distance(transform.position, player.transform.position) < chaseRadius;
        }

        // Called by Unity
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseRadius);
        }
    }
}
