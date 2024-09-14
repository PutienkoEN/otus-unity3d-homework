using Zenject;

namespace Homeworks.SaveLoad
{
    public class SaveLoadInstaller : MonoInstaller
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


            Container
                .Bind<MyGameRepository>()
                .AsSingle();
        }
    }
}