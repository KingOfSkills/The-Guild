using TheGuild.Core;
using TheGuild.Movement;
using UnityEngine;

namespace TheGuild.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private Transform handPostition;
        [SerializeField] private WeaponSO currentWeaponSO = null;

        private Health target;
        private float timeSinceLastAttack = 100f;

        private void Start()
        {
            EquipWeapon(currentWeaponSO);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null || target.IsDead()) return;

            if (!IsInAttackRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        public void EquipWeapon(WeaponSO weaponSO)
        {
            currentWeaponSO = weaponSO;
            weaponSO.Spawn(handPostition, GetComponent<Animator>());
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack >= currentWeaponSO.GetCooldown())
            {
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("cancelAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        // Animation Event
        private void Hit()
        {
            if (target == null) return;
            target.TakeDamage(currentWeaponSO.GetDamage());
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            Health targetHealh = combatTarget.GetComponent<Health>();
            return !targetHealh.IsDead() && targetHealh != null;
        }

        public bool IsInAttackRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= currentWeaponSO.GetRange();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("cancelAttack");
        }
    }
}
