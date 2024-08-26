using UnityEngine;

namespace TheGuild.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] private int startingLevel = 1;
        [SerializeField] private CharacterClass characterClass;
        [SerializeField] private CharacterProgressionSO characterProgressionSO;

        public float GetHealth()
        {
            return characterProgressionSO.GetStatValueAtLevel(StatType.Health, startingLevel);
        }
    }
}
