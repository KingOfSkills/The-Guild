using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    private NavMeshAgent agent;
    private Ray lastRay;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MoveToMouse();
        }
    }

    private void MoveToMouse()
    {
        RaycastHit hit;
        lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hasHit = Physics.Raycast(lastRay, out hit);
        if (hasHit)
        {
            agent.SetDestination(hit.point);
            Debug.DrawRay(lastRay.origin, lastRay.direction * 25f);
        }
    }
}
