using TheGuild.Core;
using TheGuild.Movement;
using UnityEngine;

namespace TheGuild.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float attackCooldown = 3f;
        [SerializeField] private float weaponDamage = 20f;

        private Transform target;
        private float timeSinceLastAttack = 0f;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (!IsInAttackRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack >= attackCooldown)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
        }

        // Animation Event
        private void Hit()
        {
            if (target.TryGetComponent(out Health health))
            {
                health.TakeDamage(weaponDamage);
            }
            else
            {
                Debug.Log("Cannot deal damage");
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public bool IsInAttackRange()
        {
            return Vector3.Distance(transform.position, target.position) <= attackRange;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}
