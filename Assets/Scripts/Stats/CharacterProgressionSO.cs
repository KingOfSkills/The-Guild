using System.Collections.Generic;
using UnityEngine;

namespace TheGuild.Stats
{
    [CreateAssetMenu(fileName = "New Character Stats ProgressionSO", menuName = "Stats/New Character Stats ProgressionSO", order = 1)]
    public class CharacterProgressionSO : ScriptableObject
    {
        public CharacterClass CharacterClass;
        public List<StatProgressionSO> ProgressionSOList;

        public float GetStatValueAtLevel(StatType statType, int level)
        {
            foreach (StatProgressionSO statProgressionSO in ProgressionSOList)
            {
                if (statProgressionSO.StatType == statType)
                {
                    return statProgressionSO.GetValueAtLevel(level);
                }
            }
            return -1;
        }
    }
}
