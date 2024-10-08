using TheGuild.Core;
using TheGuild.Saving;
using TheGuild.Stats;
using UnityEngine;

namespace TheGuild.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        public System.Action OnHealthChanged;

        [SerializeField] private float health = 100f;

        private bool isDead = false;

        private void Start()
        {
            health = GetComponent<BaseStats>().GetHealth();
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0f);
            OnHealthChanged?.Invoke();
            if (health == 0 && !isDead)
            {
                Die();
            }
        }

        public float GetHealthPercentage()
        {
            return health / GetComponent<BaseStats>().GetHealth();
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

        public void Save(string id)
        {
            ES3.Save($"{id}health", health);
            ES3.Save($"{id}isDead", isDead);
        }

        public void Load(string id)
        {
            if (!ES3.KeyExists($"{id}health")) return;

            health = ES3.Load<float>($"{id}health");
            isDead = ES3.Load<bool>($"{id}isDead");

            if (isDead)
            {
                isDead = false;
                Die();
            }
        }
    }
}
