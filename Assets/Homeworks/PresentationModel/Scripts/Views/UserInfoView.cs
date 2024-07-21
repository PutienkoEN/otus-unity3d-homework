using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM.Views
{
    public class UserInfoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Image iconImage;

        public void ChangeName(string name)
        {
            nameText.text = name;
        }

        public void ChangeDescription(string description)
        {
            descriptionText.text = description;
        }

        public void ChangeIcon(Sprite sprite)
        {
            iconImage.sprite = sprite;
        }
    }
}