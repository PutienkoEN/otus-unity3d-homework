using System;
using Lessons.Architecture.PM.Views;
using Zenject;

namespace Lessons.Architecture.PM.Mono
{
    public class CharacterLevelPresenter : IInitializable, IDisposable
    {
        private CharacterLevelModel characterLevelModel;
        private CharacterLevelView characterLevelView;

        [Inject]
        public CharacterLevelPresenter(CharacterLevelModel characterLevelModel, CharacterLevelView characterLevelView)
        {
            this.characterLevelModel = characterLevelModel;
            this.characterLevelView = characterLevelView;
        }

        public void Initialize()
        {
            characterLevelModel.OnLevelUp += UpdateViewOnLevelUp;
            characterLevelModel.OnExperienceChanged += UpdateViewForExperience;

            characterLevelView.SubscribeToLevelUpClick(LevelUp);

            UpdateViewOnLevelUp();
        }

        public void Dispose()
        {
            characterLevelModel.OnLevelUp -= UpdateViewOnLevelUp;
            characterLevelModel.OnExperienceChanged -= UpdateViewForExperience;

            characterLevelView.UnsubscribeToLevelUpClick(LevelUp);
        }

        private void LevelUp()
        {
            characterLevelModel.LevelUp();

            UpdateViewOnLevelUp();
        }

        private void UpdateViewOnLevelUp()
        {
            UpdateViewForLevel();
            UpdateViewForExperience();
        }

        private void UpdateViewForLevel()
        {
            var currentLevel = characterLevelModel.CurrentLevel;
            var currentLevelText = "Level: " + currentLevel;
            characterLevelView.ChangeLevel(currentLevelText);
        }

        // I assume we can't change model, so this method required only to pass to View.
        private void UpdateViewForExperience(int currentExperience)
        {
            UpdateViewForExperience();
        }

        private void UpdateViewForExperience()
        {
            var sliderData = CreateSliderData();
            characterLevelView.ChangeExperience(sliderData);

            var canLevelUp = characterLevelModel.CanLevelUp();
            characterLevelView.AllowLevelUp(canLevelUp);
        }

        private CharacterLevelView.PlayerLevelSliderData CreateSliderData()
        {
            var requiredExperience = characterLevelModel.RequiredExperience;
            var currentExperience = characterLevelModel.CurrentExperience;

            var experienceText = $"XP: {currentExperience} / {requiredExperience}";

            return new CharacterLevelView.PlayerLevelSliderData
            (
                requiredExperience,
                currentExperience,
                experienceText
            );
        }
    }
}