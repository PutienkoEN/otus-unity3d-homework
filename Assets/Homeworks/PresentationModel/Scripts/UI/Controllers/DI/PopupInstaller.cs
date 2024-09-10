using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PresentationModel
{
    public class PopupInstaller : MonoInstaller
    {
        [SerializeField] private UserInfoView userInfoView;
        [SerializeField] private CharacterLevelView characterLevelView;
        [SerializeField] private CharacterInfoView characterInfoView;

        public override void InstallBindings()
        {
            BindUserInfo();
            BindCharacterLevel();
            BindCharacterInfo();
        }

        private void BindCharacterInfo()
        {
            Container
                .BindInstance(characterInfoView)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<CharacterInfoController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindCharacterLevel()
        {
            Container
                .BindInstance(characterLevelView)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<CharacterLevelController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindUserInfo()
        {
            Container
                .BindInstance(userInfoView)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<UserInfoController>()
                .AsSingle()
                .NonLazy();
        }
    }
}