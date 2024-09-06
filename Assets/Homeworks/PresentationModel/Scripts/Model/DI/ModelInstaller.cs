using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM.Mono
{
    public class ModelInstaller : MonoInstaller
    {
        [Header("User Data (Use to configure initial values)")] [SerializeField]
        private string userName;

        [SerializeField] private string userDescription;
        [SerializeField] private Sprite userIcon;

        [Space] [Header("Data Debug")] [SerializeField]
        private UserInfoModel userInfoModel;

        public override void InstallBindings()
        {
            Container
                .Bind<UserInfoModel>()
                .AsSingle()
                .OnInstantiated<UserInfoModel>((_, userInfoModel) => SetupUserInfo(userInfoModel))
                .NonLazy();

            Container
                .Bind<CharacterInfoModel>()
                .AsSingle()
                .OnInstantiated<CharacterInfoModel>(
                    (_, characterInfoModel) => SetupCharacterInfoModel(characterInfoModel))
                .NonLazy();

            Container
                .Bind<CharacterLevelModel>()
                .AsSingle()
                .NonLazy();

            userInfoModel = Container.Resolve<UserInfoModel>();
        }


        private void SetupUserInfo(UserInfoModel userInfoModel)
        {
            userInfoModel.ChangeName(userName);
            userInfoModel.ChangeDescription(userDescription);
            userInfoModel.ChangeIcon(userIcon);
        }


        private void SetupCharacterInfoModel(CharacterInfoModel characterInfoModel)
        {
            // TODO: Implement it.
        }
    }
}