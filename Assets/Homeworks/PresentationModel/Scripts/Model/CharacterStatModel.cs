using System;
using Sirenix.OdinInspector;
using UniRx;

namespace Lessons.Architecture.PresentationModel
{
    [Serializable]
    public sealed class CharacterStatModel
    {
        [ShowInInspector, ReadOnly] public StringReactiveProperty Name { get; private set; } = new();
        [ShowInInspector, ReadOnly] public IntReactiveProperty Value { get; private set; } = new();

        public CharacterStatModel(string name, int value)
        {
            Name.Value = name;
            Value.Value = value;
        }

        [Button]
        public void ChangeValue(int value)
        {
            Value.Value = value;
        }
    }
}