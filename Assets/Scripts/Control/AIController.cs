using TheGuild.Combat;
using TheGuild.Core;
using UnityEngine;

namespace TheGuild.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseRadius = 5f;

        private Fighter fighter;
        private GameObject player;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = FindObjectOfType<PlayerController>().gameObject;
        }

        private void Update()
        {
            if (GetComponent<Health>().IsDead()) return;

            if (IsInChaseRadius())
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        public bool IsInChaseRadius()
        {
            return Vector3.Distance(transform.position, player.transform.position) < chaseRadius;
        }
    }
}
