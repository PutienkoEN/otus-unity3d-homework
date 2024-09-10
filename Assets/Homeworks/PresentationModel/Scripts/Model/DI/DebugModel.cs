using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PresentationModel
{
    public class DebugModel : MonoBehaviour
    {
        [SerializeField] private UserInfoModel userInfoModel;
        [SerializeField] private CharacterLevelModel characterLevelModel;
        [SerializeField] private CharacterInfoModel characterInfoModel;

        [Inject]
        public void Construct(
            UserInfoModel userInfoModel,
            CharacterLevelModel characterLevelModel,
            CharacterInfoModel characterInfoModel)
        {
            this.userInfoModel = userInfoModel;
            this.characterLevelModel = characterLevelModel;
            this.characterInfoModel = characterInfoModel;
        }
    }
}