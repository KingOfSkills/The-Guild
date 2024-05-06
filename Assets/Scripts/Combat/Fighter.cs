using TheGuild.Core;
using TheGuild.Movement;
using UnityEngine;

namespace TheGuild.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float attackRange = 2f;

        Transform target;

        private void Update()
        {
            if (target == null) return;

            if (!IsInAttackRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
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
