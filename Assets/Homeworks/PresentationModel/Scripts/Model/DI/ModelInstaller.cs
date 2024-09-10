using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PresentationModel
{
    public class ModelInstaller : MonoInstaller
    {
        [Header("User Data (Use to configure initial values)")] [SerializeField]
        private string userName;

        [SerializeField] private string userDescription;
        [SerializeField] private Sprite userIcon;

        [SerializeField] private List<string> characterStats;

        public override void InstallBindings()
        {
            Container
                .Bind<UserInfoModel>()
                .AsSingle()
                .OnInstantiated<UserInfoModel>((_, it) => SetupUserInfo(it))
                .NonLazy();

            Container
                .Bind<CharacterLevelModel>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<CharacterInfoModel>()
                .AsSingle()
                .OnInstantiated<CharacterInfoModel>(
                    (_, characterInfo) => SetupCharacterInfoModel(characterInfo))
                .NonLazy();
        }

        private void SetupUserInfo(UserInfoModel userInfoModel)
        {
            userInfoModel.ChangeName(userName);
            userInfoModel.ChangeDescription(userDescription);
            userInfoModel.ChangeIcon(userIcon);
        }


        private void SetupCharacterInfoModel(CharacterInfoModel characterInfoModel)
        {
            foreach (var characterStat in characterStats)
            {
                characterInfoModel.stats.Add(new CharacterStatModel(characterStat, 10));
            }
        }
    }
}