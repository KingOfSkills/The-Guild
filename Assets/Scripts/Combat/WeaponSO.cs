using System;
using TheGuild.Core;
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
        [SerializeField] private bool isRightHanded = true;
        [SerializeField] private Projectile projectile = null;

        private const string weaponName = "Weapon";

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyWeapon(rightHand, leftHand);

            if (weaponPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                GameObject newWeapon = Instantiate(weaponPrefab, handTransform);
                newWeapon.name = weaponName;
            }

            if (weaponAnimatorOverride != null)
            {
                animator.runtimeAnimatorController = weaponAnimatorOverride;
            }
        }

        private void DestroyWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }

            if (oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            return (isRightHanded) ? rightHand : leftHand;
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
