using System.Collections;
using TheGuild.Control;
using UnityEngine;

namespace TheGuild.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private WeaponSO weaponSO;
        [SerializeField] private float respawnTime = 5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                playerController.GetComponent<Fighter>().EquipWeapon(weaponSO);
                StartCoroutine(PickupRoutine());
            }
        }

        private IEnumerator PickupRoutine()
        {
            ShowPickup(false);
            yield return new WaitForSeconds(respawnTime);
            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }
    }
}
