using System.Collections.Generic;
using UnityEngine;

namespace TheGuild.Stats
{
    [CreateAssetMenu(fileName = "New Stat ProgressionSO", menuName = "Stats/New Stat ProgressionSO", order = 0)]
    public class StatProgressionSO : ScriptableObject
    {
        public StatType StatType;
        public List<float> StatProgressionList;

        public float GetValueAtLevel(int level)
        {
            return StatProgressionList[level - 1];
        }
    }

    public class CharacterStat
    {
        public readonly StatType StatType;
        public readonly float Value;
    }

    public enum StatType
    {
        Health,
        Strength,
        Agility,
        Intelligence,
    }
}
