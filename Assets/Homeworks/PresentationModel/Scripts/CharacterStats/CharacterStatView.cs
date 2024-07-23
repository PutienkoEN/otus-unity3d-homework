using TMPro;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public class CharacterStatView : MonoBehaviour
    {
        [SerializeField] private TMP_Text statText;

        public void UpdateStatValue(string statValue)
        {
            statText.text = statValue;
        }
    }
}