using Zenject;

namespace Homeworks.SaveLoad
{
    public class SaveLoaderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<MyGameManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<UnitSaveLoader>()
                .AsSingle();

            Container
                .Bind<SaveLoadManager>()
                .AsSingle()
                .NonLazy();
        }
    }
}