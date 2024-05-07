using UnityEngine;

namespace TheGuild.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float health = 100f;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0f);
            Debug.Log($"Remaining health: {health}");
        }
    }
}
