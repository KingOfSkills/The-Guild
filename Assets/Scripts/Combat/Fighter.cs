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

        private Health target;
        private float timeSinceLastAttack = 0f;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null || target.IsDead()) return;

            if (!IsInAttackRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
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
            if (target == null) return;
            target.TakeDamage(weaponDamage);
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public bool IsInAttackRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= attackRange;
        }

        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("cancelAttack");
            target = null;
        }
    }
}
