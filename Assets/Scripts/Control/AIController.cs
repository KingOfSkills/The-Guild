using System;
using TheGuild.Attributes;
using TheGuild.Combat;
using TheGuild.Core;
using TheGuild.Movement;
using TheGuild.Saving;
using UnityEngine;

namespace TheGuild.Control
{
    public class AIController : MonoBehaviour, ISaveable
    {
        [SerializeField] private float chaseRadius = 5f;
        [SerializeField] private float suspisiousTime = 5f;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float pauseTime = 3f;
        [Range(0f, 1f)]
        [SerializeField] private float patrolSpeedFraction = .27f;

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

        private void SuspisiousBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private void PatrolBehaviour()
        {
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
                else
                {
                    guardPosition = patrolPath.GetWaypoint(currentWaypointIndex);
                }

            }

            mover.StartMoveAction(guardPosition, patrolSpeedFraction);
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

        public bool IsInChaseRadius()
        {
            return Vector3.Distance(transform.position, player.transform.position) < chaseRadius;
        }

        public void Save(string id)
        {
            ES3.Save($"{id}currentWaypointIndex", currentWaypointIndex);
        }

        public void Load(string id)
        {
            if (!ES3.KeyExists($"{id}currentWaypointIndex")) return;

            currentWaypointIndex = ES3.Load<int>($"{id}currentWaypointIndex");
        }

        // Called by Unity
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseRadius);
        }
    }
}
