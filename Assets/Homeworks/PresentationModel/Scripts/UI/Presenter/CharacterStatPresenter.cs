using System;
using UniRx;
using Zenject;

namespace Lessons.Architecture.PresentationModel
{
    public class CharacterStatPresenter : IInitializable, IDisposable
    {
        private readonly CompositeDisposable compositeDisposable = new();

        private readonly CharacterStatModel characterStatModel;
        private readonly CharacterStatView characterStatView;
        public event Action<CharacterStatView> OnRemove;

        public CharacterStatPresenter(
            CharacterStatModel characterStatModel,
            CharacterStatView characterStatView,
            Action<CharacterStatView> remove)
        {
            this.characterStatModel = characterStatModel;
            this.characterStatView = characterStatView;
            this.OnRemove += remove;
        }

        public void Initialize()
        {
            characterStatModel.Value
                .Subscribe(UpdateStatValue)
                .AddTo(compositeDisposable);

            UpdateStatValue(characterStatModel.Value.Value);
        }

        private void UpdateStatValue(int statValue)
        {
            var newStatText = $"{characterStatModel.Name} : {characterStatModel.Value}";
            characterStatView.UpdateStatValue(newStatText);
        }

        public void Dispose()
        {
            OnRemove?.Invoke(characterStatView);
            compositeDisposable.Dispose();
        }
    }
}