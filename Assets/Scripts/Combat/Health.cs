using UnityEngine;

namespace TheGuild.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float health = 100f;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0f);
            if (health == 0)
            {
                GetComponent<Animator>().SetTrigger("die");
            }
            Debug.Log($"Remaining health: {health}");
        }
    }
}
