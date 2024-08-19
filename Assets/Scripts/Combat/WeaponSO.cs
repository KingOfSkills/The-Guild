using UnityEngine;

namespace TheGuild.Combat
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponSO : ScriptableObject
    {
        [SerializeField] private GameObject weaponPrefab = null;
        [SerializeField] private AnimatorOverrideController weaponAnimatorOverride = null;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float attackCooldown = 3f;
        [SerializeField] private float weaponDamage = 20f;

        public void Spawn(Transform handPostition, Animator animator)
        {
            if (weaponPrefab != null)
            {
                Instantiate(weaponPrefab, handPostition);
            }

            if (weaponAnimatorOverride != null)
            {
                animator.runtimeAnimatorController = weaponAnimatorOverride;
            }
        }

        public float GetRange()
        {
            return attackRange;
        }

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetCooldown()
        {
            return attackCooldown;
        }
    }
}
