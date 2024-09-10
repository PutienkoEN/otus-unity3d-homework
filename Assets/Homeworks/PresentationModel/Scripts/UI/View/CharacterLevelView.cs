using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PresentationModel
{
    public class CharacterLevelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelText;

        [Header("Slider")] [SerializeField] private Slider experienceSlider;
        [SerializeField] private TMP_Text experienceSliderText;

        [SerializeField] private Button levelUpButton;


        public IObservable<Unit> OnLevelUpButtonClick()
        {
            return levelUpButton.OnClickAsObservable();
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