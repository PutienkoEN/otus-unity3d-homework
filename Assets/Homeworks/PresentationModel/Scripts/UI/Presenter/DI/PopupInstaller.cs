using Lessons.Architecture.PM.Views;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Lessons.Architecture.PM.Mono.DI
{
    public class PopupInstaller : MonoInstaller
    {
        [SerializeField] private UserInfoView userInfoView;
        [FormerlySerializedAs("playerLevelView")] [SerializeField] private CharacterLevelView characterLevelView;

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
                .BindInstance(characterLevelView)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<CharacterLevelPresenter>()
                .AsSingle()
                .NonLazy();
        }
    }
}