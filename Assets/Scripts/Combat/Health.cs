using UnityEngine;

namespace TheGuild.Combat
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
                GetComponent<Animator>().SetTrigger("die");
                isDead = true;
            }
            Debug.Log($"Remaining health: {health}");
        }

        public bool IsDead()
        {
            return isDead;
        }
    }
}
