using TheGuild.Saving;
using UnityEngine;

namespace TheGuild.Core
{
    public class Health : MonoBehaviour, ISaveable
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

        public void Save(string id)
        {
            ES3.Save($"{id}health", health);
            ES3.Save($"{id}isDead", isDead);
        }

        public void Load(string id)
        {
            health = ES3.Load<float>($"{id}health");
            isDead = ES3.Load<bool>($"{id}isDead");

            PostLoad();
        }

        public void PostLoad()
        {
            TakeDamage(0);
        }
    }
}
