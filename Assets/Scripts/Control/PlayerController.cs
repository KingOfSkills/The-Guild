using TheGuild.Movement;
using UnityEngine;

namespace TheGuild.Control
{

    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButton(1))
            {
                MoveToMouse();
            }
        }

        private void MoveToMouse()
        {
            RaycastHit hit;
            Ray lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(lastRay, out hit);
            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }
    }
}
