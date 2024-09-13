using Zenject;

namespace Homeworks.SaveLoad
{
    public class SaveLoaderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
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