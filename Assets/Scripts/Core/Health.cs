using UnityEngine;

namespace TheGuild.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float health = 100f;

        private bool isDead = false;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0f);
            if (health == 0 && !isDead)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public bool IsDead()
        {
            return isDead;
        }
    }
}
