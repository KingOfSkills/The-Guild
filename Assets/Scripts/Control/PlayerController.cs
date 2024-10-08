using TheGuild.Combat;
using TheGuild.Attributes;
using TheGuild.Movement;
using UnityEngine;

namespace TheGuild.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Fighter fighter;
        private Health health;

        [SerializeField] private LayerMask groundLayer;
        private float distance = 100f;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.TryGetComponent(out CombatTarget target))
                {
                    if (!fighter.CanAttack(target.gameObject))
                    {
                        continue;
                    }

                    if (Input.GetMouseButtonDown(1))
                    {
                        fighter.Attack(target.gameObject);
                    }
                    return true;
                }
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit, distance, groundLayer);
            if (hasHit)
            {
                if (Input.GetMouseButton(1))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
