using Unity.VisualScripting;
using UnityEngine;

namespace TheGuild.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] private bool isCircular;

        private float gizmosSphereRadius = .5f;
        private bool isBackTracking = false;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(GetWaypoint(i), gizmosSphereRadius);

                if (isCircular)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(GetNextIndex(i)));
                }
                else
                {
                    if (i < transform.childCount - 1)
                    {
                        Gizmos.color = Color.white;
                        Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
                    }
                }
            }
        }

        public int GetNextIndex(int i)
        {
            if (!isCircular && isBackTracking)
            {
                int k = i - 1;
                if (k < 0)
                {
                    isBackTracking = false;
                    return 0;
                }
                return k;
            }
            else
            {
                int k = i + 1;
                if (k == transform.childCount)
                {
                    if (isCircular)
                    {
                        return 0;
                    }
                    isBackTracking = true;
                    return k - 2;
                }
                return k;
            }
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }

        public bool IsCircular()
        {
            return isCircular;
        }
    }
}
