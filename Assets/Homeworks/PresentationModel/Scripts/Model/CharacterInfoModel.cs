using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public sealed class CharacterInfoModel
    {
        public event Action<CharacterStatModel> OnStatAdded;
        public event Action<CharacterStatModel> OnStatRemoved;

        [ShowInInspector] private readonly HashSet<CharacterStatModel> stats = new();

        [Button]
        public void AddStat(string name, int value)
        {
            var characterStatModel = new CharacterStatModel(name, value);
            if (stats.Add(characterStatModel))
            {
                OnStatAdded?.Invoke(characterStatModel);
            }
        }

        [Button]
        public void AddStat(CharacterStatModel statModel)
        {
            if (stats.Add(statModel))
            {
                OnStatAdded?.Invoke(statModel);
            }
        }

        [Button]
        public void RemoveStat(CharacterStatModel statModel)
        {
            if (stats.Remove(statModel))
            {
                OnStatRemoved?.Invoke(statModel);
            }
        }

        public CharacterStatModel GetStat(string name)
        {
            foreach (var stat in stats)
            {
                if (stat.Name == name)
                {
                    return stat;
                }
            }

            throw new Exception($"Stat {name} is not found!");
        }

        public CharacterStatModel[] GetStats()
        {
            return stats.ToArray();
        }
    }
}