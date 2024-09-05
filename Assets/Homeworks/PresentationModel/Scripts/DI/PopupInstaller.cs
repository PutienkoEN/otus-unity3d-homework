using Lessons.Architecture.PM.Views;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM.Mono.DI
{
    public class PopupInstaller : MonoInstaller
    {
        [SerializeField] private UserInfoView userInfoView;
        [SerializeField] private PlayerLevelView playerLevelView;

        public override void InstallBindings()
        {
            Container
                .BindInstance(userInfoView)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<UserInfoPresenter>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInstance(playerLevelView)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<PlayerLevelPresenter>()
                .AsSingle()
                .NonLazy();
        }
    }
}