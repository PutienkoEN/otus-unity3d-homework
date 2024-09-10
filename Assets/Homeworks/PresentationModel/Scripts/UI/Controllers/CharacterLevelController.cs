using System;
using UniRx;
using Zenject;

namespace Lessons.Architecture.PresentationModel
{
    public class CharacterLevelController : IInitializable, IDisposable
    {
        private readonly CompositeDisposable compositeDisposable = new();

        private readonly CharacterLevelModel characterLevelModel;
        private readonly CharacterLevelView characterLevelView;

        [Inject]
        public CharacterLevelController(CharacterLevelModel characterLevelModel, CharacterLevelView characterLevelView)
        {
            this.characterLevelModel = characterLevelModel;
            this.characterLevelView = characterLevelView;
        }

        public void Initialize()
        {
            characterLevelModel.CurrentLevel
                .Subscribe(UpdateViewForLevel)
                .AddTo(compositeDisposable);

            characterLevelModel.CurrentExperience
                .Subscribe(UpdateViewForExperience)
                .AddTo(compositeDisposable);

            characterLevelModel.CanLevelUp
                .Subscribe(UpdateViewForLevelUpButton)
                .AddTo(compositeDisposable);

            characterLevelView.OnLevelUpButtonClick()
                .Subscribe(LevelUp)
                .AddTo(compositeDisposable);
        }

        public void Dispose()
        {
            compositeDisposable.Dispose();
        }

        private void UpdateViewForLevel(int level)
        {
            var currentLevelText = "Level: " + level;
            characterLevelView.ChangeLevel(currentLevelText);
        }

        private void UpdateViewForExperience(int _)
        {
            var sliderData = CreateSliderData();
            characterLevelView.ChangeExperience(sliderData);
        }

        private void UpdateViewForLevelUpButton(bool canLevelUp)
        {
            characterLevelView.AllowLevelUp(canLevelUp);
        }

        private CharacterLevelView.PlayerLevelSliderData CreateSliderData()
        {
            var requiredExperience = characterLevelModel.RequiredExperience.Value;
            var currentExperience = characterLevelModel.CurrentExperience.Value;

            var experienceText = $"XP: {currentExperience} / {requiredExperience}";

            return new CharacterLevelView.PlayerLevelSliderData
            (
                requiredExperience,
                currentExperience,
                experienceText
            );
        }

        private void LevelUp(Unit _)
        {
            characterLevelModel.LevelUp();
        }
    }
}