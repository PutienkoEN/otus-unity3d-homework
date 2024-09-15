using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad.Scripts.Persistance
{
    public class PersistenceInstaller : MonoInstaller
    {
        [SerializeField] private string gameContextProperty = "Lesson/GameState";

        public override void InstallBindings()
        {
            Container
                .Bind<GameContextRepository>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<SimplePersistingStrategy>()
                .AsSingle()
                .WithArguments(gameContextProperty);
        }
    }
}