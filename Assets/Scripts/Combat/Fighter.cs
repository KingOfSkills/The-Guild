using TheGuild.Movement;
using UnityEngine;

namespace TheGuild.Combat
{
    public class Fighter : MonoBehaviour
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
                GetComponent<Mover>().Stop();
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
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
