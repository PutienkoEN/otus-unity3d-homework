using System;
using Lessons.Architecture.PM.Views;
using Zenject;

namespace Lessons.Architecture.PM.Mono
{
    public class PlayerLevelPresenter : IInitializable, IDisposable
    {
        private PlayerLevelModel playerLevelModel;
        private PlayerLevelView playerLevelView;

        [Inject]
        public PlayerLevelPresenter(PlayerLevelModel playerLevelModel, PlayerLevelView playerLevelView)
        {
            this.playerLevelModel = playerLevelModel;
            this.playerLevelView = playerLevelView;
        }

        public void Initialize()
        {
            playerLevelModel.OnLevelUp += UpdateViewOnLevelUp;
            playerLevelModel.OnExperienceChanged += UpdateViewForExperience;

            playerLevelView.SubscribeToLevelUpClick(LevelUp);

            UpdateViewOnLevelUp();
        }

        public void Dispose()
        {
            playerLevelModel.OnLevelUp -= UpdateViewOnLevelUp;
            playerLevelModel.OnExperienceChanged -= UpdateViewForExperience;

            playerLevelView.UnsubscribeToLevelUpClick(LevelUp);
        }

        private void LevelUp()
        {
            playerLevelModel.LevelUp();

            UpdateViewOnLevelUp();
        }

        private void UpdateViewOnLevelUp()
        {
            UpdateViewForLevel();
            UpdateViewForExperience();
        }

        private void UpdateViewForLevel()
        {
            var currentLevel = playerLevelModel.CurrentLevel;
            var currentLevelText = "Level: " + currentLevel;
            playerLevelView.ChangeLevel(currentLevelText);
        }

        // I assume we can't change model, so this method required only to pass to View.
        private void UpdateViewForExperience(int currentExperience)
        {
            UpdateViewForExperience();
        }

        private void UpdateViewForExperience()
        {
            var sliderData = CreateSliderData();
            playerLevelView.ChangeExperience(sliderData);

            var canLevelUp = playerLevelModel.CanLevelUp();
            playerLevelView.AllowLevelUp(canLevelUp);
        }

        private PlayerLevelView.PlayerLevelSliderData CreateSliderData()
        {
            var requiredExperience = playerLevelModel.RequiredExperience;
            var currentExperience = playerLevelModel.CurrentExperience;

            var experienceText = $"XP: {currentExperience} / {requiredExperience}";

            return new PlayerLevelView.PlayerLevelSliderData
            (
                requiredExperience,
                currentExperience,
                experienceText
            );
        }
    }
}