using TheGuild.Control;
using UnityEngine;

namespace TheGuild.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private WeaponSO weaponSO;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                playerController.GetComponent<Fighter>().EquipWeapon(weaponSO);
                Destroy(gameObject);
            }
        }
    }
}
