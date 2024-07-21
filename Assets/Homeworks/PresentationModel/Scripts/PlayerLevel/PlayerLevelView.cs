using Lessons.Architecture.PM.Mono;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM.Views
{
    public class PlayerLevelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelText;

        [Header("Slider")] [SerializeField] private Slider experienceSlider;
        [SerializeField] private TMP_Text experienceSliderText;

        [SerializeField] private Button levelUpButton;

        public void Construct(PlayerLevelPresenter playerLevelPresenter)
        {
            levelUpButton.onClick.AddListener(playerLevelPresenter.LevelUp);
        }

        public void AllowLevelUp(bool allowLevelUp)
        {
            levelUpButton.interactable = allowLevelUp;
        }

        public void ChangeLevel(string level)
        {
            levelText.text = level;
        }

        public void ChangeExperience(PlayerLevelSliderData sliderData)
        {
            experienceSlider.maxValue = sliderData.MaxValue;
            experienceSlider.value = sliderData.CurrentValue;

            experienceSliderText.text = sliderData.Text;
        }

        public struct PlayerLevelSliderData
        {
            public float MaxValue { get; }
            public float CurrentValue { get; }
            public string Text { get; }

            public PlayerLevelSliderData(float maxValue, float currentValue, string text)
            {
                MaxValue = maxValue;
                CurrentValue = currentValue;
                Text = text;
            }
        }
    }
}