using System;
using Sirenix.OdinInspector;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterStatModel
    {
        public event Action<int> OnValueChanged;
        [ShowInInspector, ReadOnly] public string Name { get; private set; }
        [ShowInInspector, ReadOnly] public int Value { get; private set; }

        public CharacterStatModel(string name, int value)
        {
            Name = name;
            Value = value;
        }

        [Button]
        public void ChangeValue(int value)
        {
            Value = value;
            OnValueChanged?.Invoke(value);
        }
    }
}