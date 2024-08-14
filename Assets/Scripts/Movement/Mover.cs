using TheGuild.Combat;
using TheGuild.Core;
using TheGuild.Saving;
using UnityEngine;
using UnityEngine.AI;

namespace TheGuild.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] private float maxSpeed = 5.66f;

        private NavMeshAgent navMeshAgent;
        private Health health;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead())
            {
                navMeshAgent.enabled = false;
            }

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            GetComponent<Fighter>().Cancel();
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.SetDestination(destination);
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        public void Save(string id)
        {
            ES3.Save($"{id}transform ", transform);
        }

        public void Load(string id)
        {
            Transform transform = ES3.Load<Transform>($"{id}transform ");

            navMeshAgent.enabled = false;
            this.transform.position = transform.transform.position;
            this.transform.rotation = transform.transform.rotation;
            this.transform.localScale = transform.transform.localScale;
            navMeshAgent.enabled = true;
        }

    }
}
