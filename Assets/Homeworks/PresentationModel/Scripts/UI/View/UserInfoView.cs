using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PresentationModel
{
    public class UserInfoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Image iconImage;

        public void ChangeName(string userName)
        {
            nameText.text = userName;
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