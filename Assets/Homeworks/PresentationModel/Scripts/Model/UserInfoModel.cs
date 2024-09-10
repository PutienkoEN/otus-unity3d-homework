using System;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Lessons.Architecture.PresentationModel
{
    [Serializable]
    public sealed class UserInfoModel
    {
        [ShowInInspector, ReadOnly] public StringReactiveProperty Name { get; private set; } = new();
        [ShowInInspector, ReadOnly] public StringReactiveProperty Description { get; private set; } = new();
        [ShowInInspector, ReadOnly] public ReactiveProperty<Sprite> Icon { get; private set; } = new();

        [Button]
        public void ChangeName(string name)
        {
            Name.Value = name;
        }

        [Button]
        public void ChangeDescription(string description)
        {
            Description.Value = description;
        }

        [Button]
        public void ChangeIcon(Sprite icon)
        {
            Icon.Value = icon;
        }
    }
}