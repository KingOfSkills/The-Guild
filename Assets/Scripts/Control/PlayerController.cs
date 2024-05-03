using TheGuild.Combat;
using TheGuild.Movement;
using UnityEngine;

namespace TheGuild.Control
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            InteractWithMovement();
            InteractWithCombat();
        }

        private void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.TryGetComponent(out CombatTarget target))
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        GetComponent<Fighter>().Attack(target);
                    }
                }
            }
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(1))
            {
                MoveToMouse();
            }
        }

        private void MoveToMouse()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
