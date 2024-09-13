using Zenject;

namespace Homeworks.SaveLoad
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<MyGameManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<MyGameRepository>()
                .AsSingle();
        }
    }
}