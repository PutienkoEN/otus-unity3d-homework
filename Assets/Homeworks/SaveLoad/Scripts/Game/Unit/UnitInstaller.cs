using Zenject;

namespace Homeworks.SaveLoad
{
    public class UnitInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var units = FindObjectsOfType<UnitObject>();

            Container
                .Bind<UnitManager>()
                .AsSingle()
                .WithArguments(units)
                .NonLazy();
        }
    }
}