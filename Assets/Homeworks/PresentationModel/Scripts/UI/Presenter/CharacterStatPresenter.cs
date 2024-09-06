using System;
using Object = UnityEngine.Object;

namespace Lessons.Architecture.PM
{
    public class CharacterStatPresenter : IDisposable
    {
        private readonly CharacterStatModel characterStatModel;
        private readonly CharacterStatView characterStatView;

        public CharacterStatPresenter(CharacterStatModel characterStatModel, CharacterStatView characterStatView)
        {
            this.characterStatModel = characterStatModel;
            this.characterStatView = characterStatView;

            characterStatModel.OnValueChanged += UpdateStatValue;
            UpdateStatValue(characterStatModel.Value);
        }

        private void UpdateStatValue(int statValue)
        {
            var newStatText = $"{characterStatModel.Name} : {characterStatModel.Value}";
            characterStatView.UpdateStatValue(newStatText);
        }

        public bool IsSameStat(CharacterStatModel characterStatModelToCheck)
        {
            return characterStatModel == characterStatModelToCheck;
        }

        public void Dispose()
        {
            Object.Destroy(characterStatView);
            characterStatModel.OnValueChanged -= UpdateStatValue;
        }
    }
}