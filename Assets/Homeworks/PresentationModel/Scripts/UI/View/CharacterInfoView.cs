using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PresentationModel
{
    public class CharacterInfoView : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup container;
        [SerializeField] private CharacterStatView characterStatViewPrefab;

        private readonly List<CharacterStatView> characterStatViews = new();

        public CharacterStatView Add()
        {
            var characterStatView = Instantiate(characterStatViewPrefab, container.transform);
            characterStatViews.Add(characterStatView);
            return characterStatView;
        }

        public void Remove(CharacterStatView characterStatView)
        {
            if (characterStatViews.Remove(characterStatView))
            {
                Destroy(characterStatView.gameObject);
            }
        }
    }
}