using System;
using System.Linq;
using Sirenix.OdinInspector;
using UniRx;

namespace Lessons.Architecture.PresentationModel
{
    [Serializable]
    public sealed class CharacterInfoModel
    {
        [ShowInInspector] public readonly ReactiveCollection<CharacterStatModel> stats = new();

        [Button]
        public void AddStat(string name, int value)
        {
            var statModel = new CharacterStatModel(name, value);
            stats.Add(statModel);
        }

        [Button]
        public void AddStat(CharacterStatModel statModel)
        {
            stats.Add(statModel);
        }

        [Button]
        public void RemoveStat(CharacterStatModel statModel)
        {
            stats.Remove(statModel);
        }

        public CharacterStatModel GetStat(string name)
        {
            foreach (var stat in stats)
            {
                if (stat.Name.Value == name)
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