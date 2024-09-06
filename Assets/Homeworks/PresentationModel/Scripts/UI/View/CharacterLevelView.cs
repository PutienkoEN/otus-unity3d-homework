using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Lessons.Architecture.PM.Views
{
    public class CharacterLevelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelText;

        [Header("Slider")] [SerializeField] private Slider experienceSlider;
        [SerializeField] private TMP_Text experienceSliderText;

        [SerializeField] private Button levelUpButton;

        public void SubscribeToLevelUpClick(UnityAction onLevelUp)
        {
            levelUpButton.onClick.AddListener(onLevelUp);
        }

        public void UnsubscribeToLevelUpClick(UnityAction onLevelUp)
        {
            levelUpButton.onClick.RemoveListener(onLevelUp);
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