using System;
using UniRx;
using Zenject;
using Object = UnityEngine.Object;

namespace Lessons.Architecture.PresentationModel
{
    public class CharacterStatPresenter : IInitializable, IDisposable
    {
        private readonly CompositeDisposable compositeDisposable = new();

        private readonly CharacterStatModel characterStatModel;
        private readonly CharacterStatView characterStatView;

        [Inject]
        public CharacterStatPresenter(CharacterStatModel characterStatModel, CharacterStatView characterStatView)
        {
            this.characterStatModel = characterStatModel;
            this.characterStatView = characterStatView;
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
            compositeDisposable.Dispose();
            Object.Destroy(characterStatView);
        }
    }
}