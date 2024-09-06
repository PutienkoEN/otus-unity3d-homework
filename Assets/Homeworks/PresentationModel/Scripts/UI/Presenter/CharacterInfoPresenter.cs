using System;
using System.Collections.Generic;
using Homeworks.PresentationModel.Scripts.UI.View;
using UniRx;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class CharacterInfoPresenter : IInitializable, IDisposable
    {
        private readonly CompositeDisposable compositeDisposable = new();

        private readonly CharacterInfoModel characterInfoModel;
        private readonly CharacterInfoView characterInfoView;

        private readonly Dictionary<CharacterStatModel, CharacterStatPresenter> characterStatModelToPresenter = new();

        [Inject]
        public CharacterInfoPresenter(CharacterInfoModel characterInfoModel, CharacterInfoView characterInfoView)
        {
            this.characterInfoModel = characterInfoModel;
            this.characterInfoView = characterInfoView;
        }

        public void Initialize()
        {
            characterInfoModel.stats
                .ObserveAdd()
                .Subscribe(v => OnStatAdded(v.Value))
                .AddTo(compositeDisposable);

            characterInfoModel.stats
                .ObserveRemove()
                .Subscribe(v => OnStatRemoved(v.Value))
                .AddTo(compositeDisposable);
            
            foreach (var characterStatModel in characterInfoModel.stats)
            {
                OnStatAdded(characterStatModel);
            }
        }

        public void Dispose()
        {
            compositeDisposable.Dispose();
        }

        private void OnStatAdded(CharacterStatModel statModel)
        {
            var characterStatView = characterInfoView.Add();
            var characterStatPresenter = new CharacterStatPresenter(statModel, characterStatView);
            characterStatPresenter.Initialize();

            characterStatModelToPresenter.Add(statModel, characterStatPresenter);
        }

        private void OnStatRemoved(CharacterStatModel statModel)
        {
            if (characterStatModelToPresenter.TryGetValue(statModel, out var characterStatPresenter))
            {
                characterStatPresenter.Dispose();
                characterStatModelToPresenter.Remove(statModel);
            }
        }
    }
}