using TheGuild.Core;
using UnityEngine;

namespace TheGuild.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private bool isHoming;
        [SerializeField] private float lifeTime = 5f;

        private Health target;
        private float damage;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            MoveTowardsTarget();
        }

        private void MoveTowardsTarget()
        {
            if (target != null)
            {
                if (isHoming && !target.IsDead())
                {
                    transform.LookAt(GetAimLocation());
                }
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
            transform.LookAt(GetAimLocation());
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider capsuleCollider = target.GetComponent<CapsuleCollider>();
            if (capsuleCollider == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * (capsuleCollider.height / 2);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Health targetHealth))
            {
                if (target == targetHealth && !target.IsDead())
                {
                    target.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
