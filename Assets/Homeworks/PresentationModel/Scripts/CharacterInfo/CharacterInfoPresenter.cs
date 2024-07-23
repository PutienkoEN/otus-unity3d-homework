using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public class CharacterInfoPresenter
    {
        [SerializeField] private CharacterInfoModel characterInfoModel;

        private List<CharacterStatPresenter> characterStatPresenters = new();

        private Transform characterStatGrid;
        private CharacterStatView characterStatViewPrefab;

        public void Construct(
            CharacterInfoModel characterInfoModel,
            CharacterStatView characterStatViewPrefab,
            Transform characterStatGrid
        )
        {
            this.characterInfoModel = characterInfoModel;
            this.characterStatViewPrefab = characterStatViewPrefab;
            this.characterStatGrid = characterStatGrid;

            characterInfoModel.OnStatAdded += AddNewStat;
            characterInfoModel.OnStatRemoved += RemoveStat;

            CreateStat();
        }

        private void AddNewStat(CharacterStatModel characterStatModel)
        {
            var characterStatView = Object.Instantiate(characterStatViewPrefab, characterStatGrid);

            var characterStatPresenter = new CharacterStatPresenter(characterStatModel, characterStatView);
            characterStatPresenters.Add(characterStatPresenter);
        }

        private void RemoveStat(CharacterStatModel characterStatModel)
        {
            var statPresenters = characterStatPresenters
                .FindAll(presenter => presenter.IsSameStat(characterStatModel));

            characterStatPresenters
                .RemoveAll(presenter => presenter.IsSameStat(characterStatModel));

            statPresenters.ForEach(presenter => presenter.Dispose());
        }

        public void CreateStat()
        {
            var characterStatModel = new CharacterStatModel("Strength", 5);
            characterInfoModel.AddStat(characterStatModel);
        }
    }
}