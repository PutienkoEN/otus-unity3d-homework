using System;
using Sirenix.OdinInspector;
using UniRx;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public sealed class CharacterLevelModel
    {
        [ShowInInspector, ReadOnly] public IntReactiveProperty CurrentLevel { get; private set; } = new(1);
        [ShowInInspector, ReadOnly] public IntReactiveProperty CurrentExperience { get; private set; } = new();
        [ShowInInspector, ReadOnly] public IReadOnlyReactiveProperty<int> RequiredExperience { get; private set; }
        [ShowInInspector, ReadOnly] public IReadOnlyReactiveProperty<bool> CanLevelUp { get; private set; }

        public CharacterLevelModel()
        {
            RequiredExperience = CurrentLevel
                .Select(NextLevelExperience)
                .ToReactiveProperty();

            CanLevelUp = CurrentExperience
                .CombineLatest(RequiredExperience, IsLevelUpAllowed)
                .ToReactiveProperty();
        }

        [Button]
        public void AddExperience(int range)
        {
            var xp = Math.Min(CurrentExperience.Value + range, RequiredExperience.Value);
            CurrentExperience.Value = xp;
        }

        [Button]
        public void LevelUp()
        {
            if (!CanLevelUp.Value)
            {
                return;
            }

            CurrentExperience.Value = 0;
            CurrentLevel.Value++;
        }

        private bool IsLevelUpAllowed(int currentExperience, int requiredExperience)
        {
            return CurrentExperience.Value == RequiredExperience.Value;
        }

        private int NextLevelExperience(int currentLevel)
        {
            return 100 * (currentLevel + 1);
        }
    }
}